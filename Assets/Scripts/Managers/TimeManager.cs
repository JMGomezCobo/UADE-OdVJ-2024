using UnityEngine;

namespace Managers
{
    public class TimeManager : MonoBehaviour
    {
        public static TimeManager _instance;

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

        public void PauseTime()
        {
            Time.timeScale = 0f;
        }

        public void ResumeTime()
        {
            Time.timeScale = 1f;
        }
    }
}
