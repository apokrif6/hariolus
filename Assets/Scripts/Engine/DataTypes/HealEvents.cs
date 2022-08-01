using System;
using System.Globalization;
using System.Xml.Linq;
using UnityEngine;

namespace Engine
{
    public class HealEvents
    {
        public string EventType;
        public BodyParts WhereHeal;
        public float Heal;
        public float BorderValue;
        public GivingMethod GivingMethod;

        public HealEvents(XElement xElement)
        {
            if (xElement.Attribute("eventType") == null)
                Debug.Log("None eventType attribute in " + xElement);
            else EventType = xElement.Attribute("eventType").Value;

            if (xElement.Attribute("whereHeal") == null)
                Debug.Log("None whereHeal attribute in " + xElement);
            else
                WhereHeal = (BodyParts)Enum.Parse(typeof(BodyParts), xElement.Attribute("whereHeal").Value);

            if (xElement.Attribute("heal") == null)
                Debug.Log("None heal attribute in " + xElement);
            else Heal = float.Parse(xElement.Attribute("heal").Value, CultureInfo.InvariantCulture);

            if (xElement.Attribute("borderValue") == null)
                BorderValue = 0;
            else BorderValue = float.Parse(xElement.Attribute("borderValue").Value, CultureInfo.InvariantCulture);

            if (xElement.Attribute("givingMethod") == null)
                Debug.Log("None givingMethod attribute in " + xElement);
            else GivingMethod = (GivingMethod)Enum.Parse(typeof(GivingMethod), xElement.Attribute("givingMethod").Value);

        }

        public HealEvents(HealEvents cloneFrom)
        {
            EventType = cloneFrom.EventType;
            WhereHeal = cloneFrom.WhereHeal;
            Heal = cloneFrom.Heal;
            BorderValue = cloneFrom.BorderValue;
            GivingMethod = cloneFrom.GivingMethod;
        }
    }
}