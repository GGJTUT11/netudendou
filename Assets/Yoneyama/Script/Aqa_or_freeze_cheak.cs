using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aqa_or_freeze_cheak : MonoBehaviour {
	
	void Update ()
    {
        if (Aqa_or_freeze.Instance.freeze) gameObject.SetActive(false);
	}

}
