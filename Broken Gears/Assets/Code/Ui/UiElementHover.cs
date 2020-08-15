using UnityEngine.EventSystems;
using UnityEngine;

public class UiElementHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public virtual void OnPointerEnter(PointerEventData eventData) { }

    public virtual void OnPointerExit(PointerEventData eventData) { }
}
