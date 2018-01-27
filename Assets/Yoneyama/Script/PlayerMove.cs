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
	private Renderer renderer;
	private int bodyColor;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jump = 5f;
    [SerializeField] private Rigidbody rb;

    private bool isground = true;

    private float netudendou_syoki = 0.5f;

    [SerializeField] private float netudendou = 0.5f;
    private bool syozi = false;
    private bool respon_OK = false;

    [SerializeField] private GameObject netudendou_0_Obj;
    [SerializeField] private GameObject netudendou_50_Obj;
    [SerializeField] private GameObject netudendou_100_Obj;

    [SerializeField] private float ningen_idou_osoi = 0;
    [SerializeField] private float ningen_idou_nomale = 1;
    [SerializeField] private float ningen_idou_hayai = 2;

    private Vector3 windPower;

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

    void Start ()
    {
		renderer = this.gameObject.transform.Find ("Cube").GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        Objhakidasi();
        move_input();
        IceBreak();
    }

    private void FixedUpdate()
    {
        move();
    }

    private float move_x = 0;
    private float move_y = 0;

    void move_input()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            move_x = Input.GetAxis("Horizontal") * speed *Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.W) && isground == true)
        {
            move_y = jump;
            isground = false;
        }

    }

    /// <summary>
    /// プレイヤーの移動,ジャンプ
    /// </summary>
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

        if (netudendou == 0.0f)
        {
            Instantiate(netudendou_0_Obj, transform.position, transform.rotation);
            netudendou = netudendou_syoki;
            syozi = false;
            respon_OK = false;
        }

        else if (netudendou == 0.5f)
        {
            Instantiate(netudendou_50_Obj, transform.position, transform.rotation);
            netudendou = netudendou_syoki;
            syozi = false;
            respon_OK = false;
        }

        else if (netudendou == 1.0f)
        {
            Instantiate(netudendou_100_Obj, transform.position, transform.rotation);
            netudendou = netudendou_syoki;
            syozi = false;
            respon_OK = false;
        }
		StartCoroutine(changeBodyColor());
    }


    void IceBreak()
    {

        Ray ray = new Ray(new Vector3(transform.position.x,0,transform.position.z), new Vector3(1, 0, 0));

        RaycastHit hit;

        int distance = 2;

        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.collider.tag == "Ice")
            {
                hit.collider.gameObject.GetComponent<IceObj>().Tokeru(netudendou);
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Switch_IceGround" && netudendou == 0)
        {
            other.gameObject.GetComponent<IcsSwitch>().Create_IceGround();
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
