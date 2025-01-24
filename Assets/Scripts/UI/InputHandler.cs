using UnityEngine;
using UnityEngine.EventSystems;
using R3;

namespace UI
{
    public class InputHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public ReactiveProperty<Vector2> OnPointerDownPosition { get; private set; }= new ReactiveProperty<Vector2>();
        public ReactiveProperty<Vector2> OnPointerUpPosition { get; private set; }= new ReactiveProperty<Vector2>();
        
        public void OnPointerDown(PointerEventData eventData)
        {
            OnPointerDownPosition.Value = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnPointerUpPosition.Value = eventData.position;
        }

        private void OnDestroy()
        {
            OnPointerDownPosition?.Dispose();
            OnPointerUpPosition?.Dispose();
        }
    }
}
