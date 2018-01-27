using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    GameObject plane3;
    GameObject rock;
	void Start ()
    {
        plane3 = transform.Find("Plane3").gameObject;
        rock = transform.Find("Rock").gameObject;
	}
	void Update ()
    {
		
	}
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Rock")
        {
            Destroy(other.gameObject);
            plane3.SetActive(true);
            rock.SetActive(true);
        }
    }
}
