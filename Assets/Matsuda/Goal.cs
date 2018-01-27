using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Playerゴール！");
        }
    }
    public static bool GetIsClear()
    {
        return isClear;
    }
}
