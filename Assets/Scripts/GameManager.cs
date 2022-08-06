using System;
using UnityEngine;

namespace Engine
{
    public class GameManager: MonoBehaviour
    {
        public static GameManager GameManagerInstance { get; private set; }
        private bool _FirstAwake;
        private void Awake()
        {
            if (GameManagerInstance != null && GameManagerInstance != this)
                throw new Exception("Not allowed creating new GameManager copy");

            GameManagerInstance = this;

            _FirstAwake = true;
        }

        private void Update()
        {
            if (_FirstAwake)
            {
                HerbalismManager.Instance.LoadData();
                _FirstAwake = false;
            }
        }
    }
}