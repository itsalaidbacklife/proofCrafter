using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersect : Operator
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

    public override bool legalInput1(gameSet inputSet)
    {
        Debug.Log("Validating intersect input1");
        //Any input is valid if it's already proven
        if (inputSet.electrified)
        {
            input1 = inputSet;
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
    public override bool legalInput2(gameSet inputSet)
    {
        //Any input is valid if it's already proven
        if (inputSet.electrified)
        {
            input2 = inputSet;
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
        Debug.Log("\n\nChecking output legality for intersect");
        if (input1 != null && input2 != null)
        {
            Debug.Log("Had input");
            string outputname = attemptedOutputSet.setName();
            string expectedOutputName = input1.setName() + "n" + input2.setName();
            Debug.Log("Got " + outputname + ", but expected " + expectedOutputName);
            if (attemptedOutputSet.setName() == input1.setName() + "n" + input2.setName() || attemptedOutputSet.setName() == input2.setName() + "n" + input1.setName())
            {
                Debug.Log("Set names match");
                output1 = attemptedOutputSet;
                output1.transform.parent = transform;
                output1.transform.localPosition = new Vector3(1.18f, -.05f, -1.25f);
                output1.electrified = true;
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
        else
        {
            player.clip = invalidClip;
            player.Play();
            return false;
        }
    }
}
