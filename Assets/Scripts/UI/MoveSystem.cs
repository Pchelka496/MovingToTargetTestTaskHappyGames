using System;
using System.Collections.Generic;
using System.Threading;
using Additional;
using Cysharp.Threading.Tasks;
using Nenn.InspectorEnhancements.Runtime.Attributes;
using R3;
using UnityEngine;

namespace UI
{
    public class MoveSystem : MonoBehaviour
    {
        [Required] [HideLabel] [SerializeField]
        InputHandler _inputHandler;

        [Required] [SerializeField] RectTransform _moveObject;
        [Required] [SerializeField] SpeedController _speedController;

        readonly Queue<Vector2> _targetPositionQueue = new Queue<Vector2>();
        bool _isMoving;
        IDisposable _subscription;
        CancellationTokenSource _moveCts;

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
            _targetPositionQueue.Enqueue(position);
            StartMoving();
        }

        [ContextMenu("Start Moving")]
        private void StartMoving()
        {
            if (_isMoving) return;
            _isMoving = true;

            ClearToken();
            _moveCts = new();

            MoveToNextPosition(_moveCts.Token).Forget();
        }

        [ContextMenu("Stop Moving")]
        private void StopMoving()
        {
            if (!_isMoving) return;
            _isMoving = false;

            ClearToken();
        }

        private async UniTaskVoid MoveToNextPosition(CancellationToken token)
        {
            while (_targetPositionQueue.Count > 0)
            {
                Vector2 targetPosition = _targetPositionQueue.Dequeue();

                await MoveToPosition(targetPosition, token);
            }

            StopMoving();
        }

        private async UniTask MoveToPosition(Vector2 targetPosition, CancellationToken token)
        {
            var startPosition = _moveObject.position;
            var journeyLength = Vector2.Distance(startPosition, targetPosition);
            var journeyTravelled = 0f;

            while (journeyTravelled < journeyLength)
            {
                var distanceThisFrame = _speedController.CurrentSpeed * Time.deltaTime;
                journeyTravelled += distanceThisFrame;

                _moveObject.position = Vector2.MoveTowards(startPosition, targetPosition, journeyTravelled);

                await UniTask.Yield(cancellationToken: token);
            }

            _moveObject.position = targetPosition;
        }

        private void ClearToken() => ClearTokenSupport.ClearToken(ref _moveCts);

        private void OnDestroy()
        {
            _subscription?.Dispose();
            ClearToken();
        }
    }
}