using System.Globalization;
using System.Xml.Linq;
using UnityEngine;

namespace Engine
{
    public class ParameterImpact
    {
        public string Parameter;
        public float Value;

        public ParameterImpact(XElement xElement)
        {
            if (xElement.Attribute("param") == null)
                Debug.Log("None param attribute in " + xElement.ToString());
            else Parameter = xElement.Attribute("param").Value;

            if (xElement.Attribute("value") == null)
                Debug.Log("None value attribute in " + xElement.ToString());
            else Value = float.Parse(xElement.Attribute("value").Value, CultureInfo.InvariantCulture);
        }

        public ParameterImpact(ParameterImpact cloneFrom)
        {
            Parameter = cloneFrom.Parameter;
            Value = cloneFrom.Value;
        }
    }
}