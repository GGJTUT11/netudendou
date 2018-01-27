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

    [SerializeField] private GameObject netudendou_0_Obj;
    [SerializeField] private GameObject netudendou_50_Obj;
    [SerializeField] private GameObject netudendou_100_Obj;

    [SerializeField] private float ningen_idou_osoi = 0;
    [SerializeField] private float ningen_idou_nomale = 1;
    [SerializeField] private float ningen_idou_hayai = 2;

    public float Netudendou_Property
    {
        get
        {
            return netudendou;
        }
    }

    IEnumerator waitrespon()
    {
        yield return new WaitForSeconds(0.5f);
        respon_OK = true;
    }

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        move();
        Objhakidasi();
    }

    /// <summary>
    /// プレイヤーの移動,ジャンプ
    /// </summary>
    void move()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, rb.velocity.y, 0f);
        }

        if (Input.GetKeyDown(KeyCode.W) && isground == true)
        {
            rb.velocity = new Vector3(0f, jump, 0f);
            isground = false;
        }
    }

    /// <summary>
    /// Objのイジェクト
    /// </summary>
    void Objhakidasi()
    {
        if (Input.GetKeyDown(KeyCode.Space) && syozi == true && netudendou == 0.0f && respon_OK == true)
        {
            Instantiate(netudendou_0_Obj, transform.position, transform.rotation);
            netudendou = netudendou_syoki;
            syozi = false;
            respon_OK = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && syozi == true && netudendou == 0.5f && respon_OK == true)
        {
            Instantiate(netudendou_50_Obj, transform.position, transform.rotation);
            netudendou = netudendou_syoki;
            syozi = false;
            respon_OK = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && syozi == true && netudendou == 1.0f && respon_OK == true)
        {
            Instantiate(netudendou_100_Obj, transform.position, transform.rotation);
            netudendou = netudendou_syoki;
            syozi = false;
            respon_OK = false;
        }
		StartCoroutine(changeBodyColor());
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
           
            if (netudendou == 0) ningenMove.Instance.speed = ningen_idou_osoi;
            if (netudendou == 0.5) ningenMove.Instance.speed = ningen_idou_nomale;
            if (netudendou == 1) ningenMove.Instance.speed = ningen_idou_hayai;   
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


    void OnCollisionStay(Collision collision)
    {
       
        //今は触れ続けるために移動し続けなければいけない→どうにかしたい
        if (collision.transform.tag == "Ice")
        {

            IceObj.Instance.Tokeru(netudendou);
        }
    }

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
