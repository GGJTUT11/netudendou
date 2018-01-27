using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Miyada
{
    /// <summary>
    /// WindZoneの気流を切り替える.
    /// </summary>
    public class WindZoneSwitcher : MonoBehaviour
    {
        void OnTriggerEnter(Collider col)
        {
            var player = col.GetComponent<PlayerMove>();
            if (!player) return;

            const float hot = 1.0f;
            const float cold = 0.0f;
        }
    }   
}
