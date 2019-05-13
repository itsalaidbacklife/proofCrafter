using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class drag_and_drop_separate_script : MonoBehaviour, IDropHandler {
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped!");
        //throw new NotImplementedException();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
