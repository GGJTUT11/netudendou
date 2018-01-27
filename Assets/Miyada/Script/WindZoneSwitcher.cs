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
        [SerializeField]
        private MyWindZone targetWindZone;

        void OnTriggerEnter(Collider col)
        {
            var player = col.GetComponent<PlayerMove>();
            if (!player) return;

            const string PlayerLayerName = "Player";
            if (player.gameObject.layer != (int)LayerMask.NameToLayer(PlayerLayerName)) return;

            const int HOT = 1;
            const int COLD = 0;

            // これはひどい
            // ニュートラルは冷たい扱いなのでこうしますがががが
            switch((int)player.Netudendou_Property)
            {
                case HOT:
                    targetWindZone.SetState(MyWindZone.WindState.Hot);
                    break;

                case COLD:
                default:
                    targetWindZone.SetState(MyWindZone.WindState.Cold);
                    break;
            }
        }
    }   
}
