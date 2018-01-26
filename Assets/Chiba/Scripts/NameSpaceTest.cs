using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chiba;

public class NameSpaceTest : MonoBehaviour {
	bool isMuteki = false;

	IEnumerator TestCoroutine(){
		yield return new WaitForSeconds (3f);
		isMuteki = false;
	} 

	void Damage(){
		if (isMuteki) {
			return;
		}
		Test.Instance.HP -= 10;
		isMuteki = true;
		StartCoroutine (TestCoroutine());
	}
}
