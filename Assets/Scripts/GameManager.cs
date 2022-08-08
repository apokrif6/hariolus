using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Engine
{
    public class GameManager: MonoBehaviour
    {
        public static GameManager GameManagerInstance { get; private set; }
        public GameObject playerController;
        
        private bool _firstAwake;
        private void Awake()
        {
            if (GameManagerInstance != null && GameManagerInstance != this)
                throw new Exception("Not allowed creating new GameManager copy");

            GameManagerInstance = this;

            _firstAwake = true;
        }

        private void Update()
        {
            if (_firstAwake)
            {
                HerbalismManager.Instance.LoadData();
                _firstAwake = false;
            }
        }
    }
}