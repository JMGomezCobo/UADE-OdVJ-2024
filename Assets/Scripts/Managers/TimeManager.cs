using UnityEngine;

namespace Managers
{
    public class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance;

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
            LevelManager.Instance.onPause.AddListener(PauseGame);
            LevelManager.Instance.onResume.AddListener(ResumeGame);
        }

        private void OnDisable()
        {
            if (GameManager.Instance == null) return;
            
            LevelManager.Instance.onPause.RemoveListener(PauseGame);
            LevelManager.Instance.onResume.RemoveListener(ResumeGame);
        }

        public void PauseGame()
        {
            Time.timeScale = 0f;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
        }
    }
}

