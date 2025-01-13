using UnityEngine;

namespace DAD
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private float _moveSpeedPointer = 10f;       // Скорость перемещения камеры
        [SerializeField] private float _moveSpeedTouch = 1f;       // Скорость перемещения камеры

        private Camera mainCamera;

        void Start()
        {
            mainCamera = Camera.main;
        }

        void LateUpdate()
        {
#if UNITY_EDITOR
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
            float mouseMovement = Input.GetAxis("Mouse X");
            Vector3 cameraMovement = Vector3.right * -mouseMovement * _moveSpeedPointer * Time.fixedDeltaTime;
            mainCamera.transform.Translate(cameraMovement, Space.World);
        }
        void MoveCameraByTouch(Vector2 deltaPosition)
        {
            Vector3 cameraMovement = new Vector3(-deltaPosition.x, 0, 0) * _moveSpeedTouch * Time.fixedDeltaTime;
            mainCamera.transform.Translate(cameraMovement, Space.World);
        }
    }
}