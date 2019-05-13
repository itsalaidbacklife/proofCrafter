using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public Terminal t1 = null; //Start of wire
    public Terminal t2 = null; //End of wire
    LineRenderer lr;



    // Start is called before the first frame update
    void Start()
    {
        lr = this.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = new Color32(245, 238, 64, 255);
        lr.endColor = new Color32(245, 238, 64, 255);
        lr.startWidth = 0.04f;
        lr.endWidth = 0.04f;
    }

    // Update is called once per frame
    void Update()
    {
        if (t1 != null && t2 != null)
        {
            lr.SetPosition(0, t1.transform.position);
            lr.SetPosition(1, t2.transform.position);
        } 

    }
    public void destroyWire()
    {
        Debug.Log("\nDestroying Wire!");
        Operator op = t2.transform.parent.GetComponent<Operator>();
        //Removeoff output1
        if (op.output1 != null)
        {
            op.output1.electrified = false;
            op.output1.chainIndex = null;
            op.output1.transform.parent = null;
            op.output1 = null;
        }
        //Remove output2
        if (op.output2 != null)
        {
            op.output2.electrified = false;
            op.output2.chainIndex = null;
            op.output2.transform.parent = null;
            op.output2 = null;
        }

        switch (t2.terminalNumber)
        {
            case 1:
                op.input1 = null;
                break;
            case 2:
                op.input2 = null;
                break;
        }
        Destroy(this.gameObject);

    }
    
}
