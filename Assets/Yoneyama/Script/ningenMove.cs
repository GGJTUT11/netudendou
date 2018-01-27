using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ningenMove : MonoSingleton<ningenMove>
{

    public float speed = 1f;
    Rigidbody rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        rb.velocity = new Vector3(speed, 0f, 0f);

    }

}
