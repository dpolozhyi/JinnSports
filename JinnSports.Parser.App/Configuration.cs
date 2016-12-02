using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinnSports.Parser.App
{
    public static class ConfigSettings
    {
        public static string Xml()
        {
            var config = ConfigurationManager.GetSection("ProxyXml") as MyConfigSection;
            return config.Instances[0].Path;
        }
    }
    public class MyConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public MyConfigInstanceCollection Instances
        {
            get { return (MyConfigInstanceCollection)this[""]; }
            set { this[""] = value; }
        }
    }
    public class MyConfigInstanceCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new MyConfigInstanceElement();
        }
        public MyConfigInstanceElement this[int index]
        {
            get { return this.BaseGet(index) as MyConfigInstanceElement; }
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            //set to whatever Element Property you want to use for a key
            return ((MyConfigInstanceElement)element).Path;
        }
    }

    public class MyConfigInstanceElement : ConfigurationElement
    {
        [ConfigurationProperty("path", IsKey = true, IsRequired = true)]
        public string Path
        {
            get { return (string)base["path"]; }
            set { base["path"] = value; }
        }
    }
}