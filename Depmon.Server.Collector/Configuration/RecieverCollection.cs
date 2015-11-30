using System.Configuration;

namespace Depmon.Server.Collector.Configuration
{
    public class RecieverCollection: ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new Reciever();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Reciever)element).Name;
        }

        public Reciever this[int idx] => (Reciever)BaseGet(idx);
    }
}
