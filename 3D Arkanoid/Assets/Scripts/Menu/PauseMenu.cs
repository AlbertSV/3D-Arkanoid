using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Arkanoid
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool gameIsPaused = false;
        public GameObject pauseMenuUI;
        private bool isOpen;
        private Animator animator;

        private void Start()
        {
            animator = pauseMenuUI.GetComponent<Animator>();
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(gameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        public void Resume()
        { 
            animator.SetBool("Open", false);
            StartCoroutine(WaitForAnimation());
            Time.timeScale = 1f;
            gameIsPaused = false;
            StopCoroutine(WaitForAnimation());
            
        }

        private void Pause()
        {
            pauseMenuUI.SetActive(true);
            animator.SetBool("Open", true);
            Time.timeScale = 0f;
            gameIsPaused = true;
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
            pauseMenuUI.SetActive(false);
        }
    }
}