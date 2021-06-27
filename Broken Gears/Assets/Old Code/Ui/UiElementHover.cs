using UnityEngine.EventSystems;
using UnityEngine;

namespace BrokenGears.Old {
    public class UiElementHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

        public virtual void OnPointerEnter(PointerEventData eventData) { }

        public virtual void OnPointerExit(PointerEventData eventData) { }
    }
}