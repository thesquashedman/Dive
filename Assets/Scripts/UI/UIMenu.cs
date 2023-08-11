using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Lowscope.Saving;

namespace SlimUI.ModernMenu{
    public class UIManage : MonoBehaviour {

        private Animator CameraObject;

        // campaign button sub menu
        [Header("MENUS")]
        public GameObject mainMenu;
        public GameObject viewport;
        public GameObject playMenu;
        public GameObject exitMenu;

        public enum Theme {custom1, custom2, custom3};
        [Header("THEME SETTINGS")]
        public Theme theme;
        private int themeIndex;
        public ThemedUIData themeController;
    
        [Header("PANELS")]
        public GameObject deadInfo;
        public GameObject reportPanel;

        public static bool isGamePaused = false;

        // Start is called before the first frame update
        void Start()
        {
            EventManager.current.onPlayerDeath += YouDied;
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape)) {
                if (isGamePaused) {
                    ResumeGame();
                }
                else {
                    PauseGame();
                }
            }
        }

        public void AreYouSure(){
			exitMenu.SetActive(true);
			// DisablePlayCampaign();
		}

        public void ReturnMenu(){
			exitMenu.SetActive(false);
			mainMenu.SetActive(true);
		}

        public void QuitGame(){
			#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
			#else
				Application.Quit();
			#endif
		}

        public void PauseGame() {
            isGamePaused = true;
            // Time.timeScale = 0f;
            mainMenu.SetActive(true);
            viewport.SetActive(false);
        }

        public void ResumeGame() {
            isGamePaused = false;
            // Time.timeScale = 1.0f;
            mainMenu.SetActive(false);
            viewport.SetActive(true);
        }

        public void GoToStartMenu() {
            SceneManager.LoadScene("MainMenu");
        }

        public void YouDied() {
            deadInfo.SetActive(true);
            viewport.SetActive(false);
        }

        public void Report() {
            reportPanel.SetActive(true);
        }
    }
}