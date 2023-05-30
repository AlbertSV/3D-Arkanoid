using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEditor;

namespace Arkanoid
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject mainMenuUI;


        public void PlayGame()
        {
            StartCoroutine(WaitForAnimation());
            
        }

        public void QuitGame()
        {
            Application.Quit();

#if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isPlaying)
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }
#endif
        }

        private IEnumerator WaitForAnimation()
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}