using UnityEngine;

namespace Utils
{
    public class SamePosition: MonoBehaviour
    {
        [SerializeField] private Transform _targetTransform;

        private void Update()
        {
            transform.position = _targetTransform.position;
        }
    }
}