using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace Extras
{
    public class StringsTranslator
    {
        private const string Filename = "strings";
        private const string Path = "Localization";
        private static string _language = "";
        private static Dictionary<string, string> _data;

        public static string EnterString(string name)
        {
            if (_data == null)
            {
                Load();
            }

            if (_data.ContainsKey(name))
            {
                return _data[name];
            }
            else
            {
                Debug.LogError("Can't find string with name " + name);
            }

            return null;
        }

        public static bool ContainsString(string name)
        {
            if (_data == null)
            {
                Load();
            }

            return _data.ContainsKey(name);
        }
        
        private static void Load()
        {
            string SystemLanguageCode = ConvertLanguageCode(Application.systemLanguage);

            _data = new Dictionary<string, string>();
            LoadResources(Path + Filename + SystemLanguageCode);
        }

        private static string ConvertLanguageCode(SystemLanguage language)
        {
            switch (language)
            {
                case SystemLanguage.English:
                    return "en";
                case SystemLanguage.Russian:
                    return "ru";
                case SystemLanguage.Polish:
                    return "pl";
                default:
                    return "en";
            }
        }

        private static void LoadResources(string resourceXML)
        {
            XDocument document = Functions.LoadResourceXMLAsDoc(resourceXML);
            XElement content = document.Element("strings");
            IEnumerable<XElement> nodeList = content.Descendants("string");

            foreach (XElement node in nodeList)
            {
                string name = node.Attribute("name").Value;
                string text = node.Value;
                text = text.Replace("\n", " ");
                text = text.Replace("\\n", "\n");

                if (name == null)
                {
                    Debug.LogError("String has null value: " + text + " in " + resourceXML);
                }

                if (_data.ContainsKey(name))
                {
                    _data[name] = text;
                }
                else
                {
                    _data.Add(name, text);
                }
            }
        }
    }
}