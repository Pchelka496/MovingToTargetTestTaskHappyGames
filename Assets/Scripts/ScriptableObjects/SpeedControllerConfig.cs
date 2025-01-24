using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SpeedControllerConfig", menuName = "Scriptable Objects/SpeedControllerConfig")]
    public class SpeedControllerConfig : ScriptableObject
    {
        [SerializeField] float _minSpeed = 0f;
        [SerializeField] float _maxSpeed = 2000f;

        public float MinSpeed => _minSpeed;
        public float MaxSpeed => _maxSpeed;

        private void OnValidate()
        {
            _minSpeed = Mathf.Clamp(_minSpeed, 0f, _maxSpeed);
            
            if (_maxSpeed < 0f)
            {
                _maxSpeed = 0f;
            }
        }
    }
}