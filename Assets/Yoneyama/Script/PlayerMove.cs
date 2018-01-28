using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
	private enum colorFlag
	{
		blue = 0,
		yellow = 1,
		red = 2
	}

	[SerializeField] private Renderer renderer;
	private int bodyColor;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jump = 5f;
    [SerializeField] private Rigidbody rb;

    private bool isground = true;

    private float netudendou_syoki = 0.5f;

    [SerializeField] private float netudendou = 0.5f;
    private bool syozi = false;
    private bool respon_OK = false;
    private bool move_OK = true;

    [SerializeField] private GameObject netudendou_0_Obj;
    [SerializeField] private GameObject netudendou_50_Obj;
    [SerializeField] private GameObject netudendou_100_Obj;

    [SerializeField] private float ningen_idou_osoi = 0;
    [SerializeField] private float ningen_idou_nomale = 1;
    [SerializeField] private float ningen_idou_hayai = 2;

    private float move_x = 0;
    private float move_y = 0;

    private Vector3 windPower;

    public Vector3 diff = new Vector3(0,0,1f);

    private AudioSource audiosource;
    private float SongTime;
    private float song_count;
    private bool songstart = false;

    public float Netudendou_Property
    {
        get
        {
            return netudendou;
        }
    }

    public Vector3 WindPower
    {
        set
        {
            windPower = value;
        }
    }

    IEnumerator waitrespon()
    {
        yield return new WaitForSeconds(0.5f);
        respon_OK = true;
    }

    IEnumerator waitmove()
    {
        yield return new WaitForSeconds(0.5f);
        move_OK = true;
    }

    void Start()
    {
        //		renderer = this.gameObject.transform.Find ("Cube").GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
        audiosource.clip = SoundManager.Instance.audioClip[5];
        SongTime = audiosource.clip.length;
    }
	
	void Update ()
    {
        Objhakidasi();
        move_input();
        if (Mathf.Abs(move_x) < 0.1) audiosource.Stop();
    }

    private void FixedUpdate()
    {
        move();
    }



    void move_input()
    {
        if (!move_OK) return;
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                move_x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
                Anim_change_Player.Instance.walk_change();
                diff.z = -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                move_x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
                Anim_change_Player.Instance.walk_change();
                diff.z = 1;
            }

            if (Input.GetKeyDown(KeyCode.W) && isground == true)
            {
                move_y = jump;
                isground = false;
                move_sound();
            }
            move_sound();
        }

        if (Mathf.Abs(move_x) < 0.1) Anim_change_Player.Instance.idol_change(); ;
        transform.rotation = Quaternion.LookRotation(diff);
    }

    
    void move_sound()
    {
        if (songstart) audiosource.Play();
        if (song_count > SongTime)
        {
            audiosource.Play();
            song_count = 0;
        }
        song_count += Time.deltaTime;
    }

    void move()
    {
        rb.velocity = new Vector3(move_x * speed, rb.velocity.y , 0f) + windPower;

        if(move_y != 0)
        {
            rb.velocity += Vector3.up * move_y;
            move_y = 0;
        }

        move_x = 0;
    }

    /// <summary>
    /// Objのイジェクト
    /// </summary>
    void Objhakidasi()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        if (!syozi) return;
        if (!respon_OK) return;

        Anim_change_Player.Instance.kyusyu_change();
        SoundManager.Instance.soundshot(4);

        if (netudendou == 0.0f)
        {
            Instantiate(netudendou_0_Obj, transform.position, transform.rotation);
        }

        else if (netudendou == 0.5f)
        {
            Instantiate(netudendou_50_Obj, transform.position, transform.rotation);
        }

        else if (netudendou == 1.0f)
        {
            Instantiate(netudendou_100_Obj, transform.position, transform.rotation);
        }

        if (move_OK) move_OK = false;
        StartCoroutine(waitmove());

        netudendou = netudendou_syoki;
        syozi = false;
        respon_OK = false;
        StartCoroutine(changeBodyColor());
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Switch_IceGround" && netudendou == 0)
        {
            other.gameObject.GetComponent<IcsSwitch>().Create_IceGround();
            Aqa_or_freeze.Instance.freeze = true;
        }
        if (other.transform.tag == "Switch_IceGround" && netudendou == 1)
        {
            other.gameObject.GetComponent<IcsSwitch>().Destory_IceGround();
        }
        if (other.transform.tag == "ningen")
        {
            
            if (netudendou == 0) ningenMove.Instance.speed_ = ningen_idou_osoi;
            if (netudendou == 0.5) ningenMove.Instance.speed_ = ningen_idou_nomale;
            if (netudendou == 1) ningenMove.Instance.speed_ = ningen_idou_hayai;   
        }


    }



    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Weathenium")
        {

            if (Input.GetKeyDown(KeyCode.Space) && syozi == false)
            {
                SoundManager.Instance.soundshot(4);
                Anim_change_Player.Instance.kyusyu_change();

                if (move_OK) move_OK = false;
                StartCoroutine(waitmove());

                netudendou = other.GetComponent<WeatheniumStatus>().netudendou;
                syozi = true;
                Destroy(other.gameObject);
                StartCoroutine(waitrespon());
                StartCoroutine(changeBodyColor());
            }

        }
    }

     void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            isground = true;
        }
    }



    //void OnCollisionStay(Collision collision)
    //{
       
    //    //今は触れ続けるために移動し続けなければいけない→どうにかしたい→レイで判定
    //    if (collision.transform.tag == "Ice")
    //    {
    //        collision.gameObject.GetComponent<IceObj>().Tokeru(netudendou);
    //    }
    //}

	/// <summary>
	/// テンプリンの色が変わる処理
	/// </summary>
	private IEnumerator changeBodyColor()
	{
		float changeTimer = 0f;
		if (netudendou == 0.0f) {
			bodyColor = (int)colorFlag.blue;
		} else if (netudendou == 0.5f) {
			bodyColor = (int)colorFlag.yellow;
		} else {
			bodyColor = (int)colorFlag.red;
		}

		switch (bodyColor) 
		{
		case (int)colorFlag.blue:
			while (true) {
				yield return new WaitForEndOfFrame ();
				if (renderer.material.color	== Color.blue) {
					break;
				}
				changeTimer += Time.deltaTime;
				renderer.material.color	= Color.Lerp (renderer.material.color, Color.blue, changeTimer);
			}
			break;
		case (int)colorFlag.yellow:
			while (true) {
				yield return new WaitForEndOfFrame ();
				if (renderer.material.color	== Color.yellow) {
					break;
				}
				changeTimer += Time.deltaTime;
				renderer.material.color	= Color.Lerp (renderer.material.color, Color.yellow, changeTimer);
			}
			break;
		case (int)colorFlag.red:
			while (true) {
				yield return new WaitForEndOfFrame ();
				if (renderer.material.color	== Color.red) {
					break;
				}
				changeTimer += Time.deltaTime;
				renderer.material.color	= Color.Lerp (renderer.material.color, Color.red, changeTimer);
			}
			break;
		}
	}

}
