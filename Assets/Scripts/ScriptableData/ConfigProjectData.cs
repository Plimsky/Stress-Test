using CodeStage.AdvancedFPSCounter;
using UnityEngine;

namespace ScriptableData
{
    [CreateAssetMenu]
    public class ConfigProjectData : ScriptableObject
    {
        [Header("FPS configuration")]
        public int LimitFpsSecurity = 10;

        [Header("Spawn configuration")]
        public int MaxSpawn = 10000;
        public int MaxSpawnPerFrame = 200;
        public int MaxHeightSpawn = 25;

        [Header("Manipulation configuration")]
        public bool randomRotation;
        public bool randomScale;
        public bool randomTranslate;

        [HideInInspector] public int ActualFps;
        [HideInInspector] public int AverageFps;
        [HideInInspector] public int MinFps;
        [HideInInspector] public int MaxFps;
        [HideInInspector] public FPSLevel CurrentFpsLevel;

        [HideInInspector] public int ActualSpawn;
    }
}