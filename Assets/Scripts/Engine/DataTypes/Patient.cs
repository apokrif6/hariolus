using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public class Patient: Character
    {
        public int NumberCurrentEvents { get { return _currentEvents.Count;}}
        private List<MedicalEvent> _currentEvents;
        
        public Patient() : base()
        {
            _currentEvents = new List<MedicalEvent>();
        }

        public void AddNewEvent(MedicalEvent medicalEvent)
        {
            Debug.Log("Get:  " + medicalEvent.Name);
            _currentEvents.Add(medicalEvent);
        }   
    }
}