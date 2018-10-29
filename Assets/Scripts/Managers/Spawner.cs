using System.Collections.Generic;
using ScriptableData;
using UnityEditor.VersionControl;
using UnityEngine;
using Utils;

namespace Managers
{
    [ExecuteInEditMode]
    public class Spawner : MonoBehaviour
    {
        public static Spawner           Instance;
        public        ConfigProjectData Configuration;
        public        ModelSpawnData    ModelDataSpawn;

        [SerializeField]
        private Collider _spawnZone;

        private readonly Stack<GameObject> _instances = new Stack<GameObject>();

        // Todo events to make for other scripts to know what they can do and what they cannot do
        // Example : dont check perform during instancing (option?)

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        private void Start()
        {
            var gameObjects = FindObjectsOfType<GameObject>();

            foreach (var go in gameObjects)
            {
#if UNITY_EDITOR
                if (go.name.Contains("Clone"))
                    DestroyImmediate(go);
#else
                if (go.name.Contains("Clone"))
                    Destroy(go);
#endif
            }
        }

        private void Update()
        {
            if (ModelDataSpawn != null)
            {
                for (int i = 0; i < ModelDataSpawn.TotalToSpawnPerFrame; i++)
                {
                    var spawnPosition = new Vector3(Random.Range(_spawnZone.bounds.min.x, _spawnZone.bounds.max.x),
                                                    Random.Range(_spawnZone.bounds.min.y,
                                                                 ModelDataSpawn.HeightMaxSpawn != 0f
                                                                     ? ModelDataSpawn.HeightMaxSpawn
                                                                     : _spawnZone.bounds.min.y),
                                                    Random.Range(_spawnZone.bounds.min.z, _spawnZone.bounds.max.z));

                    var rotation = Configuration.randomRotation ? Random.rotation : Quaternion.identity;

                    if (_instances.Count < ModelDataSpawn.TotalToSpawn)
                    {
                        GameObject instance = Instantiate(ModelDataSpawn.ModelToSpawn, spawnPosition, rotation);

                        if (Configuration.randomTranslate)
                        {
                            float     randomValue     = Random.value;
                            Translate scriptReference = instance.AddComponent<Translate>();
                            scriptReference.Direction = new Vector3(
                                                                    Random.Range(-randomValue, randomValue),
                                                                    Random.Range(-randomValue, randomValue),
                                                                    Random.Range(-randomValue, randomValue)
                                                                   );
                        }

                        if (Configuration.randomScale)
                        {
                            float randomScaleValue = Random.Range(ModelDataSpawn.MinScale, ModelDataSpawn.MaxScale);
                            instance.transform.localScale = new Vector3(
                                                                        randomScaleValue,
                                                                        randomScaleValue,
                                                                        randomScaleValue
                                                                       );
                        }

                        _instances.Push(instance);
                    }
                    else if (_instances.Count > ModelDataSpawn.TotalToSpawn)
                    {
#if UNITY_EDITOR
                        if (_instances.Count > 0)
                            DestroyImmediate(_instances.Pop());
#else
                    if (_instances.Count > 0)
                        Destroy(_instances.Pop());
#endif
                    }
                }

                Configuration.ActualSpawn = _instances.Count;
            }
        }
    }
}