using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public int lives = 3;
        
        [Header("Unity Events")]
        public UnityEvent onGameWin;
        public UnityEvent onGameOver;
        public UnityEvent onPause;
        public UnityEvent onResume;
        
        private int _score;
        private int _totalBricks;
        
        public static GameManager Instance;

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
            _totalBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
            
            UIManager.Instance.UpdateLives(3);
        }
        
        private void OnEnable()
        {
            CustomUpdateManager.Instance.SubscribeToUpdate(PauseGame);
        }


        public void AddScore(int points)
        {
            _score += points;
            UIManager.Instance.UpdateScore(_score);
        }
        
        public void AddLife(int amount)
        {
            lives += amount;
            UIManager.Instance.UpdateLives(lives);
        }

        public void LoseLife()
        {
            lives--;
            UIManager.Instance.UpdateLives(lives);

            if (lives <= 0)
            {
                onGameOver.Invoke();
            }
        }

        public void BrickDestroyed()
        {
            _totalBricks--;
            
            if (_totalBricks <= 0)
            {
                onGameWin.Invoke();
            }
        }

        public void PauseGame()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            
            onPause.Invoke();
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            onResume.Invoke();
            Time.timeScale = 1;
        }
    }
}