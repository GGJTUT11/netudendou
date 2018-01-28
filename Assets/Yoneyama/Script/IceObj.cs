using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceObj : MonoBehaviour
{

    [SerializeField] private float HP = 10f;
    private float Ice_tokeru = 0;
    private float tokeruritu = 0;

    private void Start()
    {
        Ice_tokeru = HP;
        tokeruritu = transform.localScale.y / Ice_tokeru;

    }

    public void Tokeru(float dendouritu)
    {
        //Debug.Break();

        if (dendouritu == 1.0f)
        {
            HP -= 1f;
            transform.localScale -= new Vector3(0, tokeruritu, 0) ;
            transform.position = new Vector3(transform.position.x, transform.position.y - tokeruritu, transform.position.z);

        }
        if (HP < Ice_tokeru / 2.5)
        {
            Destroy(gameObject);
        }
    }

}
