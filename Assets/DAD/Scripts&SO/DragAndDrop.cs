using UnityEngine;

namespace DAD
{
    public class DragAndDrop : MonoBehaviour
    {
        private Vector3 _offset;
        private bool _isDragging = false;
        private float _depthOffset = 0f;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Boom");
        }

        void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPosition = touch.position;

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touchPosition), Vector2.zero);
                        if (hit.collider != null && hit.collider.gameObject == gameObject)
                        {
                            _isDragging = true;
                            _offset = transform.position - Camera.main.ScreenToWorldPoint(touchPosition);
                            _depthOffset = transform.position.z;
                        }
                        break;
                    case TouchPhase.Moved:
                        if (_isDragging)
                        {
                            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(touchPosition) + _offset;
                            transform.position = new Vector3(cursorPosition.x, cursorPosition.y, _depthOffset);
                        }
                        break;
                    case TouchPhase.Ended:
                        if (_isDragging)
                        {
                            _isDragging = false;
                            GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionY;

                            // Устанавливаем глубину в зависимости от положения касания
                            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touchPosition);
                            transform.position = new Vector3(transform.position.x, transform.position.y, touchPos.z);
                        }
                        break;
                }
            }
        }
    }

}