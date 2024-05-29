using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] public GameObject gameOverScreen;
        [SerializeField] public GameObject pauseScreen;
        [SerializeField] public GameObject winScreen;

        private static UIManager _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            
            else
            {
                Destroy(gameObject);
            }
        }

        public void ShowGameOverScreen()
        {
            gameOverScreen.SetActive(true);
            pauseScreen.SetActive(false);
            winScreen.SetActive(false);
            
            TimeManager._instance.PauseTime();
        }

        public void ShowPauseScreen()
        {
            pauseScreen.SetActive(true);
            
            TimeManager._instance.PauseTime();
        }

        public void ShowWinScreen()
        {
            gameOverScreen.SetActive(false);
            pauseScreen.SetActive(false);
            winScreen.SetActive(true);
            
            TimeManager._instance.PauseTime();
        }

        public void HidePauseScreen()
        {
            pauseScreen.SetActive(false);
            
            TimeManager._instance.ResumeTime();
        }

        public void HideAllScreens()
        {
            pauseScreen.SetActive(false);
            gameOverScreen.SetActive(false);
            winScreen.SetActive(false);
            
            TimeManager._instance.ResumeTime();
        }
    }
}