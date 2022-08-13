using System.Collections.Generic;

namespace Engine
{
    public class Character
    {
        protected Dictionary<string, float> Characteristics;

        public Character()
        {
            Characteristics = new Dictionary<string, float>();
        }

        public float GetCharacteristic(string name)
        {
            if (Characteristics.ContainsKey(name))
                return Characteristics[name];

            return 0;
        }

        public void SetCharacteristic(string name, float value)
        {
            if (Characteristics.ContainsKey(name))
                Characteristics[name] = value;
        }
    }
}