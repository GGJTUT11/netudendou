using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    private bool clearorover = true;
    private float onemoreplayInterval = 2.0f;
    private bool onemoreplayflag = false;
    private float delta = 0.0f;
    enum GameState
    {
        Result, Continue, End,
    }
    GameState gameState;
    GameObject gameClear;
    GameObject gameOver;
    GameObject onemoreplay;
    GameObject cursor;
	void Start ()
    {
        clearorover = DeathBlock.GetIsClear();
        clearorover = Rock.GetIsClear();
        clearorover = Goal.GetIsClear();
        gameState = GameState.Result;
        gameClear = transform.Find("GameClear").gameObject;
        gameOver = transform.Find("GameOver").gameObject;
        onemoreplay = transform.Find("OneMorePlay").gameObject;
        cursor = transform.Find("OneMorePlay/Cursor").gameObject;
	}
	void Update ()
    {
        if (gameState == GameState.Result)
        {
            if (clearorover == true)
            {
                gameClear.SetActive(true);
                onemoreplayflag = true;
            }
            else if (clearorover == false)
            {
                gameOver.SetActive(true);
                onemoreplayflag = true;
            }
            if (onemoreplayflag)
            {
                delta += Time.deltaTime;
            }
            if (delta > onemoreplayInterval)
            {
                gameState = GameState.Continue;
                onemoreplay.SetActive(true);
            }
        }
        if (gameState == GameState.Continue)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("StartScene");
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                GetComponent<AudioSource>().Play();
                cursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(-125, -125);
                gameState = GameState.End;
            }
        }
        else if (gameState == GameState.End)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Application.Quit();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                GetComponent<AudioSource>().Play();
                cursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(-125, -25);
                gameState = GameState.Continue;
            }
        }
	}
    //クリアorオーバーか引数で設定するメソッド
    public void SetClearOrOver(bool clearorover)
    {
        this.clearorover = clearorover;
    }
    //クリアorオーバーか渡すメソッド
    public bool GetClearOrOver()
    {
        return clearorover;
    }
}
