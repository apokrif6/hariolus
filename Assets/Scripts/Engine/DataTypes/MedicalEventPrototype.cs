using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace Engine
{
    public class MedicalEventPrototype
    {
        public string Name;
        public List<ParameterImpact> TotalDamage;
        public bool Revealed; 

        public MedicalEventPrototype()
        {
            TotalDamage = new List<ParameterImpact>();
        }

        public void LoadData(XElement xElement)
        {
            if (xElement.Attribute("name") == null)
                Debug.LogError("None name attribute in " + xElement.ToString());
            else Name = xElement.Attribute("name").Value;
            
            Revealed = xElement.Attribute("revealed") == null;
            
            foreach (XElement element in xElement.Elements())
            {
                if (element.Name != "damage") continue;
                
                ParameterImpact damage = new ParameterImpact(element);
                TotalDamage.Add(damage);
            }
        }
    }
}