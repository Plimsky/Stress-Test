using System.Runtime.Serialization.Formatters;
using UnityEngine;

namespace Utils.UI
{
    [ExecuteInEditMode]
    public class ResizeDeactivateEditor : MonoBehaviour
    {
#if UNITY_EDITOR
        private void OnEnable()
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        private void OnDisable()
        {
            transform.localScale = new Vector3(0, 0, 0);
        }
#endif
    }
}