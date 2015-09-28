﻿using System;

namespace Depmon.Server.Collector
{
    /// <summary>
	/// Фабрика объектов. Абстрагирует IoC-контейнер. Нельзя напрямую инжектировать
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
	public interface IObjectFactory
    {
        /// <summary>
        /// Создает объект, тип которого зарегистрирован в IoC-контейнере.
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <returns>Экземпляр объекта</returns>
        T Create<T>();
        
        object Create(Type specificType);

        void CreateScope();
    }
}
