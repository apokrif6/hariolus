using System.IO;
using System;
using System.Text;
using UnityEngine;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Extras
{
    public class Functions
    {
        private static string LoadResourceXML(string fileName, AssetBundle abToLoad = null)
        {
            StringBuilder content = new StringBuilder();
            try
            {
                TextAsset resourceXML;

                if (abToLoad == null)
                    resourceXML = Resources.Load(fileName) as TextAsset;
                else
                    resourceXML = abToLoad.LoadAsset<TextAsset>(fileName);

                if (resourceXML == null)
                    return "";

                StringReader reader = new StringReader(resourceXML.text);
                content.Append(reader.ReadToEnd());
                reader.Close();
            }
            catch (Exception)
            {
                Debug.Log("Load error " + fileName);
            }

            return content.ToString();
        }

        public static XDocument LoadResourceXMLAsDoc(string fileName, AssetBundle abToLoad = null)
        {
            string content = LoadResourceXML(fileName, abToLoad);
            XDocument doc = XDocument.Parse(content);
            return doc;
        }

        public static IEnumerable<XElement> LoadResourceXMLAsElements(string fileName, string mainNode,
            AssetBundle abToLoad = null)
        {
            XDocument doc = LoadResourceXMLAsDoc(fileName, abToLoad);
            if (doc.Elements(mainNode) == null)
            {
                Debug.Log("Error in file " + fileName);
                return null;
            }
            else
            {
                return doc.Elements(mainNode).Elements();
            }
        }
    }
}