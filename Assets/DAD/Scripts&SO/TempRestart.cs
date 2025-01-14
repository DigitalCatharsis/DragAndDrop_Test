using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DAD 
{
    public class TempRestart : MonoBehaviour
    {
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

