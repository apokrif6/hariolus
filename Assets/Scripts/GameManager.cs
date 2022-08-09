using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Engine
{
    public class GameManager: MonoBehaviour
    {
        public static GameManager GameManagerInstance { get; private set; }
        public GameObject playerController;
        public GameObject[] stationaryPatientsPlaces;
        
        private bool _firstAwake;
        private GameObject _patientPrefab;
        private void Awake()
        {
            if (GameManagerInstance != null && GameManagerInstance != this)
                throw new Exception("Not allowed creating new GameManager copy");

            GameManagerInstance = this;

            _firstAwake = true;
            _patientPrefab = null;
        }

        private void Update()
        {
            if (_firstAwake)
            {
                DayNightSystem.Instance.ChangeHour += HourLapse;
                HerbalismManager.Instance.LoadData();
                _firstAwake = false;
            }
        }

        private void HourLapse(int Hour)
        {
            foreach (GameObject patientPlace in stationaryPatientsPlaces)
            {
                if (patientPlace.transform.childCount == 0 && UnityEngine.Random.Range(0, 2) == 0)
                {
                    CreatePatient(patientPlace);
                }
            }
        }

        private void CreatePatient(GameObject creationPlace)
        {
            if (_patientPrefab == null)
                _patientPrefab = Resources.Load<GameObject>("Prefabs/Patient");

            if (_patientPrefab == null)
                Debug.LogError("Patient Prefab is missing");
            else
            {
                GameObject newPatient = Instantiate(_patientPrefab, Vector3.zero, Quaternion.identity, creationPlace.transform);
                newPatient.transform.localPosition = Vector3.zero;
            }

        }
    }
}