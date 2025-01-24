using System;
using Nenn.InspectorEnhancements.Runtime.Attributes;
using R3;
using UnityEngine;

namespace UI.TouchMark
{
    public class TouchMarkSpawner : MonoBehaviour
    {
        [Required] [HideLabel] [SerializeField]
        InputHandler _inputHandler;

        [Required] [SerializeField] TouchMark _touchMarkPrefab;

        IDisposable _subscription;

        private void OnEnable()
        {
            _subscription = _inputHandler.OnPointerUpPosition
                .Skip(1) //first Vector.zero
                .Subscribe(OnClickPositionUpdated);
        }

        private void OnDisable()
        {
            _subscription?.Dispose();
        }

        private void OnClickPositionUpdated(Vector2 position)
        {
            var touchMark = Instantiate(_touchMarkPrefab, transform);
            touchMark.transform.position = position;
        }

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }
    }
}