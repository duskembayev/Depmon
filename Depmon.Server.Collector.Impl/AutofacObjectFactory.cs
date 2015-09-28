using System;
using Autofac;

namespace Depmon.Server.Collector.Impl
{
    /// <summary>
	/// Фабрика объектов, реализованная через Autofac. Абстрагирует IoC-контейнер. Нельзя напрямую инжектировать
	/// в классы с бизнес логикой, так как в этом случае он будет являться Service Locator (антипаттерн). Можно
	/// использовать только в фабриках, создающих объекты конкретных типов.
	/// </summary>
	/// <remarks>
	/// Использование Service Locator является антипаттерном, так как с помощью него
	/// можно создать любую зависимость, что скрывает, какие зависимости есть у класса.
	/// Больше информации здесь: http://blog.ploeh.dk/2010/02/03/ServiceLocatorIsAnAntiPattern.aspx
	/// Поэтому IObjectFactory нужно оборачивать в фабрики, которые могут создавать только один тип
	/// объектов. В нашем случае это, например, IRepositoryFactory, IQueryFactory. Только так можно
	/// быть уверенным, что пользователи этих фабрик не смогут создавать объекты любых типов по своему
	/// усмотрению.
	/// </remarks>
	public class AutofacObjectFactory : IObjectFactory, IDisposable
    {
        #region Private Fields

        // Объект, определяющий временные рамки существования создаваемого объекта
        private readonly IContainer _container;
        private ILifetimeScope _scope;
        private bool _isDisposed;

        #endregion

        #region Constructors

        public AutofacObjectFactory(IContainer container)
        {
            _container = container;
        }

        #endregion

        #region IObjectFactory Members

        /// <summary>
        /// Создает объект, тип которого зарегистрирован в IoC-контейнере.
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <returns>Экземпляр объекта</returns>
        public T Create<T>()
        {
            return _container.Resolve<T>();
        }

        public object Create(Type specificType)
        {
            return _container.Resolve(specificType);
        }

        public void CreateScope()
        {
            _scope?.Dispose();
            _scope = _container.BeginLifetimeScope();
        }

        #endregion

        public void Dispose()
        {
            if (_isDisposed) return;

            _isDisposed = true;
            _scope?.Dispose();
        }
    }
}
