using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameSet : MonoBehaviour
{
    public enum SetType
    {
        Simple, Intersection, Union, Difference
    }
    public SetType type = SetType.Simple;
    public string leftSet;
    public string rightSet;
    public Sprite offSprite;
    public Sprite onSprite;
    public bool electrified = false; //Whether set is currently powered (in circuit)
    public int? chainIndex = null; //Which chain of sets this set is in (if it is in a circuit)
    public bool isGoalSet = false;
    //Render lightbulb (on or off) above goal set(s)
    public Sprite bulbOffSprite;
    public Sprite bulbOnSprite;
    private SpriteRenderer bulbRenderer;

    public string setName()
    {
        switch (type)
        {
            case SetType.Simple:
                return leftSet;
            case SetType.Intersection:
                return leftSet + "n" + rightSet;
            case SetType.Union:
                return leftSet + "u" + rightSet;
            default:
                return "";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        if (type == SetType.Intersection || type == SetType.Union)
        {
            TextMesh nameText = this.transform.GetChild(4).GetComponent<TextMesh>();
            TextMesh leftText = this.transform.GetChild(2).GetComponent<TextMesh>();
            TextMesh rightText = this.transform.GetChild(3).GetComponent<TextMesh>();
            string adjustedLeftText = leftSet;
            if (leftSet.Length > 1)
            {
                adjustedLeftText = "(" + leftSet + ")";
            }
            leftText.text = adjustedLeftText;
            string adjustedRightText = rightSet;
            if (rightSet.Length > 1)
            {
                Debug.Log("Adjusting set name for right set " + rightSet);
                adjustedRightText = "(" + rightSet + ")";
            }
            rightText.text = adjustedRightText;
            string operatorChar;
            if (type == SetType.Intersection)
            {
                operatorChar = " n ";
            }
            else
            {
                operatorChar = " u ";
            }
            nameText.text = adjustedLeftText + operatorChar + adjustedRightText;
            bulbRenderer = this.transform.GetChild(5).GetComponent<SpriteRenderer>();

        }
        else if (type == SetType.Simple)
        {
            this.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = isGoalSet;
            TextMesh setName = this.transform.GetChild(0).GetComponent<TextMesh>();
            setName.text = leftSet;
            bulbRenderer = this.transform.GetChild(2).GetComponent<SpriteRenderer>();
        }

        bulbRenderer.enabled = isGoalSet;

    }

    // Update is called once per frame
    void Update()
    {
        setSprite();
        positionTerminal();
    }
    public virtual string getSetName()
    {
        switch(type)
        {
            case SetType.Simple:
                return leftSet;
            case SetType.Intersection:
                return leftSet + "n" + rightSet;
            default:
                return "";
        }
    }

    //Chooses appropriate sprite based on whether set is on/off
    void setSprite()
    {
        switch(electrified)
        {
            case false:
                switch (type)
                {
                    case SetType.Simple:
                        //Assign sprite for set
                        if (GetComponent<SpriteRenderer>().sprite != offSprite) GetComponent<SpriteRenderer>().sprite = offSprite;
                        if (isGoalSet)
                        {
                            bulbRenderer.sprite = bulbOffSprite;
                            bulbRenderer.enabled = true;
                        } else
                        {
                            bulbRenderer.enabled = false;
                        }
                        break;
                    case SetType.Intersection:
                    case SetType.Union:
                        if (this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite != offSprite) this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = offSprite;
                        if (isGoalSet)
                        {
                            bulbRenderer.sprite = bulbOffSprite;
                            bulbRenderer.enabled = true;
                        }
                        else
                        {
                            bulbRenderer.enabled = false;
                        }
                        break;
                }
                break;
            case true:
                switch (type)
                {
                    case SetType.Simple:
                        if (GetComponent<SpriteRenderer>().sprite != onSprite) GetComponent<SpriteRenderer>().sprite = onSprite;
                        if (isGoalSet)
                        {
                            bulbRenderer.sprite = bulbOnSprite;
                            bulbRenderer.enabled = true;

                        } else
                        {
                            bulbRenderer.enabled = false;
                        }
                        break;
                    case SetType.Intersection:
                    case SetType.Union:
                        if (this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite != onSprite) this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = onSprite;
                        if (isGoalSet)
                        {
                            bulbRenderer.sprite = bulbOnSprite;
                        }
                        break;
                }
                break;
        }
    }

    //Adjusts terminal positioning for appropriate sprite (on/off sprites)
    void positionTerminal()
    {
        if (electrified && !isGoalSet)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }

}
