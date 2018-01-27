using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTrailWindEffect : MonoBehaviour {
	public GameObject WindEffect;

	void Start()
	{
		switch (this.gameObject.name)
		{
		case "WindEffect01":
			GetComponent<Animator> ().Play ("WindEffect01");
			StartCoroutine (clearTrailRenderer ());
			break;
		case "WindEffect02":
			GetComponent<Animator> ().Play ("WindEffect02");
			StartCoroutine (clearTrailRenderer ());
			break;
		case "WindEffect03":
			GetComponent<Animator> ().Play ("WindEffect03");
			StartCoroutine (clearTrailRenderer ());
			break;
		case "WindEffect04":
			GetComponent<Animator> ().Play ("WindEffect04");
			StartCoroutine (clearTrailRenderer ());
			break;
		case "WindEffect05":
			GetComponent<Animator> ().Play ("WindEffect05");
			StartCoroutine (clearTrailRenderer ());
			break;
		case "WindEffect06":
			GetComponent<Animator> ().Play ("WindEffect06");
			StartCoroutine (clearTrailRenderer ());
			break;
		case "WindEffect07":
			GetComponent<Animator> ().Play ("WindEffect07");
			StartCoroutine (clearTrailRenderer ());
			break;
		case "WindEffect08":
			GetComponent<Animator> ().Play ("WindEffect08");
			StartCoroutine (clearTrailRenderer ());
			break;
		}

	}

	IEnumerator clearTrailRenderer()
	{
		switch (this.gameObject.name)
		{
		case "WindEffect01":
			yield return new WaitForSeconds (5.2f);
			GetComponent<Animator> ().Play ("WindEffect01");
			GetComponent<TrailRenderer> ().Clear ();
			StartCoroutine (clearTrailRenderer());
			break;
		case "WindEffect02":
			yield return new WaitForSeconds (5.2f);
			GetComponent<Animator> ().Play ("WindEffect02");
			GetComponent<TrailRenderer> ().Clear ();
			StartCoroutine (clearTrailRenderer());
			break;
		case "WindEffect03":
			yield return new WaitForSeconds (6.5f);
			GetComponent<Animator> ().Play ("WindEffect03");
			GetComponent<TrailRenderer> ().Clear ();
			StartCoroutine (clearTrailRenderer());
			break;
		case "WindEffect04":
			yield return new WaitForSeconds (6.5f);
			GetComponent<Animator> ().Play ("WindEffect04");
			GetComponent<TrailRenderer> ().Clear ();
			StartCoroutine (clearTrailRenderer());
			break;
		case "WindEffect05":
			yield return new WaitForSeconds (8.7f);
			GetComponent<Animator> ().Play ("WindEffect05");
			GetComponent<TrailRenderer> ().Clear ();
			StartCoroutine (clearTrailRenderer());
			break;
		case "WindEffect06":
			yield return new WaitForSeconds (8.7f);
			GetComponent<Animator> ().Play ("WindEffect06");
			GetComponent<TrailRenderer> ().Clear ();
			StartCoroutine (clearTrailRenderer());
			break;
		case "WindEffect07":
			yield return new WaitForSeconds (13f);
			GetComponent<Animator> ().Play ("WindEffect07");
			GetComponent<TrailRenderer> ().Clear ();
			StartCoroutine (clearTrailRenderer());
			break;
		case "WindEffect08":
			yield return new WaitForSeconds (13f);
			GetComponent<Animator> ().Play ("WindEffect08");
			GetComponent<TrailRenderer> ().Clear ();
			StartCoroutine (clearTrailRenderer());
			break;
		}
	}
}
