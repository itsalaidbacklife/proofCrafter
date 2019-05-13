using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    VideoPlayer player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (!player.isPlaying)
        {
            player.enabled = false;
            this.transform.parent.GetChild(1).gameObject.SetActive(false);
        }*/
    }
}
