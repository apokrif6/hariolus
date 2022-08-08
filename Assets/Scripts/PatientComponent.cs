using System;
using UnityEngine;

namespace Engine
{
    public class PatientComponent: MonoBehaviour
    {
        public Patient PrivatePatient { get; private set; }

        private void Start()
        {
            PrivatePatient = new Patient();
        }

        private void Update()
        {
            var position = GameManager.GameManagerInstance.playerController.transform.position;
            
            Vector3 lookTarget = new Vector3(position.x, transform.position.y, position.z);
            
            Quaternion lookDirection = Quaternion.LookRotation(lookTarget - transform.position, transform.up);
            transform.rotation = lookDirection;
        }
    }
}