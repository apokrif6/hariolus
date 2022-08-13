using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public class Patient: Character
    {
        public static int NextID = 0;

        public bool IsAlive
        {
            get { return GetCharacteristic("Health") > 0; }
        }
        public bool IsHealthy
        {
            get { return _currentEvents.Count == 0;  }
        }

        public int NumberCurrentEvents
        {
            get { return _currentEvents.Count;}
        }

        public int IDPatient
        {
            get;
            private set;
        }

        public string DeathCause
        {
            get;
            private set;
        }
        
        private List<MedicalEvent> _currentEvents;
        
        public Patient() : base()
        {
            Characteristics.Add("Health", 100);
            Characteristics.Add("Strength", 100);
            Characteristics.Add("Vitality", 100);

            IDPatient = ++NextID;
            DeathCause = "";
            
            _currentEvents = new List<MedicalEvent>();
        }

        public void AddNewEvent(MedicalEvent medicalEvent)
        {
            Debug.Log("Get:  " + medicalEvent.Name);
            _currentEvents.Add(medicalEvent);
        }

        public void HourlyEventHandling()
        {
            List<MedicalEvent> allNewMedicalEvents = new List<MedicalEvent>();
            List<MedicalEvent> medicalEventsToUnset = new List<MedicalEvent>();

            foreach (MedicalEvent medicalEvent in _currentEvents)
            {
                List<MedicalEvent> newMedicalEvents = HerbalismManager.Instance.ProcessMedicalEventStatus(medicalEvent);
                allNewMedicalEvents.AddRange(newMedicalEvents);

                medicalEvent.Intensity = medicalEvent.Intensity - (1 - GetCharacteristic("Vitality") / 5000f) -
                                         0.01f * (GetCharacteristic("Vitality") / 50f);

                foreach (ParameterImpact impact in medicalEvent.TotalDamage)
                {
                    SetCharacteristic(impact.Parameter, GetCharacteristic(impact.Parameter) + impact.Value * medicalEvent.Intensity * 1f);
                }

                if (medicalEvent.Mortality > 0 && Random.Range(0f, 1f) < (medicalEvent.Mortality * 1f))
                {
                    DeathCause = string.Format("{0} {1}", medicalEvent.Name, medicalEvent.Localization);
                    SetCharacteristic("Health", 0);
                }

                if (medicalEvent.Intensity <= 0)
                {
                    medicalEventsToUnset.Add(medicalEvent);
                }

                if (medicalEvent.Disposable)
                {
                    medicalEventsToUnset.Add(medicalEvent);
                }
            }
            
            _currentEvents.AddRange(allNewMedicalEvents);

            for (int i = 0; i < _currentEvents.Count; i++)
            {
                for (int j = i + 1; j < _currentEvents.Count; j++)
                {
                    if (_currentEvents[i].Name == _currentEvents[j].Name &&
                        (_currentEvents[i].Localization & _currentEvents[j].Localization) != 0)
                    {
                        _currentEvents[i].Intensity += _currentEvents[j].Intensity;
                        _currentEvents[i].Exacerbation = _currentEvents[i].Exacerbation < _currentEvents[j].Exacerbation
                            ? _currentEvents[i].Exacerbation
                            : _currentEvents[j].Exacerbation;
                        _currentEvents[i].Normalize();
                        
                        medicalEventsToUnset.Add(_currentEvents[j]);
                    }
                }
            }

            foreach (MedicalEvent medicalEvent in medicalEventsToUnset)
            {
                _currentEvents.Remove(medicalEvent);
            }
        }
    }
}