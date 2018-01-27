using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rock : MonoBehaviour
{
    private float speed = -5.0f;
    private Vector3 moveDirection = Vector3.zero;
    public static bool isClear;
    Rigidbody rb;
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
    void Update()
    {
        if (GameObject.Find("RockSwitch").GetComponent<RockSwitch>().GetTouchRockSwitch() == true)
        {
            rb.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
        }
    }
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "ningen")
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
