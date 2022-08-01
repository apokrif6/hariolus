using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace Engine
{
    public class Substance
    {
        public string Name;
        public List<HealEvents> Heal;
        public float Size;
        public string UnitName;

        public Substance()
        {
            Heal = new List<HealEvents>();
        }

        //klon innej substancji
        public Substance(Substance cloneFrom)
        {
            Name = cloneFrom.Name;
            Heal = new List<HealEvents>();

            foreach (HealEvents healClone in cloneFrom.Heal)
            {
                Heal.Add(new HealEvents(healClone));
                Size = cloneFrom.Size;
                UnitName = cloneFrom.UnitName;
            }
        }

        public void LoadData(XElement xElement)
        {
            Size = 0;
            if (xElement.Attribute("name") == null)
                Debug.Log("None name attribute in " + xElement);
            else
                Name = xElement.Attribute("name").Value;
            
            UnitName = xElement.Attribute("unitName") != null ? xElement.Attribute("unitName").Value : "";
            
            foreach (XElement element in xElement.Elements())
            {
                if (element.Name != "heal") continue;
                
                HealEvents healElement = new HealEvents(element);
                Heal.Add(healElement);
            }
        }
    }
}