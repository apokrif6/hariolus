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
    }
}