using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Miyada
{
    /// <summary>
    /// タスク：空気を温めて上昇気流
    /// 
    /// 人間のRigidbodyに干渉して進めないようにしたり上昇させたりする
    /// </summary>
    public class MyWindZone : MonoBehaviour
    {
        public enum WindState { Cold, Neutral, Hot }
        private WindState windState = WindState.Cold; 

        [Header(" ----- Cold ----- ")]
        [SerializeField]
        private Vector3 coldWindForce = Constants.Vector3Zero;

        [Header(" ----- Hot ----- ")]
        [SerializeField]
        private Vector3 hotWindForce = Constants.Vector3Zero;

        #region UnityCallback
        void OnTriggerStay(Collider col)
        {
            const string HumanLayerName = "ningen";
            if (col.gameObject.layer != (int)LayerMask.NameToLayer(HumanLayerName)) return;

            switch (windState)
            {
                case WindState.Cold:
                case WindState.Neutral:
                    blowColdWind(col);
                    break;

                case WindState.Hot:
                    blowHotWind(col);
                    break;
            }
        }
        #endregion // UnityCallback

        #region Public Methods
        /// <summary>
        /// 気流の状態を設定.
        /// </summary>
        /// <param name="state">State.</param>
        public void SetState(WindState state)
        {
            windState = state;
        }
        #endregion // Public Methods

        #region Private Methods
        void blowColdWind(Collider col)
        {
            var humanRb = col.GetComponent<Rigidbody>();
            humanRb.AddForce(coldWindForce);

            // TODO:あとでエフェクト切り替え追加.
        }

        void blowHotWind(Collider col)
        {
            var humanRb = col.GetComponent<Rigidbody>();
            humanRb.AddForce(hotWindForce);

            // TODO:あとでエフェクト切り替え追加.
        }
        #endregion // Private Methods
    }
}