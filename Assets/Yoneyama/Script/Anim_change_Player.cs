using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_change_Player : MonoSingleton<Anim_change_Player> {

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void walk_change()
    {
        anim.SetBool("walk", true);
    }

    public void idol_change()
    {
        anim.SetBool("walk", false);
    }

    public void kyusyu_change()
    {
        anim.SetTrigger("kyusyu");
    }


}
