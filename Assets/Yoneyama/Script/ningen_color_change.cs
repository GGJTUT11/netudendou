using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ningen_color_change : MonoBehaviour {

    private float netudendou = 0.5f;
    [SerializeField] private Renderer renderer;

    private AudioSource audiosource;
    private float SongTime;
    private float song_count;
    private bool songstart = false;

    private enum colorFlag
    {
        blue = 0,
        yellow = 1,
        red = 2
    }
    private int bodyColor;


    void Start () {
        //  renderer = this.gameObject.transform.Find("Cube").GetComponent<Renderer>();
        StartCoroutine(changeBodyColor());
        audiosource = GetComponent<AudioSource>();
        audiosource.clip = SoundManager.Instance.audioClip[5];
        SongTime = audiosource.clip.length;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "Player")
        {
            netudendou = other.gameObject.GetComponent<PlayerMove>().Netudendou_Property;
            StartCoroutine(changeBodyColor());
        }
    }

    private IEnumerator changeBodyColor()
    {
        float changeTimer = 0f;
        if (netudendou == 0.0f)
        {
            bodyColor = (int)colorFlag.blue;
        }
        else if (netudendou == 0.5f)
        {
            bodyColor = (int)colorFlag.yellow;
        }
        else
        {
            bodyColor = (int)colorFlag.red;
        }

        switch (bodyColor)
        {
            case (int)colorFlag.blue:
                while (true)
                {
                    yield return new WaitForEndOfFrame();
                    if (renderer.material.color == Color.blue)
                    {
                        break;
                    }
                    changeTimer += Time.deltaTime;
                    renderer.material.color = Color.Lerp(renderer.material.color, Color.blue, changeTimer);
                }
                break;
            case (int)colorFlag.yellow:
                while (true)
                {
                    yield return new WaitForEndOfFrame();
                    if (renderer.material.color == Color.yellow)
                    {
                        break;
                    }
                    changeTimer += Time.deltaTime;
                    renderer.material.color = Color.Lerp(renderer.material.color, Color.yellow, changeTimer);
                }
                break;
            case (int)colorFlag.red:
                while (true)
                {
                    yield return new WaitForEndOfFrame();
                    if (renderer.material.color == Color.red)
                    {
                        break;
                    }
                    changeTimer += Time.deltaTime;
                    renderer.material.color = Color.Lerp(renderer.material.color, Color.red, changeTimer);
                }
                break;
        }
    }
}
