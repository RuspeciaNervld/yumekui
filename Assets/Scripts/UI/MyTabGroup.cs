using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyTabGroup : MonoBehaviour
{
    public List<MyTabButton> tabButtons;
    public Sprite tabIdle;
    public Color idle;
    public Sprite tabHover;
    public Color hover;
    public Sprite tabActive;
    public Color active;
    public MyTabButton selectedTab;
    public List<GameObject> objectsToSwap;

    public void Subscribe(MyTabButton button) {
        if(tabButtons == null) {
            tabButtons = new List<MyTabButton>();
        }
        tabButtons.Add(button);
    }
    

    public void OnTabEnter(MyTabButton button) {
        ResetTabs();
        if(selectedTab == null || button != selectedTab) {
            button.bg.sprite = tabHover;
            button.bg.color = hover;
        }
    }

    public void OnTabExit(MyTabButton button) {
        ResetTabs();
    }

    public void OnTabSelected(MyTabButton button) {
        if(selectedTab != null) {
            selectedTab.DeSelect();
        }


        ResetTabs();
        button.bg.sprite = tabActive;
        button.bg.color = active;
        selectedTab = button;

        selectedTab.Select();

        int idx = button.transform.GetSiblingIndex();
        for(int i = 0; i < objectsToSwap.Count; i++) {
            if (i == idx) {
                objectsToSwap[i].SetActive(true);
            } else {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs() {
        foreach(MyTabButton button in tabButtons) {
            if(selectedTab == null || button != selectedTab) {
                button.bg.sprite = tabIdle;
                button.bg.color = idle;
            }
        }
    }
}
