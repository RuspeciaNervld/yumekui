using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class MyTabButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public MyTabGroup tabGroup;

    public Image bg;

    public UnityEvent onTabSelected;
    public UnityEvent OnTabDeSelected;

    public void OnPointerClick(PointerEventData eventData) {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData) {
        tabGroup.OnTabExit(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        bg = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    public void Select() {
        if (onTabSelected != null) {
            onTabSelected.Invoke();
        }
    }

    public void DeSelect() {
        if (OnTabDeSelected != null) {
            OnTabDeSelected.Invoke();
        }
    }
}
