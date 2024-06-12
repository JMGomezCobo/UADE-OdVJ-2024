using System;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [Header("UI │ Screens")]
        public GameObject winScreen;
        public GameObject gameOverScreen;
        public GameObject pauseScreen;
        
        [Header("UI │ Texts")]
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI livesText;

        public static UIManager Instance;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            
            else Destroy(gameObject);
        }
        
        private void Start()
        {
            winScreen.SetActive(false);
            gameOverScreen.SetActive(false);
            pauseScreen.SetActive(false);
        }

        public void ShowWinScreen()
        {
            winScreen.SetActive(true);
        }

        public void ShowGameOverScreen()
        {
            gameOverScreen.SetActive(true);
        }

        public void ShowPauseScreen()
        {
            pauseScreen.SetActive(true);
        }

        public void HidePauseScreen()
        {
            pauseScreen.SetActive(false);
        }

        public void UpdateScore(int score)
        {
            scoreText.text = "Score: " + score;
        }

        public void UpdateLives(int lives)
        {
            livesText.text = "Lives: " + lives;
        }
    }
}