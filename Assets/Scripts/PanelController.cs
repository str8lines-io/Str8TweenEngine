using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Str8lines.Tweening;

public class PanelController : MonoBehaviour 
{    
    private int panelIndex;

    void Start()
    {
        panelIndex = 0;
        for(int i = 0; i < this.gameObject.transform.childCount - 1; i++) this.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(panelIndex).gameObject.SetActive(true);
    }

    public void Next(){
        this.gameObject.transform.GetChild(panelIndex).gameObject.SetActive(false);
        if(panelIndex < 3) panelIndex++;
        this.gameObject.transform.GetChild(panelIndex).gameObject.SetActive(true);
    }

    public void Previous(){
        this.gameObject.transform.GetChild(panelIndex).gameObject.SetActive(false);
        if(panelIndex > 0) panelIndex--;
        this.gameObject.transform.GetChild(panelIndex).gameObject.SetActive(true);
    }

    public void ChangeFadeTarget(Dropdown change){
        Transform fadePanel = this.gameObject.transform.GetChild(3);
        for(int i = 0; i < fadePanel.childCount - 2; i++) fadePanel.GetChild(i).gameObject.SetActive(false);
        fadePanel.GetChild(change.value).gameObject.SetActive(true);
    }
}