using System;
using Extras;
using UnityEngine;
using UnityEngine.UI;

namespace Engine
{
    public class PatientComponent: MonoBehaviour
    {
        private const float DistanceToShow = 5;
        private const float CheckDistanceFrequency = 0.5f;
        private float _secondCounter;
        
        public Patient PrivatePatient { get; private set; }
        private Text _patientInfo;

        private void Awake()
        {
            Canvas patientUI = GetComponentInChildren<Canvas>();
            if (patientUI != null)
            {
                _patientInfo = patientUI.GetComponentInChildren<Text>();
            }

            _secondCounter = 0;
        }

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

            _secondCounter -= Time.deltaTime;

            if (_secondCounter < 0)
            {
                _secondCounter = CheckDistanceFrequency;

                if ((GameManager.GameManagerInstance.playerController.transform.position - transform.position)
                    .magnitude < DistanceToShow)
                {
                    _patientInfo.transform.parent.gameObject.SetActive(true);
                }
                else
                {
                    _patientInfo.transform.parent.gameObject.SetActive(false);
                }
            }
        }

        private void HourLapse(int hour)
        {
            if (PrivatePatient.IsAlive)
            {
                PrivatePatient.HourlyEventHandling();

                if (_patientInfo != null)
                {
                    _patientInfo.text = PrivatePatient.DebugEvents();
                }

                if (!PrivatePatient.IsAlive)
                {
                    if (PrivatePatient.DeathCause != "")
                    {
                        Debug.Log(string.Format("{0} {1} {2} {3}",
                            StringsTranslator.EnterString("Patient"),
                            PrivatePatient.IDPatient,
                            StringsTranslator.EnterString("DiedViolently"),
                            PrivatePatient.DeathCause));
                    }
                    else
                    {
                        Debug.Log(string.Format("{0} {1} {2}",
                            StringsTranslator.EnterString("Patient"),
                            PrivatePatient.IDPatient,
                            StringsTranslator.EnterString("Died")));
                    }
                }

                if (PrivatePatient.IsHealthy)
                {
                    Debug.Log(string.Format("{0} {1} {2}",
                        StringsTranslator.EnterString("Patient"),
                        PrivatePatient.IDPatient,
                        StringsTranslator.EnterString("Recovered")));
                }

                if (PrivatePatient.IsHealthy || !PrivatePatient.IsAlive)
                {
                    DayNightSystem.Instance.ChangeHour -= HourLapse;
                    Destroy(gameObject);
                }
            }
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