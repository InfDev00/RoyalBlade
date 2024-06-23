using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utils
{
    public class LongClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        public Action OnButtonDown = null;
        public Action OnButtonUp = null;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("DOWN");
            OnButtonDown?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("UP");
            OnButtonUp?.Invoke();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("CLICK");
            OnButtonUp?.Invoke();
        }
    }
}