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

        [SerializeField]
        private Vector3 coldBoxScale;
        [SerializeField]
        private Vector3 coldBoxLocalPos;

        [SerializeField]
        private Vector3 hotBoxScale;
        [SerializeField]
        private Vector3 hotBoxLocalPos;

        void Start()
        {
            targetWindZone.SetState(MyWindZone.WindState.Cold);
            targetWindZone.transform.localScale = coldBoxScale;
            targetWindZone.transform.localPosition = coldBoxLocalPos;
        }

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
                    targetWindZone.StopColdWind();
                    StartCoroutine(WaitAndChangeState());
                    targetWindZone.transform.localScale = hotBoxScale;
                    targetWindZone.transform.localPosition = hotBoxLocalPos;
                    break;

                case COLD:
                default:
                    targetWindZone.SetState(MyWindZone.WindState.Cold);
                    targetWindZone.transform.localScale = coldBoxScale;
                    targetWindZone.transform.localPosition = coldBoxLocalPos;
                    break;
            }
        }

        IEnumerator WaitAndChangeState()
        {
            yield return new WaitForEndOfFrame();
            targetWindZone.SetState(MyWindZone.WindState.Hot);
        }
    }   
}
