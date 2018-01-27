using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSwitch : MonoBehaviour
{
    [SerializeField] private GameObject rockSwitch;
    private bool touchrockswitch = false;
	void Start ()
    {

	}
	void Update ()
    {
		
	}
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            touchrockswitch = true;
        }
    }
    public bool GetTouchRockSwitch()
    {
        return touchrockswitch;
    }
}
