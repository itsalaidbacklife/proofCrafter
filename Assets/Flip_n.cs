using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip_n : Operator
{
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override bool legalInput1(gameSet attemptedInputSet)
    {
        if (input1 == null && attemptedInputSet.type == gameSet.SetType.Intersection)
        {
            input1 = attemptedInputSet;
            player.clip = inputClip;
            player.Play();
            return true;
        }
        else
        {
            player.clip = invalidClip;
            player.Play();
            return false;
        }
    }
    public override bool legalOutput1(gameSet attemptedOutputSet)
    {
        if (input1 != null && (input1.leftSet == attemptedOutputSet.rightSet && input1.rightSet == attemptedOutputSet.leftSet))
        {
            output1 = attemptedOutputSet;
            output1.electrified = true;
            //Add new output to same set chain as input1
            output1.chainIndex = input1.chainIndex;
            //instructions.setChains[output1.chainIndex].Add(output1); //Add new output to the current chain
            output1.transform.parent = transform;
            output1.transform.localPosition = new Vector3(1.2f, 0f, -1.25f);
            player.clip = outputClip;
            player.Play();
            return true;
        }
        else
        {
            player.clip = invalidClip;
            player.Play();
            return false;
        }
    }
}
