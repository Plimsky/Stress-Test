using ScriptableData;
using TMPro;
using UnityEngine;

namespace Managers.UI
{
    public class FpsManager : MonoBehaviour
    {
        [SerializeField] private ConfigProjectData _configuration;

        // Actual FPS
        [SerializeField] private TMP_Text _actualFpsText;
        [SerializeField] private string _actualFpsString =  " Actual : ";

        // Actual FPS
        [SerializeField] private TMP_Text _averageFpsText;
        [SerializeField] private string _averageFpsString =  " Average : ";

        // Actual FPS
        [SerializeField] private TMP_Text _minMaxFpsText;
        [SerializeField] private string _minFpsString =  " Min : ";
        [SerializeField] private string _maxFpsString =  " Max : ";

        // Total Spawn
        [SerializeField] private TMP_Text _totalSpawnText;
        [SerializeField] private string _totalSpawnString =  " Total : ";

        private void Update()
        {
            _actualFpsText.text = _actualFpsString + _configuration.ActualFps;
            _averageFpsText.text = _averageFpsString + _configuration.AverageFps;
            _minMaxFpsText.text = _minFpsString + _configuration.MinFps + " - " + _maxFpsString + _configuration.MaxFps;
            _totalSpawnText.text = _totalSpawnString + _configuration.ActualSpawn;
        }
    }
}