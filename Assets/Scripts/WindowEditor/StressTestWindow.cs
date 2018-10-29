using System;
using System.UI;
using Managers;
using ScriptableData;
using UnityEngine;
using UnityEditor;

namespace WindowEditor
{
    public class StressTestWindow : EditorWindow
    {
        private ConfigProjectData ConfigProject
        {
            get { return _realConfigProject; }
            set
            {
                _realConfigProject = value;

                if (Spawner.Instance != null)
                    Spawner.Instance.Configuration = value;

                if (MenuManager.Instance != null)
                    MenuManager.Instance.ConfigProjectData = value;

                if (SecurityFps.Instance != null)
                    SecurityFps.Instance.Configuration = value;
            }
        }

        private ModelSpawnData SpawnData
        {
            get { return _realSpawnData; }
            set
            {
                _realSpawnData = value;

                if (Spawner.Instance != null)
                    Spawner.Instance.ModelDataSpawn = value;

                if (MenuManager.Instance != null)
                    MenuManager.Instance.ModelDataSpawn = value;
            }
        }

        private ModelSpawnData    _realSpawnData;
        private ConfigProjectData _realConfigProject;

        [MenuItem("Window/Stress Test")]
        public static void InitWindow()
        {
            StressTestWindow window = GetWindow<StressTestWindow>("Stress Test");
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("Main Settings", EditorStyles.boldLabel);
            ConfigProject = (ConfigProjectData) EditorGUILayout.ObjectField("Configuration",
                                                                            ConfigProject,
                                                                            typeof(ConfigProjectData),
                                                                            true);
            if (ConfigProject != null)
            {
                ConfigProject.LimitFpsSecurity = EditorGUILayout.IntSlider("Limit FPS",
                                                                           ConfigProject.LimitFpsSecurity,
                                                                           0,
                                                                           60);

                ConfigProject.MaxSpawn = EditorGUILayout.IntSlider("Max spawn",
                                                                   ConfigProject.MaxSpawn,
                                                                   0,
                                                                   50000);

                ConfigProject.MaxSpawnPerFrame = EditorGUILayout.IntSlider("Max spawn per frame",
                                                                           ConfigProject.MaxSpawnPerFrame,
                                                                           0,
                                                                           1000);

                ConfigProject.MaxHeightSpawn = EditorGUILayout.IntSlider("Max Height per spawn",
                                                                         ConfigProject.MaxHeightSpawn,
                                                                         0,
                                                                         50);
            }

            // Separator
            GUILayout.Space(10f);
            GUILayout.Label("", GUI.skin.horizontalSlider);

            GUILayout.Label("Spawn Settings", EditorStyles.boldLabel);
            SpawnData = (ModelSpawnData) EditorGUILayout.ObjectField("Model",
                                                                     SpawnData,
                                                                     typeof(ModelSpawnData),
                                                                     true);
            GUILayout.Space(10f);
            if (GUILayout.Button("Reset loaded model data"))
            {
                if (SpawnData != null)
                {
                    SpawnData.TotalToSpawn         = 0;
                    SpawnData.HeightMaxSpawn       = 0;
                    SpawnData.TotalToSpawnPerFrame = 1;
                }
            }

            // Separator
            GUILayout.Space(10f);
            GUILayout.Label("", GUI.skin.horizontalSlider);

            GUILayout.Label("Scene Settings", EditorStyles.boldLabel);
            if (GUILayout.Button("Clean Scene"))
            {
                var gameObjects = FindObjectsOfType<GameObject>();

                foreach (var go in gameObjects)
                {
                    if (go.name.Contains("Clone"))
                        DestroyImmediate(go);
                }
            }

        }
    }
}