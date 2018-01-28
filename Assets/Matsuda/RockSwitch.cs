using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSwitch : MonoBehaviour
{
    private bool touchrockswitch = false;

    public void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "ningen" || other.gameObject.tag == "Player")
        {
            touchrockswitch = true;
        }
    }
    public bool GetTouchRockSwitch()
    {
        return touchrockswitch;
    }
}
