using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceObj : MonoBehaviour
{

    [SerializeField] private float HP = 10f;
    //[SerializeField] private Vector3 offset_pos = new Vector3(0, 0, 0);

    //private void Start()
    //{
    //    offset_pos = transform.position;
    //}


    public void Tokeru(float dendouritu)
    {
        if (dendouritu == 1.0f)
        {
            HP -= 1f;
            transform.localScale -= new Vector3(0, transform.localScale.y / 10, 0) ;
            transform.position = new Vector3(transform.position.x, transform.position.y / 10, transform.position.z);

        }
        if (HP < 0)
        {
            Destroy(gameObject);
        }
    }

}
