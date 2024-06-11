using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        public int lives = 3;
        public int score;

        [ReadOnly] public int totalBricks;
        
        [Header("Unity Events")]
        public UnityEvent onGameWin;
        public UnityEvent onGameOver;
        public UnityEvent onPause;
        public UnityEvent onResume;

        public UnityEvent<int> onLivesChanged;
        public UnityEvent<int> onScoreChanged;

        public static LevelManager Instance;

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
            onLivesChanged.Invoke(lives);
            onScoreChanged.Invoke(score);

            totalBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
        }
        
        private void OnEnable()
        {
            CustomUpdateManager.Instance.SubscribeToUpdate(PauseGame);
        }

        public void AddScore(int points)
        {
            score += points;
            onScoreChanged.Invoke(score);
        }

        public void LoseLife()
        {
            lives--;
            onLivesChanged.Invoke(lives);

            if (lives <= 0)
            {
                onGameOver.Invoke();
            }
        }

        public void BrickDestroyed()
        {
            totalBricks--;
            
            if (totalBricks <= 0)
            {
                onGameWin.Invoke();
            }
        }

        public void PauseGame()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                onPause.Invoke();
                Time.timeScale = 0;
            }
        }

        public void ResumeGame()
        {
            onResume.Invoke();
            Time.timeScale = 1;
        }
    }
}