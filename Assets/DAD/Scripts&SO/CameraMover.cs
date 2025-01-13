using UnityEngine;

namespace DAD
{
    public class CameraMover : MonoBehaviour
    {
        public float moveSpeed = 10f;       // Скорость перемещения камеры

        private Camera mainCamera;

        void Start()
        {
            mainCamera = Camera.main;
        }

        void LateUpdate()
        {
            if (Input.GetMouseButton(0))
            {
                MoveCameraByMouseAxis();
            }
        }

        void MoveCameraByMouseAxis()
        {
            float mouseMovement = Input.GetAxis("Mouse X");
            Vector3 cameraMovement = Vector3.right * -mouseMovement * moveSpeed * Time.fixedDeltaTime;
            mainCamera.transform.Translate(cameraMovement, Space.World);
        }
    }
}