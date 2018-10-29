using ScriptableData;
using UnityEngine;
using UnityEngine.Analytics;

namespace Managers
{
    public class SecurityFps : MonoBehaviour
    {
        public static SecurityFps Instance;
        public ConfigProjectData Configuration;

        [SerializeField] private float _timeToWaitBeforeStartCheckingFps = 0.8f;
        [SerializeField] private float _totalTimeLowFPS = 2f;

        private float _timeLowFPS;
        private bool _canCheckFps;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        private void Start()
        {
            Invoke("StartCheck", _timeToWaitBeforeStartCheckingFps);
        }

        private void Update()
        {
            if (_canCheckFps && Configuration.ActualFps <= Configuration.LimitFpsSecurity)
                _timeLowFPS += Time.deltaTime;

            if (_timeLowFPS > _totalTimeLowFPS)
                Quit();
        }

        private void StartCheck()
        {
            _canCheckFps = true;
        }

        private static void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}