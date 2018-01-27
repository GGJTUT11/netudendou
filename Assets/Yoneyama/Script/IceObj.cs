using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceObj : MonoSingleton<IceObj>
{

    [SerializeField] private float HP = 10f; 

	public void Tokeru(float dendouritu)
    {
        if (dendouritu == 1.0f)
        {
            HP -= 1f;
        }
        if (HP < 0)
        {
            Destroy(gameObject);
        }
    }

}
