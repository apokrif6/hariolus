using System;
using UnityEngine;

namespace Engine
{
    public class PatientComponent: MonoBehaviour
    {
        public const int MAX_DISEASES = 3;
        public Patient PrivatePatient { get; private set; }

        private void Start()
        {
            PrivatePatient = new Patient();
            DayNightSystem.Instance.ChangeHour += HourLapse;
            DayNightSystem.Instance.ChangeMinute += MinuteLapse;
        }

        private void Update()
        {
            var position = GameManager.GameManagerInstance.playerController.transform.position;
            
            Vector3 lookTarget = new Vector3(position.x, transform.position.y, position.z);
            
            Quaternion lookDirection = Quaternion.LookRotation(lookTarget - transform.position, transform.up);
            
            transform.rotation = lookDirection;
        }

        private void HourLapse(int hour)
        {
            if (PrivatePatient.NumberCurrentEvents < MAX_DISEASES)
            {
                MedicalEvent newMedicalEvent = HerbalismManager.Instance.CreatePrimaryMedicalEvent();

                if (newMedicalEvent != null)
                {
                    PrivatePatient.AddNewEvent(newMedicalEvent);
                }
            }
            
            if (PrivatePatient.NumberCurrentEvents > 0)
                gameObject.SetActive(true);
        }
        
        private void MinuteLapse(int minute)
        {
            if (PrivatePatient.NumberCurrentEvents == 0)
            {
                MedicalEvent medicalEvent = HerbalismManager.Instance.CreatePrimaryMedicalEvent();

                if (medicalEvent == null)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    PrivatePatient.AddNewEvent(medicalEvent);
                    gameObject.SetActive(true);
                }
            }
        }
    }
}