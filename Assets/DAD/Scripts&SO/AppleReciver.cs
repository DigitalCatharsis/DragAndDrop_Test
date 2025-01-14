using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DAD
{
    public class AppleReciver : MonoBehaviour
    {
        private BoxCollider2D _collider;
        private int _layerToCheck;
        private int _layerToSet;
        private int _appleCount = 0;

        public event Action OnAppleDetected;
        public event Action OnAllApplesPlaced;

        private void Start()
        {
            int layerNumber = 3;
            _layerToCheck = 1 << layerNumber;

            layerNumber = 2;
            _layerToSet = 1 << layerNumber;

            _collider = GetComponentInChildren<BoxCollider2D>();
        }

        private void Update()
        {
            CheckForApples();

            if (_appleCount == 3)
            {
                OnAllApplesPlaced?.Invoke();
            }
        }

        private void CheckForApples()
        {
            var colliders = Physics2D.OverlapAreaAll(_collider.bounds.min, _collider.bounds.max, _layerToCheck);

            foreach (var col in colliders)
            {
                _appleCount++;
                col.gameObject.layer = _layerToSet;
                col.transform.SetParent(transform);
                OnAppleDetected?.Invoke();
            }
        }
    }
}