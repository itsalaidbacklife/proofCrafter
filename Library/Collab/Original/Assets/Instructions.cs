using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{
    string[][] messages; //2D array lists a multiple series of messages
    int outerIndex;
    int innerIndex;
    public bool displayText = true; //Toggles text display
    public gameSet startSet;
    public gameSet targetSet;
    public gameSet targetSet2;
    public string level;
    //public List<List<gameSet>> setChains;

    public Button winButton;

    // Start is called before the first frame update
    void Start()
    {
        startSet.electrified = true; //Electricify first set
        startSet.chainIndex = 0; // Starting set is in the first chain
        targetSet.isGoalSet = true;
        if (targetSet2 != null) targetSet2.isGoalSet = true;
        //setChains = new List<List<gameSet>>();
        //List<gameSet> firstChain = new List<gameSet>();
        //firstChain.Add(startSet);
        //setChains.Add(firstChain);
        switch (level)
        {
            case "0A":
                messages = new string[][] { new string[] { "Let's show that anything in AnB must be in A.\nPress 'SPACE' to Continue", "We start with AnB and drag a wire from its terminal\n to the input of the Separate operator.", "Once the operator has input, we can drag the Set A \ninto the FIRST output of the operator."}, new string[] { "This is the second series of messages"} };
                break;
            case "0B":
                messages = new string[][] { new string[] { "Now you try. Show that anyting in AnB must be in B.\nPress 'SPACE' to Continue", "We start with AnB and drag a wire from its terminal\n to the input of the Separate operator.", "Once the operator has input, we can drag the Set B \ninto the SECOND output of the operator." }, new string[] { "This is the second series of messages" } };
                break;
            case "0C":
                messages = new string[][] { new string[] { "Now, try to unpack CnD into C and D!"}};
                break;
            case "0E":
                messages = new string[][] { new string[] { "Finally, go ahead and unpack FnE into E and F.\nWatch out for the other sets!"}};
                break;
            case "1A":
                messages = new string[][] { new string[] { "How do you know if two groups are equal?\nPress 'SPACE' to Continue", "If everything in one group (A) is in another group (B),\nthen A is a subset of B.", "And if everything in B is also in A, then both groups are equal.", "Let's try it with AnB and BnA.", "Prove AnB is a subset of BnA by creating a circuit\n that starts with AnB and ends with BnA", "We'll start with AnB as our power source." }, new string[] { "This is the second series of messages", "sure is" } };
                break;
            case "1B":
                messages = new string[][] { new string[] { "Prove that BnA is a subset of AnB. \nPress 'SPACE' to Continue \n1/2" }, new string[] { "This is the second series of messages", "sure is" } };
                break;
            case "2A":
            case "2B":
                messages = new string[][] { new string[] { "Can you prove An(BnC) = An(CnB)?\nPress 'SPACE' to Continue \n", "", "", "", "", "" }, new string[] { "This is the second series of messages", "sure is" } };
                break;
            case "3A":
                messages = new string[][] { new string[] { "Prove An(BnC) is a subset of C" }, new string[] { "This is the second series of messages", "sure is" } };
                break;
            case "3B":
                messages = new string[][] { new string[] { "Prove (BnE)nF is a subset of E" }, new string[] { "This is the second series of messages", "sure is" } };
                break;
            case "5A":
                messages = new string[][] { new string[] { "Now we can prove that AnB is a subset of BnA!\nTry and do that using both the \"Split\" operator\nand the \"Combine\" operator!"}};
                break;
            case "5B":
                messages = new string[][] { new string[] { "Try and prove the BnA is a subset of AnB."}};
                break;
            case "Cn":
                messages = new string[][] { new string[] {"Try it yourself to get AnB from A and B."}};
                break;
            case "flip_tutorial":
                messages = new string[][] { new string[] { "Prove AnB is a subset of BnA" }, new string[] { "This is the second series of messages", "sure is" } };
                break;
            case "flip_tutorial2":
                messages = new string[][] { new string[] { "Prove EnF is a subset of FnE" }, new string[] { "This is the second series of messages", "sure is" } };
                break;
            case "flip_tutorial3":
                messages = new string[][] { new string[] { "Now Prove An(BnC) is a subset of CnB" }, new string[] { "This is the second series of messages", "sure is" } };
                break;
            case "An(BnC)->An(CnB)":
                messages = new string[][] { new string[] { "Prove An(BnC) is a subset of An(CnB)" }, new string[] { "This is the second series of messages", "sure is" } };
                break;
            default:
                messages = new string[][] { new string[] { "How do you know if two groups are equal?\nPress 'SPACE' to Continue \n1/2", "If everything in one group (A) is in another group (B),\nthen A is a subset of B.\n2/3", "And if everything in B is also in A, then both groups are equal.\n3/5", "Let's try it with AnB and BnA.\n4/5", "Prove AnB is a subset of BnA by creating a circuit\n that starts with AnB and ends with BnA\n5/6.", "We'll start with AnB as our power source. \n6/6" }, new string[] { "This is the second series of messages", "sure is" } };
                break;
        }
        outerIndex = 0;
        innerIndex = 0;

        if (winButton) {
            winButton.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Check for win
        if (targetSet.electrified && (targetSet2 == null || targetSet2.electrified) )
        {
            displayText = true;
            GetComponent<TextMesh>().text = "Great Job!";
            if (winButton) {
                winButton.gameObject.SetActive(true);
            }

        } else {
            GetComponent<TextMesh>().text = messages[outerIndex][innerIndex];
            if (!displayText) {
                GetComponent<TextMesh>().text = "";
            }
        }


        //Player hit space to progress through text
        if (Input.GetKeyDown("space"))
        {
            if (innerIndex < messages[outerIndex].Length - 1)
            {
                innerIndex += 1;
            }
            else
            {
                innerIndex = 0;
                displayText = false;
            }
        }

    }

}
