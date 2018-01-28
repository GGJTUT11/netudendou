using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Miyada
{
    /// <summary>
    /// クマのアニメーション管理.
    /// 
    /// Idle:待機
    /// Walk:歩行
    /// Attack:攻撃
    /// SleepStart:睡眠開始
    /// SleepEnd:睡眠終了
    /// </summary>
	public class BearAnimationController : MonoSingleton<BearAnimationController> {

        private Animation anim = null;

        private const string IdleName = "Arm_bear|idle_search";
        private const string WalkName = "Arm_bear|walk._1";
        private const string AttackName = "Arm_bear|attack_4";
        private const string SleepStartName = "Arm_bear|sleep_start";
        private const string SleepEndName = "Arm_bear|sleep_end";

        [SerializeField]
        private ParticleSystem sleepEffect;

    	// Use this for initialization
    	void Start () {
            anim = GetComponent<Animation>();
    	}
    	
        public void PlayIdleAnimation()
        {
            anim.CrossFade(IdleName);
        }

        public void PlayWalkAnimation()
        {
            anim.CrossFade(WalkName);
        }

        public void PlayAttackAnimation()
        {
            anim.CrossFade(AttackName);
        }

        public void PlaySleepStartAnimation()
        {
            anim.CrossFade(SleepStartName);
            Invoke("startSleepEffect", 1f);
        }

        public void PlaySleepEndAnimation()
        {
            anim.CrossFade(SleepEndName);
            endSleepEffect();
        }

        void startSleepEffect()
        {
            sleepEffect.Play();
        }

        void endSleepEffect()
        {
            sleepEffect.Stop();
        }
    }
}