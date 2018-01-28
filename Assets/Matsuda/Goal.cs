using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public static bool isClear;
    void Start ()
    {
		
	}
	void Update ()
    {
		
	}
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ningen")
        {
            isClear = true;
			SceneManager.LoadScene ("EndScene");
        }
    }
    public static bool GetIsClear()
    {
        return isClear;
    }
}
