using System.Collections.Generic;
using System.Reflection;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.UI;

namespace System.UI
{
    public class SpawnConfigMenuState : AUIState
    {
        private PopupWindowAnimation _popupWindowAnimation;
        private readonly Dictionary<string, TMP_Text> _dictionarySliderValueTexts = new Dictionary<string, TMP_Text>();

        public SpawnConfigMenuState(MenuManager menuManager) : base(menuManager)
        {
            _menuManager.ModelDataSpawn.TotalToSpawn = 0;
            _menuManager.ModelDataSpawn.TotalToSpawnPerFrame = 1;
        }

        public override void Setup(Transform root)
        {
            base.Setup(root);
            _popupWindowAnimation = root.gameObject.GetComponent<PopupWindowAnimation>();
            SpawnHeightSlider(null, 0f);
            TranslateToggle(false);
            RotateToggle(false);
        }

        public override void Disable()
        {
            if (_popupWindowAnimation != null)
                _popupWindowAnimation.CloseWindow();
            else
                base.Disable();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        private void CloseButton()
        {
            _menuManager.RemoveLastState(UIStateEnum.ACTIONSMENU);
        }

        private void SpawnTotalSlider(Slider sliderRef, float value)
        {
            if (sliderRef != null && (int) sliderRef.maxValue != _menuManager.ConfigProjectData.MaxSpawn)
                sliderRef.maxValue = _menuManager.ConfigProjectData.MaxSpawn;

            String methodName = MethodBase.GetCurrentMethod().Name + "ValueText";
            UpdateSlider(value, methodName);

            _menuManager.ModelDataSpawn.TotalToSpawn = (int) value;
        }

        private void SpawnPerFrameSlider(Slider sliderRef, float value)
        {
            if (sliderRef != null && (int) sliderRef.maxValue != _menuManager.ConfigProjectData.MaxSpawnPerFrame)
                sliderRef.maxValue = _menuManager.ConfigProjectData.MaxSpawnPerFrame;

            String methodName = MethodBase.GetCurrentMethod().Name + "ValueText";
            UpdateSlider(value, methodName);

            _menuManager.ModelDataSpawn.TotalToSpawnPerFrame = (int) value;
        }

        private void SpawnHeightSlider(Slider sliderRef, float value)
        {
            if (sliderRef != null && (int) sliderRef.maxValue != _menuManager.ConfigProjectData.MaxHeightSpawn)
                sliderRef.maxValue = _menuManager.ConfigProjectData.MaxHeightSpawn;

            String methodName = MethodBase.GetCurrentMethod().Name + "ValueText";
            UpdateSlider(value, methodName);

            _menuManager.ModelDataSpawn.HeightMaxSpawn = (int) value;
        }

        private void TranslateToggle(bool value)
        {
            _menuManager.ConfigProjectData.randomTranslate = value;
        }

        private void RotateToggle(bool value)
        {
            _menuManager.ConfigProjectData.randomRotation = value;
        }

        private void UpdateSlider(float value, string methodName)
        {
            if (!_dictionarySliderValueTexts.ContainsKey(methodName))
            {
                GameObject gameObjectToFind = GameObject.Find(methodName);
                TMP_Text tmpText;

                if (gameObjectToFind != null && (tmpText = gameObjectToFind.GetComponent<TMP_Text>()) != null)
                    _dictionarySliderValueTexts.Add(gameObjectToFind.name, tmpText);
            }

            if (_dictionarySliderValueTexts.ContainsKey(methodName))
                _dictionarySliderValueTexts[methodName].text = ((int) value).ToString();
        }
    }
}