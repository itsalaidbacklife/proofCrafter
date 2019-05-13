using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialController : MonoBehaviour
{
    public Text[] tutorialText;

    public float delay;

    public Shading shape;

    public Button button;

    private int textIndex;

    public VideoPlayer video; 

    private bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Text t in tutorialText) {
            t.CrossFadeAlpha(0, 0, true);
        }

        button.gameObject.SetActive(false);

        Invoke("NextText", delay);

        if (video != null) {
            video.Pause();
        }
    }

    void Update() {
        if (Input.GetKeyDown("space")) {
            NextText();
        }
    }

    void NextText() {
        if (textIndex < tutorialText.Length) {
            tutorialText[textIndex].CrossFadeAlpha(1, 0.5f, false);
            textIndex++;
            Invoke("NextText", delay);
        } else {
            if (!done) {
                if (button != null) {
                    button.gameObject.SetActive(true);
                }

                if (shape != null) {
                    shape.LoadProblem();
                }

                if (video != null) {
                    video.Play();
                }
                done = true;
            }
        }
    }
}
