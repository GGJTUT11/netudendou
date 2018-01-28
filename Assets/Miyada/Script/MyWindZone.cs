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

        public WindState WindStateProp
        {
            set { windState = value; }
        }

        private ningenMove targetHuman;

        [Header(" ----- Cold ----- ")]
        [SerializeField]
        private float changeSpeedTime = 1.0f;

        [SerializeField]
        private ParticleSystem horizonParticle = null;

        [Header(" ----- Hot ----- ")]
        [SerializeField]
        private Vector3 hotWindForce = Constants.Vector3Zero;

        [SerializeField]
        private ParticleSystem verticalParticle = null;

        private Animator[] horAnimators;
        private Animator[] verAnimators;

        private const float PlaySpeed = 1.0f;
        private const float StopSpeed = 0.0f;

        #region UnityCallback

        private void Start()
        {
            if (horizonParticle)
            {
                horizonParticle.Play();
                verticalParticle.Stop();

                horAnimators = horizonParticle.GetComponentsInChildren<Animator>();
                verAnimators = verticalParticle.GetComponentsInChildren<Animator>();

                foreach (var anim in horAnimators)
                {
                    anim.speed = PlaySpeed;
                }

                foreach (var anim in verAnimators)
                {
                    anim.speed = StopSpeed;
                }   
            }
        }

        private void OnTriggerEnter(Collider col)
        {
            const string HumanLayerName = "ningen";
            if (col.gameObject.layer != (int)LayerMask.NameToLayer(HumanLayerName)) return;

            switch (windState)
            {
                case WindState.Cold:
                case WindState.Neutral:
                    blowColdWind(col);
                    break;
            }
        }

        void OnTriggerStay(Collider col)
        {
            const string HumanLayerName = "ningen";
            if (col.gameObject.layer != (int)LayerMask.NameToLayer(HumanLayerName)) return;

            switch (windState)
            {
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
            var human = col.GetComponent<ningenMove>();
            if (!human) return;
            human.MakeStop(changeSpeedTime);
            targetHuman = human;

            if(horizonParticle)
            {
                horizonParticle.Play();
                verticalParticle.Stop();

                foreach (var anim in horAnimators)
                {
                    anim.speed = PlaySpeed;
                }

                foreach (var anim in verAnimators)
                {
                    anim.speed = StopSpeed;
                }   
            }
        }

        public void StopColdWind()
        {
            if (!targetHuman) return;
            targetHuman.MakeMove(changeSpeedTime);
            targetHuman = null;
        }

        void blowHotWind(Collider col)
        {
            Debug.Log("hot");
            var human = col.GetComponent<ningenMove>();
            if (!human) return;
            human.AddWind(hotWindForce);

            if(horizonParticle) {
                horizonParticle.Stop();
                verticalParticle.Play();

                foreach (var anim in horAnimators)
                {
                    anim.speed = StopSpeed;
                }

                foreach (var anim in verAnimators)
                {
                    anim.speed = PlaySpeed;
                }   
            }
        }
        #endregion // Private Methods
    }
}