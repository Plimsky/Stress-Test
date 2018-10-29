using UnityEngine;

namespace ScriptableData
{
    [CreateAssetMenu]
    public class ModelSpawnData: ScriptableObject
    {
        [Header("Infos")]
        public string ModelName;
        public string Description;

        [Header("Spawn")]
        public GameObject ModelToSpawn;
        public int TotalToSpawn = 1;
        public float HeightMaxSpawn = 1f;
        public int TotalToSpawnPerFrame = 100;

        [Header("Scale")]
        public float MinScale = 0f;
        public float MaxScale = 1f;
    }
}