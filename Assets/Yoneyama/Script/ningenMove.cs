using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ningenMove : MonoSingleton<ningenMove>
{

    public float speed = 1f;
    [HideInInspector] public float speed_ = 0;
    private float timer = 0;
    private float initTime = 0;
    private bool makeMove = false;

    Rigidbody rb;

    private Vector3 windPower;

    private Vector3 offset_point = new Vector3 (0,0,0);

    private bool kabeharituki = true;

    private Vector3 ningen = new Vector3(0, 0, 0);

	[SerializeField] private GameObject ningenRootBody;
	[SerializeField] bool isFastSpeed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed_ = speed;
        offset_point = transform.position;
        ningen = transform.position;
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

	float changeTimer = 0;
	void Update(){
		if (!isFastSpeed) {
			changeTimer += Time.deltaTime;
			if (changeTimer > 2f) {
				Anim_change.Instance.walk_change ();
				isFastSpeed = true;
				changeTimer = 0;
			}
		} else {
			changeTimer = 0;
		}
	}

	Vector3 beforeMove;
    void FixedUpdate()
    {
		beforeMove = transform.localPosition;
		var clipInfo = ningenRootBody.GetComponent<Animator> ().GetCurrentAnimatorClipInfo (0);
		var clip = clipInfo [0];
		switch (clip.clip.name)
		{
		case "WALK00_L":
			isFastSpeed = true;
			break;
		case "WAIT01":
			isFastSpeed = false;
			break;
		}

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
			if (isFastSpeed) {
				rb.velocity = new Vector3 (speed_, rb.velocity.y, 0f);
			} else {
				rb.velocity = new Vector3 (0, rb.velocity.y, 0f);
			}
        }
		StartCoroutine (checkNingenDistance (beforeMove));
    }

	IEnumerator checkNingenDistance(Vector3 pos){
		yield return new WaitForSeconds (0.5f);
		pos.y = transform.localPosition.y;
		if (Mathf.Abs (Vector3.Distance (transform.localPosition, pos)) * 1000 < 1) {
			Anim_change.Instance.idol_change ();
		} else {
			Anim_change.Instance.walk_change ();
		}
	}
}
