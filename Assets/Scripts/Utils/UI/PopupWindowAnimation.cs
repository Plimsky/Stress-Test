using UnityEngine;

namespace Utils.UI
{
    [RequireComponent(typeof(Animator))]
    public class PopupWindowAnimation : MonoBehaviour
    {
        public string OpenParemeterValue = "Open";
        private Animator _animator;

        private void OnEnable()
        {
            if (CheckAndGetAnimator())
                _animator.SetBool(OpenParemeterValue, true);
        }

        private bool CheckAndGetAnimator()
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();

            return _animator != null;
        }


        public void CloseWindow()
        {
            if (CheckAndGetAnimator() && gameObject.activeSelf)
                _animator.SetBool(OpenParemeterValue, false);
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}