using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_change : MonoSingleton<Anim_change>
{

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void walk_change()
    {
        anim.SetTrigger("walk");
    }

    public void idol_change()
    {
        anim.SetTrigger("idol");
    }

}
