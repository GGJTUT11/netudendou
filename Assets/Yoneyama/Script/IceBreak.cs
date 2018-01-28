using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBreak : MonoBehaviour {

    [SerializeField] private GameObject player;
    private float netudendou = 0.5f;
    private bool oneshot = true;

    IEnumerator waitshot()
    {
        yield return new WaitForSeconds(2.0f);
        oneshot = true;
    }

    void Update () {

        netudendou = player.GetComponent<PlayerMove>().Netudendou_Property;
        icebreak();
	}


    void icebreak()
    {

        Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(1, 0, 0));

        RaycastHit hit;

        int distance = 2;

        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.collider.tag == "Ice")
            {
                hit.collider.gameObject.GetComponent<IceObj>().Tokeru(netudendou);
                if (oneshot)
                {
                    SoundManager.Instance.soundshot(1);
                    oneshot = false;
                    StartCoroutine(waitshot());
                }
            }

        }
    }
}
