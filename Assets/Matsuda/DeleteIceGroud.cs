using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteIceGroud : MonoBehaviour {
    private GameObject icegroundhole;
	// Use this for initialization
	void Start () {
        icegroundhole = GameObject.Find("IceGroundOnHole");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && GameObject.Find("Player").GetComponent<PlayerMove>().Netudendou_Property == 1.0f)
        {
            Destroy(icegroundhole.gameObject);
        }
    }
}
