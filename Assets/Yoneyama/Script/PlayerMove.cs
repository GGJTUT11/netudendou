using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jump = 5f;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float netudendou_syoki = 0.5f;

    [SerializeField] private float netudendou = 0.5f;
    private bool syozi = false;
    private bool respon_OK = false;

    [SerializeField] private GameObject netudendou_0_Obj;
    [SerializeField] private GameObject netudendou_50_Obj;
    [SerializeField] private GameObject netudendou_100_Obj;



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
    /// プレイヤーの移動
    /// </summary>
    void move()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, rb.velocity.y, 0f);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = new Vector3(0f, jump, 0f);
        }
    }

    void Objhakidasi()
    {
        if (Input.GetKeyDown(KeyCode.Space) && syozi == true && netudendou == 0.0f && respon_OK == true)
        {
            Instantiate(netudendou_0_Obj, transform.position, transform.rotation);
            syozi = false;
            respon_OK = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && syozi == true && netudendou == 0.5f && respon_OK == true)
        {
            Instantiate(netudendou_50_Obj, transform.position, transform.rotation);
            syozi = false;
            respon_OK = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && syozi == true && netudendou == 1.0f && respon_OK == true)
        {
            Instantiate(netudendou_100_Obj, transform.position, transform.rotation);
            syozi = false;
            respon_OK = false;
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
            }

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



}
