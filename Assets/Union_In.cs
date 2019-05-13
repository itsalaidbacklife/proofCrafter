using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Union_In : Operator
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool legalInput1(gameSet attemptedInputSet)
    {
        if (input1 == null && attemptedInputSet.type == gameSet.SetType.Union)
        {
            input1 = attemptedInputSet;
            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool legalOutput1(gameSet attemptedOutputSet)
    {
        if (input1 != null && input1.leftSet == attemptedOutputSet.getSetName())
        {
            output1 = attemptedOutputSet;
            output1.electrified = true;
            output1.transform.parent = transform;
            return true;
        } else
        {
            return false;
        }
    }

    public override bool legalOutput2(gameSet attemptedOutputSet)
    {
        if (input1 != null && input1.rightSet == attemptedOutputSet.getSetName())
        {
            output2 = attemptedOutputSet;
            output2.electrified = true;
            output2.transform.parent = transform;
            return true;
        }
        else
        {
            return false;
        }
    }
}
