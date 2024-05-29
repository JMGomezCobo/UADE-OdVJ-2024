using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        public GameObject winPanel;
        public GameObject gameOverPanel;
        public GameObject pausePanel;
    
        public static UIManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            LevelManager.Instance.onWin.AddListener(ShowWinScreen);
            LevelManager.Instance.onGameOver.AddListener(ShowGameOverScreen);
            LevelManager.Instance.onPause.AddListener(ShowPauseScreen);
            LevelManager.Instance.onResume.AddListener(HidePauseScreen);
        }

        private void OnDisable()
        {
            if (GameManager.Instance == null) return;
        
            LevelManager.Instance.onWin.RemoveListener(ShowWinScreen);
            LevelManager.Instance.onGameOver.RemoveListener(ShowGameOverScreen);
            LevelManager.Instance.onPause.RemoveListener(ShowPauseScreen);
            LevelManager.Instance.onResume.RemoveListener(HidePauseScreen);
        }

        public void ShowWinScreen()
        {
            winPanel.SetActive(true);
            gameOverPanel.SetActive(false);
            pausePanel.SetActive(false);
        }

        public void ShowGameOverScreen()
        {
            gameOverPanel.SetActive(true);
            winPanel.SetActive(false);
            pausePanel.SetActive(false);
        }

        public void ShowPauseScreen()
        {
            pausePanel.SetActive(true);
        }

        public void HidePauseScreen()
        {
            pausePanel.SetActive(false);
        }
        
        public void HideAllScreens()
        {
            winPanel.SetActive(false);
            gameOverPanel.SetActive(false);
            pausePanel.SetActive(false);
        }
    }
}