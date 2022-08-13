using System;
using Random = UnityEngine.Random;

namespace Engine
{
    public class MedicalEvent: MedicalEventPrototype
    {
        public BodyParts Localization;
        public float Intensity;
        public float Exacerbation;

        public MedicalEvent(MedicalEventPrototype prototype)
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

        public void ProcessHourly()
        {
            Intensity += Exacerbation / 24;
            Exacerbation += Random.Range(0, 01f) - 0.05f;
            Normalize();
        }

        public void Normalize()
        {
            if (Intensity < 0)
                Intensity = 0;

            if (Intensity > 1)
                Intensity = 1;

            if (Exacerbation < -0.5)
                Exacerbation = -0.5f;

            if (Exacerbation > 0.5)
                Exacerbation = 0.5f;
        }
    }
}