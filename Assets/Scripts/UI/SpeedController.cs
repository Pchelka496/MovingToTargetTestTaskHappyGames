using Nenn.InspectorEnhancements.Runtime.Attributes;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SpeedController : MonoBehaviour
    {
        [Required] [HideLabel] [SerializeField]
        SpeedControllerConfig _config;

        [Required] [SerializeField] 
        Slider _speedSlider;

        public float CurrentSpeed { get; private set; }

        private void Awake()
        {
            _speedSlider.minValue = _config.MinSpeed;
            _speedSlider.maxValue = _config.MaxSpeed;

            CurrentSpeed = _speedSlider.value;

            _speedSlider.onValueChanged.AddListener(UpdateSpeedFromSlider);
        }

        private void UpdateSpeedFromSlider(float value)
        {
            CurrentSpeed = value;
        }

        private void OnDestroy()
        {
            _speedSlider.onValueChanged.RemoveListener(UpdateSpeedFromSlider);
        }
    }
}