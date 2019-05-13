using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class drag_and_drop_set_script : MonoBehaviour, IDragHandler, IEndDragHandler, IDropHandler {
    public void OnDrag(PointerEventData eventData)
    {

        //Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        //transform.position = Input.mousePosition;
        // Find Camera Position
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        // Calculate distance between mouse and camera
        Vector2 rayPoint = ray.GetPoint(Vector2.Distance(transform.position, Camera.main.transform.position));
        Vector3 newPos = new Vector3(rayPoint.x, rayPoint.y, 2);
        //Move the GameObject to mouse position
        transform.position = newPos;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped the set");
        //throw new NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //transform.position = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
        Debug.Log("end drag");

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit[] hits;
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            hits = Physics.RaycastAll(camRay);
            Debug.Log("hits " + hits.Length);
            GameObject lastGameObject = null;
        }

    }
}
