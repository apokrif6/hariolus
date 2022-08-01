using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace Engine
{
    public class HealItem
    {
        public string Name;
        public int Uptime;
        public List<Substance> ActiveSubstance;

        public HealItem()
        {
            ActiveSubstance = new List<Substance>();
        }
        
        public void LoadData(XElement xElement, List<Substance> listSubstancePatterns)
        {
            if (xElement.Attribute("name") == null)
                Debug.Log("None name attribute in " + xElement);
            else
                Name = xElement.Attribute("name").Value;
            
            Uptime = xElement.Attribute("uptime") != null ? int.Parse(xElement.Attribute("uptime").Value) : 1;
            
            foreach (XElement element in xElement.Elements())
            {
                if (element.Name == "substance")
                {
                    if(element.Attribute("name") == null)
                        Debug.Log("None name attribute in " + element);
                    else
                    {
                        string substanceName = element.Attribute("name").Value;
                        Substance FindedSubstance = null;
                        
                        foreach(Substance substance in listSubstancePatterns)
                        {
                            if (substance.Name == substanceName)
                                FindedSubstance = new Substance(substance);
                        }
                        
                        if(FindedSubstance == null)
                            Debug.Log("None substance with name " + substanceName);
                        else
                        {
                            FindedSubstance.Size = element.Attribute("count") == null ? 1 : int.Parse(element.Attribute("count").Value );

                            ActiveSubstance.Add(FindedSubstance);
                        }
                    }
                }
            }
        }
    }
}