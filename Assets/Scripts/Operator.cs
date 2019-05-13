using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operator : MonoBehaviour
{
    public string type;
    public gameSet input1 = null;
    public gameSet input2 = null;
    public gameSet output1 = null;
    public gameSet output2 = null;
    public AudioSource player;
    public AudioClip inputClip;
    public AudioClip outputClip;
    public AudioClip invalidClip;
    //public Instructions instructions; //Used to add new sets to the set chain
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual bool legalInput1(gameSet inputSet)
    {
        Debug.Log("Validating legalInput1 for default operator");
        return false;
    }
    public virtual bool legalInput2(gameSet inputSet)
    {
        return false;
    }
    public virtual bool legalOutput1(gameSet outputSet)
    {
        Debug.Log("Failed to overload legalOutput1");
        return false;
    }
    public virtual bool legalOutput2(gameSet outputSet)
    {
        return false;
    }
}
