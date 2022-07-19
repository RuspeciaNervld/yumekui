using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICreature : MonoBehaviour
{
    [Header("=== ability settings ===")]
    public bool canAttack;
    public bool canBeHurt;

    [Header("=== figure settings ===")]
    [SerializeField] protected float hp;
    [SerializeField] protected float define;
    [SerializeField] protected float attack;

    [Header("=== controllers ===")]
    protected AttackController attackController = null;//! 有可能不会被赋值的要事先设为null
    protected BeHurtController beHurtController = null;

    /// <summary>
    /// 可以创建可攻击，不可攻击，可被攻击，不可被攻击的生物，默认受伤后不无敌且不硬直
    /// </summary>
    /// <param name="hp">血量上限</param>
    /// <param name="attack">基础攻击</param>
    /// <param name="define">基础防御</param>
    /// <param name="canAttack">能攻击</param>
    /// <param name="canBeHurt">能受伤</param>
    public virtual void init(float hp,float attack,float define,bool canAttack,bool canBeHurt) {
        this.hp = hp;
        this.attack = attack;
        this.define = define;
        this.canAttack = canAttack;
        this.canBeHurt = canBeHurt;
        if (canAttack) {
            attackController = gameObject.AddComponent<AttackController>();
            attackController.init();
        }
        if (canBeHurt) {
            beHurtController = gameObject.AddComponent<BeHurtController>();
            beHurtController.init(0,0);
        }
    }

    /// <summary>
    /// 只能创建可被攻击的生物
    /// </summary>
    /// <param name="hp">血量上限</param>
    /// <param name="attack">基础攻击</param>
    /// <param name="define">基础防御</param>
    /// <param name="canAttack">能攻击</param>
    /// <param name="hurtColdTime">受伤后的无敌时间</param>
    /// <param name="hurtRecoverTime">受伤后的硬直时间</param>
    public virtual void init(float hp, float attack, float define, bool canAttack, float hurtColdTime,float hurtRecoverTime) {
        this.hp = hp;
        this.attack = attack;
        this.define = define;
        this.canAttack = canAttack;
        this.canBeHurt = true;
        if (canAttack) {
            attackController = gameObject.AddComponent<AttackController>();
            attackController.init();
        }
        if (canBeHurt) {
            beHurtController = gameObject.AddComponent<BeHurtController>();
            beHurtController.init(hurtColdTime, hurtRecoverTime);
        }
    }
}
