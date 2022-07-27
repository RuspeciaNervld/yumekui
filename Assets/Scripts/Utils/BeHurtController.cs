using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeHurtController : MonoBehaviour
{
    private ICreature user;

    private float hurtColdTime;
    private float hurtColdTimeCounter;
    private float hurtRecoverTime;
    private float hurtRecoverTimeCounter;

    /// <summary>
    /// 初始化受伤控制器
    /// </summary>
    /// <param name="hurtColdTime">受伤后的无敌时间</param>
    /// <param name="hurtRecoverTime">受伤后的硬直时间</param>
    internal void init(float hurtColdTime,float hurtRecoverTime,ICreature user) {
        this.hurtColdTime = hurtColdTime;
        this.hurtRecoverTime = hurtRecoverTime;
        this.hurtColdTimeCounter = 0;
        this.hurtRecoverTimeCounter = 0;
        this.user = user;
    }

    /// <summary>
    /// 受伤函数
    /// </summary>
    /// <param name="computedAttack">通过攻击方计算出的“计算伤害”</param>
    /// <param name="beHurtAction">受伤回调函数</param>
    /// <returns>受到实际伤害返回true，否则为false</returns>
    public bool beHurt(float computedAttack) {
        if (hurtColdTimeCounter > 0) {
            return false;
        }
        if (computedAttack != 0 && user.canBeHurt) {
            user.hp -= computedAttack * user.accept;
            user.beHurtAction();
        }
        hurtColdTimeCounter = hurtColdTime; // 受到伤害，受伤冷却重置
        hurtRecoverTimeCounter = hurtRecoverTime; // 受到伤害，开始硬直
        return true;
    }


    public bool isRecovering() {
        return hurtRecoverTimeCounter > 0;
    }


    private void Update() {
        hurtColdTimeCounter -= Time.deltaTime;
        hurtRecoverTimeCounter -= Time.deltaTime;
    }

}
