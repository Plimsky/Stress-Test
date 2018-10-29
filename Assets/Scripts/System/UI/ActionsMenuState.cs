using UnityEngine;
using Utils.UI;

namespace System.UI
{
    public class ActionsMenuState : AUIState
    {
        private PopupWindowAnimation _popupWindowAnimation;

        public ActionsMenuState(MenuManager menuManager) : base(menuManager)
        {
        }

        public override void Setup(Transform root)
        {
            base.Setup(root);
            _popupWindowAnimation = root.gameObject.GetComponent<PopupWindowAnimation>();
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

        private void SpawnConfigButton()
        {
            _menuManager.AddState(UIStateEnum.SPAWNCONFIGMENU);
        }
    }
}