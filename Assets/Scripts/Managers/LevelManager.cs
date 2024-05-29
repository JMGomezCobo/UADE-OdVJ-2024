using System;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Unity Events")]
        public UnityEvent onWin;
        public UnityEvent onGameOver;
        public UnityEvent onPause;
        public UnityEvent onResume;
        
        public static LevelManager Instance;

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

        private void Start()
        {
            UIManager.Instance.HideAllScreens();
        }

        public void WinGame()
        {
            onWin.Invoke();
        }

        public void GameOver()
        {
            onGameOver.Invoke();
        }

        public void PauseGame()
        {
            onPause.Invoke();
        }

        public void ResumeGame()
        {
            onResume.Invoke();
        }
    }
}