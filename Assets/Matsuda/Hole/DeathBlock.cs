using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathBlock : MonoBehaviour
{
    public static bool isClear;
    void Start ()
    {

    }
	void Update ()
    {
		
	}
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "ningen")
        {
            isClear = false;
            SceneManager.LoadScene("EndScene");
        }
    }
    public static bool GetIsClear()
    {
        return isClear;
    }
}
