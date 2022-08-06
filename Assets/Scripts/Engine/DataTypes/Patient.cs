using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public class Patient: Character
    {
        public int NumberCurrentEvents { get { return _currentEvents.Count;}}
        private List<MedicalEvents> _currentEvents;
        
        public Patient() : base()
        {
            _currentEvents = new List<MedicalEvents>();
        }

        public void AddNewEvent(MedicalEvents medicalEvent)
        {
            Debug.Log("Get:  " + medicalEvent.Name);
            _currentEvents.Add(medicalEvent);
        }   
    }
}