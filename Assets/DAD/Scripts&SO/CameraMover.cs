using UnityEngine;

namespace DAD
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private float _moveSpeedPointer = 10f;       // Скорость перемещения камеры
        [SerializeField] private float _moveSpeedTouch = 1f;       // Скорость перемещения камеры

        [SerializeField] private float _minCameraX = -14.25f;       // Скорость перемещения камеры
        [SerializeField] private float _maxCameraX = 14.0763f;       // Скорость перемещения камеры


        private Camera mainCamera;

        void Start()
        {
            mainCamera = Camera.main;
        }

        void LateUpdate()
        {
#if UNITY_EDITOR
            //does not work with uniy remote lol
            if (Input.GetMouseButton(0)
                && !GameLogic.Instance.dragManager.HasSelectedItem)
            {
                MoveCameraByMouseAxis();
            }
#endif
            if (Input.touchCount > 0 && !GameLogic.Instance.dragManager.HasSelectedItem)
            {
                var touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    MoveCameraByTouch(touch.deltaPosition);
                }
            }
        }

        void MoveCameraByMouseAxis()
        {
            var mouseMovement = Input.GetAxis("Mouse X");
            var cameraMovement = Vector3.right * -mouseMovement * _moveSpeedPointer * Time.fixedDeltaTime;

            cameraMovement = CheckAndSetXPosition(cameraMovement);

            mainCamera.transform.Translate(cameraMovement, Space.World);
        }

        private Vector3 CheckAndSetXPosition(Vector3 cameraMovement)
        {
            var currentPosition = mainCamera.transform.position;            
            var newPosition = currentPosition + cameraMovement;

            newPosition.x = Mathf.Clamp(newPosition.x, _minCameraX, _maxCameraX);
                        
            return newPosition - currentPosition;
        }

        void MoveCameraByTouch(Vector2 deltaPosition)
        {
            var cameraMovement = new Vector3(-deltaPosition.x, 0, 0) * _moveSpeedTouch * Time.fixedDeltaTime;
            cameraMovement = CheckAndSetXPosition(cameraMovement);

            mainCamera.transform.Translate(cameraMovement, Space.World);
        }
    }
}