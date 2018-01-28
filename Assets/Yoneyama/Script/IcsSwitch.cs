using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcsSwitch : MonoSingleton<IcsSwitch>
{

    [SerializeField] GameObject Ice_Ground;

    public void Create_IceGround()
    {
        Ice_Ground.SetActive(true);
    }

    public void Destory_IceGround()
    {
        Ice_Ground.SetActive(false);
    }

}
