using System;
using UnityEngine;

namespace Engine
{
    public delegate void DelegateChangeTime(int currentValue);
    
    public class DayNightSystem: MonoBehaviour
    {
        public static DayNightSystem Instance { get; private set; }
        public Light Sun;
        public int DayDuration = 6;
        public int NightDuration = 6;
        public float VirtualTime = 12;

        private float _secondCounter;
        private const float _computationInterval = 0.05f; //in seconds
        
        public bool IsDay
        {
            get { return VirtualTime >= 7 && VirtualTime < 19;  }
        }

        public int Hour
        {
            get { return (int) VirtualTime; }
        }

        public int Minute
        {
            get { return (int) ((VirtualTime - Hour) * 60); }
        }

        public event DelegateChangeTime ChangeMinute;
        public event DelegateChangeTime ChangeHour;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                throw new Exception("Not allowed creating new DayNightSystem copy");
            Instance = this;
        }
        
        void Start()
        {            
            if (Sun == null)
                Debug.LogError("Field Sun in DayNightSystem is null");
            _secondCounter = 0;
        }

        void Update()
        {
            _secondCounter -= Time.deltaTime;
            if (_secondCounter < 0)
            {
                _secondCounter = _computationInterval;
                TimeLapse();
            }
        }

        private void TimeLapse()
        {
            int oldHour = Hour;
            int oldMinute = Minute;

            if (IsDay)
                VirtualTime += 1f / (DayDuration * 60 / (_computationInterval * 12));
            else
                VirtualTime += 1f / (NightDuration * 60 / (_computationInterval * 12));

            if (VirtualTime >= 24)
                VirtualTime -= 24;
            
            if (oldMinute != Minute)
                MinuteLapse();
            if (oldHour != Hour)
                HourLapse();

            float dayPart = (VirtualTime - 7) / 24;
            float sunAngle = dayPart * 360;
            Vector3 intermediatePoint = new Vector3(0, 0, 0);
            Sun.transform.position = intermediatePoint + Quaternion.Euler(0, 0, sunAngle) * (10 * Vector3.right);
            Sun.transform.LookAt(intermediatePoint);
        }

        private void MinuteLapse()
        {
            ChangeMinute?.Invoke(Minute);
        }

        private void HourLapse()
        {
            ChangeHour?.Invoke(Hour);
        }
    }
}