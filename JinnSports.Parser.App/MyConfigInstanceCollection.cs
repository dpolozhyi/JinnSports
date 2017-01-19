using System.Configuration;

namespace JinnSports.Parser.App
{
    public class MyConfigInstanceCollection : ConfigurationElementCollection
    {
        public MyConfigInstanceElement this[int index]
        {
            get { return this.BaseGet(index) as MyConfigInstanceElement; }
        }

        public new MyConfigInstanceElement this[string name]
        {
            get { return this.BaseGet(name) as MyConfigInstanceElement; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new MyConfigInstanceElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MyConfigInstanceElement)element).Profile;
        }
    }
}