using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Miyada
{
    public class EvaporateTrigger : MonoBehaviour {

        [SerializeField]
        private EvaporateWater water;

        [SerializeField]
        private GameObject windzone;

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<PlayerMove>();
            if (!player) return;

            // 風生成
            windzone.SetActive(true);
        }

        private void OnTriggerExit(Collider other)
        {
            var player = other.GetComponent<PlayerMove>();
            if (!player) return;

            // 風消.
            windzone.SetActive(false);
        }

        private void OnTriggerStay(Collider other)
        {
            var player = other.GetComponent<PlayerMove>();
            if (!player) return;

            var netu = (int)player.Netudendou_Property;
            const int HOT = 1;

            if(netu == HOT) {
                water.Evaporate();
            }
        }
    }
}
