using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    private float speed = 1.0f;
    private Vector3 moveDirection = Vector3.zero;
    private float z = 2.0f;
    int layer_Player;
    int layer_Bear;
    int layer_ningen;
    bool collisionflag = true;
    void Start()
    {
        layer_Player = LayerMask.NameToLayer("Player");
        layer_Bear = LayerMask.NameToLayer("bear");
        layer_ningen = LayerMask.NameToLayer("ningen");
    }
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (GameObject.Find("BearSwitch").GetComponent<BearSwitch>().GetTouchBearSwitch() == true)
        {
            moveDirection = new Vector3(0, 0, z);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            controller.Move(moveDirection * Time.deltaTime);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("A");
        if (other.transform.tag == "ningen" && collisionflag == true)
        {
            z = 0;
            Debug.Log("GameOver");
        }
        else if (other.transform.tag == "Player" && GameObject.Find("Player").GetComponent<PlayerMove>().Netudendou_Property == 0.0f)
        {
            z = 0;
            collisionflag = false;
            Physics.IgnoreLayerCollision(layer_Player, layer_Bear);
            Physics.IgnoreLayerCollision(layer_ningen, layer_Bear);
        }
    }
}