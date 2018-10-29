using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utils.UI;

namespace System.UI
{
    // ReSharper disable once InconsistentNaming
    public abstract class AUIState
    {
        protected readonly MenuManager _menuManager;
        protected Transform _rootTransform;

//        private List<GameObject> _listUIElement = new List<GameObject>();

        public AUIState(MenuManager menuManager)
        {
            if (menuManager == null) throw new ArgumentNullException("menuManager");
            _menuManager = menuManager;
        }

        public virtual void Setup(Transform root)
        {
            if (_rootTransform == null)
                _rootTransform = root;

//            foreach (Transform child in _rootTransform)
//            {
//                _listUIElement.Add(child.gameObject);
//            }

            BuildLinksUi(_rootTransform);
        }

        private void BuildLinksUi(Transform root)
        {
            foreach (Transform child in root)
            {
                GameObject uiElementGameObject = child.gameObject;
                Button button = uiElementGameObject.GetComponent<Button>();
                Slider slider = uiElementGameObject.GetComponent<Slider>();
                Toggle toggle = uiElementGameObject.GetComponent<Toggle>();
                MethodInfo methodInfo = GetType().GetMethod(uiElementGameObject.name,
                    BindingFlags.NonPublic | BindingFlags.Instance);

                if (button != null && methodInfo != null)
                {
                    //UnityAction action = () => methodInfo.Invoke(this, null);
                    button.onClick.AddListener(() => methodInfo.Invoke(this, null));
                }

                if (slider != null && methodInfo != null)
                {
                    slider.onValueChanged.AddListener(value => { methodInfo.Invoke(this, new object[] {slider, value}); });
                }

                if (toggle != null && methodInfo != null)
                {
                    toggle.onValueChanged.AddListener(value => { methodInfo.Invoke(this, new object[] {value}); });
                }

                if (uiElementGameObject.transform.childCount > 0)
                    BuildLinksUi(uiElementGameObject.transform);
            }
        }

        public virtual void Disable()
        {
            _rootTransform.gameObject.SetActive(false);
        }

        public virtual void Enable()
        {
            _rootTransform.gameObject.SetActive(true);
        }

        public bool IsEnabled()
        {
            return _rootTransform.gameObject.activeSelf;
        }

        public abstract void Update();
    }
}