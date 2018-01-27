using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ningenMove : MonoSingleton<ningenMove>
{

    public float speed = 1f;
    private float speed_ = 0;
    private float timer = 0;
    private float initTime = 0;
    private bool makeMove = false;

    Rigidbody rb;

    private Vector3 windPower;

    private Vector3 offset_point = new Vector3 (0,0,0);

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed_ = speed;
        offset_point = transform.position;
    }

    public void AddWind(Vector3 wind)
    {
        windPower = wind;
    }

    public void MakeMove(float time)
    {
        makeMove = true;
        timer = time;
    }

    public void MakeStop(float time)
    {
        makeMove = false;
        timer = initTime = time;
    }

    void FixedUpdate()
    {
        if (timer > 0.0f)
        {
            // 0.0 ~ 1.0
            var curRate = 1.0f - (initTime - timer) / initTime;

            // 向かい風受けはじめ.
            if (makeMove)
            {
                speed_ = Mathf.Lerp(speed, 0, curRate);
            }
            else
            {
                speed_ = Mathf.Lerp(0, speed, curRate);
            }

            rb.velocity = new Vector3(speed_, rb.velocity.y, 0f);
            timer -= Time.deltaTime;
            windPower = Miyada.Constants.Vector3Zero;

            // 完了.
            if (timer <= 0.0f)
            {
                speed_ = makeMove ? speed : 0f;
            }
        }
        else if(windPower != Miyada.Constants.Vector3Zero)
        {
            // 上昇気流を受けている場合.
            rb.velocity = new Vector3(speed_, 0f, 0f) + windPower;
            windPower = Miyada.Constants.Vector3Zero;
        }
        else
        {
            // 通常状態.
            rb.velocity = new Vector3(speed_, rb.velocity.y, 0f);
        }

        if (transform.position == offset_point)
        {
            Anim_change.Instance.idol_change();
        }
        else
        {
            Anim_change.Instance.walk_change();
        }
        offset_point = transform.position;

    }

}
