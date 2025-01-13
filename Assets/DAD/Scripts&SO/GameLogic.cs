using UnityEngine;

namespace DAD
{
    public class GameLogic : MonoBehaviour
    {
        public static GameLogic Instance { get; private set; }
        public DragManager dragManager;
        public CameraMover cameraMover;

        private void OnEnable()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }

            Instance = this;

            dragManager = GetComponentInChildren<DragManager>();
            cameraMover = GetComponentInChildren<CameraMover>();

        }
    }
}