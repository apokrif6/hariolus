using System;
using System.Globalization;
using System.Xml.Linq;
using UnityEngine;

namespace Engine
{
    public class MedicalEventChance
    {
        public string PreviousEventType;
        public string NextEventType;
        public float Chance;
        public BodyParts PreviousLocalization;
        public BodyParts NextLocalization;
        public float Mortality;

        public void LoadData(XElement xElement)
        {
            if (xElement.Attribute("previous") == null || xElement.Attribute("previous").Value == "")
                PreviousEventType = null;
            else PreviousEventType = xElement.Attribute("previous").Value;

            if (xElement.Attribute("next") == null || xElement.Attribute("next").Value == "")
                Debug.Log("None next attribute in " + xElement);
            else NextEventType = xElement.Attribute("next").Value;

            if (xElement.Attribute("chance") == null)
                Debug.Log("None chance attribute in  " + xElement);
            else
                Chance = float.Parse(xElement.Attribute("chance").Value, CultureInfo.InvariantCulture);

            if (xElement.Attribute("previousLocalization") == null)
                Debug.Log("None previousLocalization attribute in " + xElement);
            else
                PreviousLocalization =
                    (BodyParts) Enum.Parse(typeof(BodyParts), xElement.Attribute("previousLocalization").Value);

            if (xElement.Attribute("nextLocalization") == null || xElement.Attribute("nextLocalization").Value == "")
                NextLocalization = PreviousLocalization;
            else
                NextLocalization =
                    (BodyParts) Enum.Parse(typeof(BodyParts), xElement.Attribute("nextLocalization").Value);

            if (xElement.Attribute("mortality") != null)
                Mortality = float.Parse(xElement.Attribute("mortality").Value, CultureInfo.InvariantCulture);
            else
                Mortality = 0;
        }
    }
}