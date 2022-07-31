using System;

namespace Engine
{
    public class MedicalEvents: MedicalEventPrototype
    {
        public BodyParts Localization;
        public float Intensity;
        public float Exacerbation;

        public MedicalEvents(MedicalEventPrototype prototype)
        {
            Name = prototype.Name;
            foreach (ParameterImpact damage in prototype.TotalDamage)
            {
                TotalDamage.Add(new ParameterImpact(damage));
                Revealed = prototype.Revealed;
                Localization = BodyParts.Any;
                Intensity = 1;
                Exacerbation = 1;
            }
        }
    }
}