using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DAD
{
    public class DragManager : MonoBehaviour
    {
        public bool HasSelectedItem => _currentRigidbody != null;

        private AppleReciver _appleReceiver;
        private Rigidbody2D _currentRigidbody;
        private Camera _camera;
        private Vector3 _offset;

        private bool _forceStopDrag = false;

        void OnEnable()
        {
            // Инициализация переменной appleReceiver
            _appleReceiver = FindObjectOfType<AppleReciver>();
            _appleReceiver.OnAppleDetected += OnAppleReceivedHandler;
            //_appleReceiver.OnAllApplesPlaced += OnAllApplesReceivedHandler;
        }

        private void OnAppleReceivedHandler()
        {
            _forceStopDrag = true;
        }

        void OnDisable()
        {
            _appleReceiver.OnAppleDetected -= OnAppleReceivedHandler;
            //_appleReceiver.OnAllApplesPlaced -= OnAllApplesReceivedHandler;
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0)) // Проверка нажатия левой кнопки мыши
            {
                SelectObject();
            }

            if (Input.GetMouseButtonUp(0))
            {
                DropObject();
            }
#endif
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    SelectObject();
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    DropObject();
                }
            }

            if (!_forceStopDrag)
            {
                DragObject();
            }
        }

        private void SelectObject()
        {
            var camRay = _camera.ScreenToWorldPoint(Input.mousePosition);

            var hit = Physics2D.Raycast(camRay, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("PhObjects"));

            if (hit.collider != null)
            {
                _currentRigidbody = hit.collider.GetComponent<Rigidbody2D>();
                _currentRigidbody.gravityScale = 0;
                _offset = _currentRigidbody.transform.position - camRay;
                _currentRigidbody.bodyType = RigidbodyType2D.Kinematic;
            }
        }

        private void DragObject()
        {
            if (_currentRigidbody != null)
            {
                var mousePos = Input.mousePosition;
                var camRay = _camera.ScreenToWorldPoint(mousePos);
                _currentRigidbody.MovePosition(camRay + _offset);
            }
        }

        private void DropObject()
        {
            _forceStopDrag = false;

            if (_currentRigidbody == null)
            {
                return;
            }

            // Получаем размеры коллайдера яблока
            var collider = _currentRigidbody.GetComponent<BoxCollider2D>();
            if (collider == null)
            {
                Debug.LogError("BoxCollider2D not found on object!");
                return;
            }

            // Нижняя точка коллайдера
            var bottomCenter = new Vector2(collider.bounds.center.x, collider.bounds.min.y);

            // Пускаем луч вперед от нижней точки коллайдера
            var hit = Physics2D.Raycast(bottomCenter, Camera.main.transform.forward, 0.1f, LayerMask.GetMask("Surfaces"));

            if (hit)
            {
                // Нижняя часть коллайдера пересекается с поверхностью, оставляем яблоко на месте
                _currentRigidbody.bodyType = RigidbodyType2D.Kinematic;
            }
            else
            {
                // Включаем гравитацию обратно
                _currentRigidbody.gravityScale = 1;
                _currentRigidbody.bodyType = RigidbodyType2D.Dynamic;
            }

            _offset = Vector3.zero;
            _currentRigidbody = null;
        }
    }

}