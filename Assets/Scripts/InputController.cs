using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
    bool dragging; //Whether player's currently dragging
    Transform dragTarget; //Transform of drag target
    float targetZ; //Z position of drag target
    Vector3 mouseDistanceFromObjCenter; //vector from mouse position to center of obj
    string dragType; //kind of object being dragged
    public GameObject wirePrefab;
    List<Wire> wires = new List<Wire>(); //All wires drawn so far
    bool draggingWire; //Whether player's currently creating new wire
    LineRenderer lr; //Used to draw temporary wire while dragging

    // Use this for initialization
    void Start () {
        //Configure line renderer for drawing temporary wires while dragging (before drop)
        lr = this.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = new Color32(0, 0, 0, 255); //Black line
        lr.endColor = new Color32(0, 0, 0, 255); //Black line
        lr.startWidth = 0.04f;
        lr.endWidth = 0.04f;
    }
	
	// Update is called once per frame
	void Update () {
        //User begins drag
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hits.Length != 0)
            {
                RaycastHit2D topHit = hits[0];
                dragTarget = topHit.transform;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseDistanceFromObjCenter = dragTarget.position - mousePos;
                targetZ = dragTarget.position.z;
                dragging = true;
                dragType = dragTarget.gameObject.tag;

                if (dragType == "Terminal")
                {
                    draggingWire = true;
                }
            }
        }
        // Release mouse -> stop dragging and/or create wire
        if (Input.GetMouseButtonUp(0))
        {


            //If dragged from terminal to terminal, draw wire
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hits.Length > 0)
            {
                
                RaycastHit2D topHit = hits[0];
                

                if (topHit.transform.gameObject.tag != "Terminal")
                {
                    topHit = hits[1 % (hits.Length)];
                }
                GameObject dropTarget = topHit.transform.gameObject;
                //Player attempting to draw wire
                if (dropTarget.tag == "Terminal" && dragType == "Terminal")
                {
                    //Check legality of connection
                    if (dragTarget.parent.tag == "Set" && dropTarget.transform.parent.tag == "Operator")
                    {
                        gameSet inputSet = dragTarget.transform.parent.GetComponent<gameSet>();
                        bool valid = false;
                        //Check which input is being connected
                        if (dropTarget.GetComponent<Terminal>().terminalNumber == 1)
                        {
                            Debug.Log("Input controller checking validity of terminal 1");
                            if (inputSet.electrified && dropTarget.transform.parent.GetComponent<Operator>().legalInput1(inputSet)) valid = true;
                        } 
                        else
                        {
                            if (inputSet.electrified && dropTarget.transform.parent.GetComponent<Operator>().legalInput2(inputSet)) valid = true;
                        }
                        if (valid) {
                            GameObject newLine = Instantiate(wirePrefab) as GameObject;
                            newLine.transform.parent = dragTarget.transform;
                            newLine.transform.position = dragTarget.position;
                            Wire wireRef = newLine.GetComponent<Wire>();
                            wireRef.t1 = dragTarget.GetComponent<Terminal>();
                            wireRef.t2 = dropTarget.GetComponent<Terminal>();
                            wires.Add(wireRef);
                        }
                        else
                        {
                            Debug.Log("Failed legality check");
                        }
                    }
                    else
                    {
                        Debug.Log("Terminals not between set and operator");
                    }
                }
                //Dropping set into output1 of an operator
                else if (dropTarget.tag == "Output1" && dragType == "Set")
                {
                    Operator op = dropTarget.transform.parent.GetComponent<Operator>();
                    Debug.Log(op);
                    gameSet s = dragTarget.transform.GetComponent<gameSet>();
                    Debug.Log(s);
                    bool legal = op.legalOutput1(s);
                    if (legal)
                    {
                        Debug.Log("Legal Drop");
                    }
                    else
                    {
                        Debug.Log("Illegal Drop");
                    }
                }
                else if (dropTarget.tag == "Output2" && dragType == "Set")
                {
                    Debug.Log("Checking Output2 legality");
                    Operator op = dropTarget.transform.parent.GetComponent<Operator>();
                    gameSet s = dragTarget.transform.GetComponent<gameSet>();
                    bool legal = op.legalOutput2(s);
                    if (legal)
                    {
                        Debug.Log("Legal Drop");
                    }
                    else
                    {
                        Debug.Log("Illegal Drop");
                    }
                }
            }
            dragging = false;
            draggingWire = false;
            lr.enabled = false;
            targetZ = 0;
            dragTarget = null;
            mouseDistanceFromObjCenter = Vector3.zero;
        }
        //While dragging
        if (dragging == true)
        {
            if (draggingWire)
            {
                lr.enabled = true;
                lr.SetPosition(0, dragTarget.position);
                lr.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
            else
            {
                lr.enabled = false;
            }
            // Re-positioning piece
            if (dragType != "Terminal" && dragType != "Wire" && dragTarget.parent == null)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 newPosition = new Vector3(mousePos.x, mousePos.y, targetZ) + mouseDistanceFromObjCenter;
                newPosition.z = targetZ;
                dragTarget.position = newPosition;
            } 
        }

        //Delete previous wire
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            wires[wires.Count - 1].destroyWire();
            wires.RemoveAt(wires.Count - 1);
        }
    }
}
