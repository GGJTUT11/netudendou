using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearSwitch : MonoBehaviour
{
    [SerializeField] private GameObject bearSwitch;
    private bool touchbearswitch = false;
    void Start()
    {

    }
    void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            touchbearswitch = true;
        }
    }
    public bool GetTouchBearSwitch()
    {
        return touchbearswitch;
    }
}
