using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using DG.Tweening;

namespace mhartveit
{
    public class UI : Singleton<UI>
    {
        private List<UIElementCollection> elements;


        [SerializeField]
        private GameObject canvas;


        [SerializeField]
        private Ease easeType;

        [SerializeField]
        private float animationSpeed;

        private void Awake()
        {
            elements = new List<UIElementCollection>();
            elements.Add(GetComponentInChildren<PlayerUI>());
            elements.Add(GetComponentInChildren<EventLogUI>());
            elements.Add(GetComponentInChildren<MenuUI>());
            BoardEventHub.Instance.onBoardReady += () => canvas.SetActive(true);
        }

        public T GetUIElement<T>()
        {
            return (T)elements.First(e => e.GetType() == typeof(T));
        }

        public Tween OpenAnimation(Transform transform)
        {
            return transform.DOScale(1, animationSpeed).SetEase(easeType);
        }

        public Tween CloseAnimation(Transform transform)
        {
            return transform.DOScale(0, animationSpeed).SetEase(easeType);
        }
    }

    public interface UIElementCollection
    {
        
    }
}
