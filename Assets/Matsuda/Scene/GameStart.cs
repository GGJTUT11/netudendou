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
    private string sceneName = "";
    GameObject stageSelect;
    GameObject cursor;
    enum GameState
    {
        Start, Easy, Normal, Hard, Rule,
    }
    GameState gameState;
	void Start ()
    {
        pushanykeytext = transform.Find("Title/PushAnyKeyText").gameObject;
        title = transform.Find("Title").gameObject;
        instruction = transform.Find("Instruction").gameObject;
        loadtext = transform.Find("LoadText").gameObject;
        gameState = GameState.Start;
        stageSelect = transform.Find("StageSelect").gameObject;
        cursor = transform.Find("StageSelect/Cursor").gameObject;
	}
	void Update ()
    {
        Debug.Log(gameState);
        delta += Time.deltaTime;
        TextFlashing(pushanykeytext);
        if (Input.anyKeyDown && gameState == GameState.Start)
        {
            gameState = GameState.Easy;
            pushanykeytext.SetActive(false);
            stageSelect.SetActive(true);
            GetComponent<AudioSource>().Play();
        }
        else if (gameState == GameState.Easy)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                sceneName = "stage1";
                title.SetActive(false);
                stageSelect.SetActive(false);
                instruction.SetActive(true);
                gameState = GameState.Rule;
                GetComponent<AudioSource>().Play();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                gameState = GameState.Normal;
                cursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, -175);
                GetComponent<AudioSource>().Play();
            }
        }
        else if (gameState == GameState.Normal)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                gameState = GameState.Hard;
                cursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(100, -175);
                GetComponent<AudioSource>().Play();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                gameState = GameState.Easy;
                cursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(-300, -175);
                GetComponent<AudioSource>().Play();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                sceneName = "stage1";
                title.SetActive(false);
                stageSelect.SetActive(false);
                instruction.SetActive(true);
                gameState = GameState.Rule;
                GetComponent<AudioSource>().Play();
            }
        }
        else if (gameState == GameState.Hard)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                gameState = GameState.Normal;
                cursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, -175);
                GetComponent<AudioSource>().Play();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                sceneName = "stage3";
                title.SetActive(false);
                stageSelect.SetActive(false);
                instruction.SetActive(true);
                gameState = GameState.Rule;
                GetComponent<AudioSource>().Play();
            }
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
            SceneManager.LoadScene(sceneName);
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