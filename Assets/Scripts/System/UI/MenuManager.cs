using System.Collections.Generic;
using ScriptableData;
using UnityEngine;

namespace System.UI
{
    [ExecuteInEditMode]
    public class MenuManager : MonoBehaviour
    {
        #region DATA
        public static MenuManager Instance;
        public ConfigProjectData ConfigProjectData;
        public ModelSpawnData ModelDataSpawn;
        #endregion

        private readonly List<GameObject> _panelMenuList = new List<GameObject>();
        private readonly Dictionary<UIStateEnum, AUIState> _stateDictionary = new Dictionary<UIStateEnum, AUIState>();
        private readonly Stack<UIStateEnum> _stateStack = new Stack<UIStateEnum>();

        private UIStateEnum _actualStateEnum;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(Instance);

            // Get all panels in the root canvas containing the name "Menu" and
            // store them into the _panelMenuList
            FetchObjects("Menu", transform, _panelMenuList);

            // Generate a dictionnary of states from thoses panels
            GenerateStates();

            // Add the first newState into the queue states
            _stateStack.Push(GetFirstState());
            _actualStateEnum = GetFirstState();

            // Disable all panels

            DisableAllPanels();
        }

        private void Update()
        {
            if (_stateDictionary.Count > 0 && !_stateDictionary[_stateStack.Peek()].IsEnabled())
            {
                _stateDictionary[_stateStack.Peek()].Enable();
            }
        }

        public void AddState(UIStateEnum newState)
        {
            UpdateState(newState);
            DisableAllPanelsInStack();

            _stateStack.Push(newState);
        }

        public void RemoveLastState(UIStateEnum newState)
        {
            UpdateState(newState);
            DisableAllPanelsInStack();

            _stateStack.Pop();
        }

        private void UpdateState(UIStateEnum state)
        {
            _actualStateEnum = state;
        }

        private void GenerateStates()
        {
            foreach (var container in _panelMenuList)
            {
                Type typeName = Type.GetType(GetType().Namespace + "." + container.name + "State");

                if (typeName != null)
                {
                    Object[] args = {this};
                    AUIState newAuiState = Activator.CreateInstance(typeName, args) as AUIState;
                    string typeNameStr = typeName.Name.ToLower();

                    for (UIStateEnum state = UIStateEnum.NONE; state <= UIStateEnum.ACTIONSMENU; ++state)
                    {
                        if (newAuiState != null &&
                            !_stateDictionary.ContainsKey(state) &&
                            typeNameStr.Contains(state.ToString().ToLower()))
                        {
                            newAuiState.Setup(container.transform);
                            _stateDictionary.Add(state, newAuiState);

                            break;
                        }
                    }
                }
            }
        }

        private void FetchObjects(string menu, Transform transformElement, List<GameObject> containerList)
        {
            if (transformElement.name.Contains(menu))
            {
                containerList.Add(transformElement.gameObject);
            }

            foreach (Transform childTransform in transformElement)
            {
                FetchObjects(menu, childTransform, containerList);
            }
        }

        private void DisableAllPanels()
        {
            foreach (var state in _stateDictionary)
                state.Value.Disable();
        }

        private void DisableAllPanelsInStack()
        {
            foreach (var uiStateEnum in _stateStack)
                _stateDictionary[uiStateEnum].Disable();
        }

        private UIStateEnum GetFirstState()
        {
            return UIStateEnum.ACTIONSMENU;
        }
    }
}