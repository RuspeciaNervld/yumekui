using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ICreature : MonoBehaviour
{
    [Header("=== ability settings ===")]
    public bool canAttack;
    public bool canBeHurt;

    [Header("=== figure settings ===")]
    public float hp;
    public float accept;
    public float attack;

    [Header("=== controllers ===")]
    public AttackController attackController = null;//! 有可能不会被赋值的要事先设为null
    public BeHurtController beHurtController = null;
    public IWeapon weapon = null;

    [Header("=== can be hurt ===")]
    public float hurtColdTime;
    public float hurtRecoverTime;

    [Header("=== objects ===")]
    public SpriteRenderer sr;

    public virtual void beHurtAction() {
        //! 变红动画
        sr.DOComplete();
        transform.DOComplete();
        SpecialEManager.Instance.DoShake(0.05f, 0.15f);
        SpecialEManager.Instance.DoBulletTime(0.05f, 0.25f);
        sr.DOColor(new Color(1, 0, 0), 0.3f).OnComplete(() => {
            transform.DOShakePosition(0.2f, 0.2f, 1);
            sr.DOColor(new Color(1, 1, 1), 0f);
        });
    }
    public virtual bool NormalAttack() {
        if (this.attackController.doAttack(weapon.normalAttackHurtTime, weapon.normalAttackEndTime, weapon.NormalAttackHurt, weapon.NormalAttackEnd)) {
            //todo 播放攻击动画
            weapon.NormalAttackAnim();
            return true;
        } else {
            return false;
        }
    }


    public virtual bool Skill() {
        if (this.attackController.doAttack(weapon.skillHurtTime, weapon.skillEndTime, weapon.SkillAttackHurt, weapon.SkillAttackEnd)) {
            //todo 播放攻击动画
            weapon.SkillAttackAnim();
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// 可以创建可攻击，不可攻击，可被攻击，不可被攻击的生物，默认受伤后不无敌且不硬直
    /// </summary>
    /// <param name="hp">血量上限</param>
    /// <param name="attack">基础攻击</param>
    /// <param name="define">基础防御</param>
    /// <param name="canAttack">能攻击</param>
    /// <param name="canBeHurt">能受伤</param>
    public virtual void init(/*float hp,float attack,float define,*/bool canAttack,bool canBeHurt) {
        //this.hp = hp;
        //this.attack = attack;
        //this.define = define;
        this.canAttack = canAttack;
        this.canBeHurt = canBeHurt;
        if (canAttack) {
            attackController = gameObject.AddComponent<AttackController>();
            attackController.init();
            weapon = GetComponentInChildren<IWeapon>();
        }
        if (canBeHurt) {
            beHurtController = gameObject.AddComponent<BeHurtController>();
            beHurtController.init(0, 0, this);
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
    public virtual void init(/*float hp, float attack, float define,*/ bool canAttack, float hurtColdTime,float hurtRecoverTime) {
        //this.hp = hp;
        //this.attack = attack;
        //this.define = define;
        this.canAttack = canAttack;
        this.canBeHurt = true;
        if (canAttack) {
            attackController = gameObject.AddComponent<AttackController>();
            attackController.init();
            weapon = GetComponentInChildren<IWeapon>();
        }

        beHurtController = gameObject.AddComponent<BeHurtController>();
        beHurtController.init(hurtColdTime, hurtRecoverTime,this);
        
    }

    private void Start() {
        init(canAttack, hurtColdTime,hurtRecoverTime);
        sr = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    //! 这是一种有实体的受伤方式，另一种由对方直接调用函数
    private void OnTriggerEnter2D(Collider2D collision) {
        if (canBeHurt && collision.CompareTag("Weapon")) {
            if (collision.gameObject.GetComponent<IWeapon>() != null) {
                beHurtController.beHurt(collision.gameObject.GetComponent<IWeapon>().computedAttack);
            }else{
                beHurtController.beHurt(collision.gameObject.GetComponent<IBullet>().attack);
            }
        }
    }
}
