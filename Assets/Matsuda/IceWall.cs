using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && GameObject.Find("Player").GetComponent<PlayerMove>().Netudendou_Property == 1.0f)
        {
            Destroy(gameObject);
        }
    }
}
