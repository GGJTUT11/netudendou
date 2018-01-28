using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miyada;
using UnityEngine.SceneManagement;

public class Bear : MonoBehaviour
{
    private float speed = 1.0f;
    private Vector3 moveDirection = Vector3.zero;
    private float z = 2.0f;
    int layer_Player;
    int layer_Bear;
    int layer_ningen;
    bool collisionflag = true;
	[HideInInspector] public static bool isClear; 

    void Start()
    {
		BearAnimationController.Instance.PlayIdleAnimation ();
        layer_Player = LayerMask.NameToLayer("Player");
        layer_Bear = LayerMask.NameToLayer("bear");
        layer_ningen = LayerMask.NameToLayer("ningen");
    }
    void Update()
    {
        if (GameObject.Find("BearSwitch").GetComponent<BearSwitch>().GetTouchBearSwitch() == true)
        {
			moveBear ();
        }
    }

	private void moveBear()
	{
		if (!collisionflag || z == 0)
		{
			return;
		}
		CharacterController controller = GetComponent<CharacterController>();
		moveDirection = new Vector3(0, 0, z);
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;
		controller.Move(moveDirection * Time.deltaTime);
		BearAnimationController.Instance.PlayWalkAnimation ();
	}

    public void OnTriggerEnter(Collider other)
    {
		if (!collisionflag) {
			return;
		}

        if (other.transform.root.tag == "ningen")
        {
            z = 0;
			isClear = false;
			StartCoroutine (waitEndGame());
			BearAnimationController.Instance.PlayAttackAnimation ();
        }
        else if (other.transform.root.tag == "Player" && GameObject.Find("Player").GetComponent<PlayerMove>().Netudendou_Property == 0.0f)
        {
            z = 0;
			Debug.Log("冬眠");
            collisionflag = false;
            Physics.IgnoreLayerCollision(layer_Player, layer_Bear);
            Physics.IgnoreLayerCollision(layer_ningen, layer_Bear);
			GetComponent<CharacterController> ().enabled = false;
			GetComponent<BoxCollider> ().enabled = false;
			BearAnimationController.Instance.PlaySleepStartAnimation ();
        }
    }

	IEnumerator waitEndGame()
	{
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene ("EndScene");
	}

	public static bool GetIsClear()
	{
		return isClear;
	}
}