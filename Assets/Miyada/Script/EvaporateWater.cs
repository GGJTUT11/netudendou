using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Miyada
{
    public class EvaporateWater : MonoBehaviour {

    [SerializeField]
    private float lifeTime = 5.0f;
    private const float MinScaleY = 0.05f;

    private float evaporateAmount = 0.0f;

    [SerializeField]
    private ParticleSystem smokeEffect;

    private bool willDestroy = false;

    // Use this for initialization
    void Start () {
        evaporateAmount = (transform.localScale.y - MinScaleY) / lifeTime * Time.deltaTime;
    }

    public void Evaporate()
    {
        if (willDestroy) return;

        var curScale = transform.localScale;
        curScale.y -= evaporateAmount;
        transform.localScale = curScale;

        transform.Translate(0, -evaporateAmount * 0.5f, 0);

        smokeEffect.transform.Translate(0, -evaporateAmount * 0.5f, 0);
        smokeEffect.Play();

        if( transform.localScale.y <= MinScaleY)
        {
            StopEffect();
            DestroyEffect();
            willDestroy = true;
        }
    }

    public void StopEffect()
    {
        smokeEffect.Stop();
    }

    public void DestroyEffect()
    {
        Destroy(smokeEffect.gameObject, 2f);
        Destroy(gameObject);
    }
}

}