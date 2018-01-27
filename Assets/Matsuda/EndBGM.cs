using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBGM : MonoBehaviour
{
    [SerializeField] private AudioClip GameClearBGM;
    [SerializeField] private AudioClip GameOverBGM;
    GameObject Canvas;
    void Start ()
    {
        Canvas = GameObject.Find("Canvas");
		if (Canvas.GetComponent<GameEnd>().GetClearOrOver() == true)
        {
            GetComponent<AudioSource>().clip = GameClearBGM;
            GetComponent<AudioSource>().Play();
        }
        else if (Canvas.GetComponent<GameEnd>().GetClearOrOver() == false)
        {
            GetComponent<AudioSource>().clip = GameOverBGM;
            GetComponent<AudioSource>().Play();
        }
    }
}
