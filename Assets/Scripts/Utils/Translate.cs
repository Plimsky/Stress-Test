using UnityEngine;

namespace Utils
{
    public class Translate : MonoBehaviour
    {
        public Vector3 Direction;
        public float SpeedMin = 1f;
        public float SpeedMax = 10f;

        private float _currentSpeed;

        private void Awake()
        {
            _currentSpeed = Random.Range(SpeedMin, SpeedMax);
        }

        private void Update()
        {
            transform.Translate(Direction * _currentSpeed * Time.deltaTime);
        }
    }
}