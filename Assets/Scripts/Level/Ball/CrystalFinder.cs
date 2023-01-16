using System;
using UnityEngine;

namespace GamePlay.Level.PickUps
{
    public class CrystalFinder : MonoBehaviour
    {
        public event Action<Crystal> CrystalPickedUp;

        private void OnTriggerEnter(Collider other)
        {
            Crystal crystal = other.GetComponent<Crystal>();

            if (crystal == null)
                return;

            crystal.PickUp();
            CrystalPickedUp?.Invoke(crystal);
        }

        private void OnDestroy()
        {
            CrystalPickedUp = null;
        }
    }
}