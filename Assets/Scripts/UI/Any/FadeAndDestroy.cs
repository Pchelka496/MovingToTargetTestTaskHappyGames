using System;
using DG.Tweening;
using Nenn.InspectorEnhancements.Runtime.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Any
{
    public class FadeAndDestroy : MonoBehaviour
    {
        [Required] [SerializeField] Image _image;
        [Range(0f, 100f)] [SerializeField] float _fadeTime = 2f;

        private void Start()
        {
            FadeOutAndDestroy();
        }

        private void FadeOutAndDestroy()
        {
            _image.DOFade(0f, _fadeTime).OnComplete(() => { Destroy(gameObject); });
        }

        private void Reset()
        {
            _image = gameObject.GetComponent<Image>();
        }
    }
}