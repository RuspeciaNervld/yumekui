using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackController : MonoBehaviour
{
    private float hurtTime;
    private float endTime;
    private float currentTime;

    private UnityAction startAction;
    private UnityAction hurtAction;
    private UnityAction endAction;
    private bool isAttacking = false;

    //! 伴随生物诞生即初始化
    public void init() {
        this.hurtTime = 0;
        this.endTime = 0;
        this.currentTime = 0;

        this.startAction = null;
        this.hurtAction = null;
        this.endAction = null;

        this.isAttacking = false;
    }

    /// <summary>
    /// 自定义技能并开启（伤害时间、结束时间；伤害动作、结束动作）
    /// 开始则通过判断doAttack的返回值来进行
    /// </summary>
    /// <param name="hurtTime">伤害作用时间</param>
    /// <param name="endTime">攻击结束时间（即加上后摇）</param>
    /// <param name="hurtAction">伤害动作函数->（例如）计算数值完成伤害响应</param>
    /// <param name="endAction">结束动作函数->（例如）恢复正常状态或做判定等</param>
    /// <returns>攻击释放成功则true，否则false</returns>
    public bool doAttack(float hurtTime,float endTime,UnityAction hurtAction,UnityAction endAction) {
        if (this.isAttacking) {
            return false;
        }
        this.isAttacking = true;
        this.hurtTime = hurtTime;
        this.endTime = endTime;
        this.currentTime = 0;
        this.hurtAction = hurtAction;
        this.endAction= endAction;
        return true;
    }

    // Update is called once per frame
    private void Update() {
        if (!this.isAttacking) {
            return;
        }

        if (startAction != null) {
            this.startAction();
            this.startAction = null;
        } ;

        currentTime += Time.deltaTime;

        if (currentTime >= this.hurtTime) {
            if (this.hurtAction!=null) {
                this.hurtAction();
                this.hurtAction = null;
            }
        }
        if (currentTime >= this.endTime) {
            if (this.endAction != null) {
                this.endAction();
                this.endAction = null;
            }
            this.isAttacking = false;
        }
    }
}
