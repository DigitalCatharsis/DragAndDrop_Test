using UnityEngine;
using UnityEngine.SceneManagement;

namespace Angry_Girls
{
    public class MenuButtonController : MonoBehaviour
    {
        [SerializeField] private GameObject _mainmenu;
        [SerializeField] private GameObject _options;

        public void SelectButton(GameObject sender)
        {
            var animator = sender.GetComponentInParent<Animator>();
            animator.SetBool("selected", true);
        }
        public void DisselectButton(GameObject sender)
        {
            var animator = sender.GetComponentInParent<Animator>();
            animator.SetBool("selected", false);
        }

        public void OpenOptionMenu()
        {
            _mainmenu.SetActive(false);
            _options.SetActive(true);
        }

        public void ReturnToMainMenu(GameObject self)
        {
            self.SetActive(false);
            _mainmenu.SetActive(true);
        }

        public void ExecuteNewGame(GameObject sender)
        {
            var animator = sender.GetComponentInParent<Animator>();
            animator.SetBool("selected", false);
            animator.SetBool("pressed", true);
            SceneManager.LoadScene(1);
        }

        public void ExitGame()
        {
        #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
        #else
                        Application.Quit();
        #endif
        }
    }
}
