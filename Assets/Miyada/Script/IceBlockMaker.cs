using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlockMaker : MonoBehaviour {

    [SerializeField]
    private GameObject waterBlock;

    [SerializeField]
    private GameObject iceBlock;

    [SerializeField]
    private ParticleSystem fogEffect;

    [SerializeField]
    private float freezeTime = 3f;

    private float timer = 0;

    public void Reset()
    {
        timer = 0;
    }

    void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<PlayerMove>();
        if (!player) return;

        timer += Time.deltaTime;

        fogEffect.Play();

        if(timer >= freezeTime) {
            waterBlock.SetActive(false);
            iceBlock.SetActive(true);
            fogEffect.Stop();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<PlayerMove>();
        if (!player) return;

        fogEffect.Stop();
    }
}
