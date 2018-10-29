using CodeStage.AdvancedFPSCounter;
using ScriptableData;
using UnityEngine;

namespace Utils
{
    public class FpsCounter : MonoBehaviour
    {
        [SerializeField] private ConfigProjectData _configuration;
        private bool _canUpdateMinValue;

        private void Start()
        {
            Application.targetFrameRate = 300;

            // will add AFPSCounter to the scene if it not exists
            // you don't need to call it if you already have AFPSCounter in the scene
            var newCounterInstance = AFPSCounter.AddToScene(false);

            // you also may get the instance at any time
            // using AFPSCounter.Instance property
            newCounterInstance.fpsCounter.OnFPSLevelChange += OnFPSLevelChanged;
            newCounterInstance.OperationMode = OperationMode.Background;

            _configuration.CurrentFpsLevel = FPSLevel.Normal;

            Invoke("CanUpdateMinValue", 1f);
        }

        private void Update()
        {
            _configuration.ActualFps = AFPSCounter.Instance.fpsCounter.LastValue;
            _configuration.AverageFps = AFPSCounter.Instance.fpsCounter.LastAverageValue;
            _configuration.MaxFps = AFPSCounter.Instance.fpsCounter.LastMaximumValue;

            if (_canUpdateMinValue)
                _configuration.MinFps = AFPSCounter.Instance.fpsCounter.LastMinimumValue;
            else
                _configuration.MinFps = AFPSCounter.Instance.fpsCounter.LastMaximumValue;
        }

        private void OnFPSLevelChanged(FPSLevel newLevel)
        {
            _configuration.CurrentFpsLevel = newLevel;
        }

        private void CanUpdateMinValue()
        {
            _canUpdateMinValue = true;
            AFPSCounter.Instance.fpsCounter.ResetMinMax();
        }
    }
}