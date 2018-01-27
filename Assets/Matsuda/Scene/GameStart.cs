using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    private GameObject pushanykeytext;
    private GameObject title;
    private GameObject instruction;
    private GameObject loadtext;
    private float flashingIntarval = 0.5f;
    private float delta = 0;
    private float mainInterval = 5.0f;
    private float maindelta = 0;
    private bool isStart = false;
    enum GameState
    {
        Start, Rule,
    }
    GameState gameState;
	void Start ()
    {
        pushanykeytext = transform.Find("Title/PushAnyKeyText").gameObject;
        title = transform.Find("Title").gameObject;
        instruction = transform.Find("Instruction").gameObject;
        loadtext = transform.Find("LoadText").gameObject;
        gameState = GameState.Start;
	}
	void Update ()
    {
        delta += Time.deltaTime;
        TextFlashing(pushanykeytext);
        if (Input.anyKeyDown && gameState == GameState.Start)
        {
            gameState = GameState.Rule;
            title.SetActive(false);
            instruction.SetActive(true);
            GetComponent<AudioSource>().Play();
        }
        else if (Input.anyKeyDown && gameState == GameState.Rule)
        {
            isStart = true;
            GetComponent<AudioSource>().Play();
        }
        if (isStart)
        {
            loadtext.SetActive(true);
            maindelta += Time.deltaTime;
            TextFlashing(loadtext);
        }
        if (maindelta > mainInterval)
        {
            isStart = false;
            SceneManager.LoadScene("Main_Test");
        }
    }
    //テキストを点滅させるメソッド
    private void TextFlashing(GameObject text)
    {
        if (delta > this.flashingIntarval)
        {
            float alpha = text.GetComponent<CanvasRenderer>().GetAlpha();
            if (alpha == 1.0f) text.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
            else text.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
            delta = 0;
        }
    }
}