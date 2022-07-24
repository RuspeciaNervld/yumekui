using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Collider2D))]
public abstract class IWeapon : MonoBehaviour {
    [Header("=== property settings ===")]
    public float normalAttackHurtTime;
    public float normalAttackEndTime;
    public float skillHurtTime;
    public float skillEndTime;
    public float normalAttackPlus;
    public float normalAttackMult;

    public float computedAttack;

    protected ICreature user;
    //protected Animator anim;
    protected Collider2D c2d;

    public void Awake() {
        user = GetComponentInParent<ICreature>();
        //anim = GetComponent<Animator>();
        c2d = GetComponent<Collider2D>();
        c2d.isTrigger = true;
        this.tag = "Weapon";
        for (int i = 0; i < transform.childCount; i++) {
            Transform child = transform.GetChild(i);
            child.tag = "Weapon";
        }
    }

    public abstract void NormalAttackAnim();
    public abstract void NormalAttackEnd();
    public abstract void NormalAttackHurt();
    public abstract void SkillAttackAnim();
    public abstract void SkillAttackEnd();
    public abstract void SkillAttackHurt();
    public abstract void SwordAgainstAnim();
    public abstract void SwordAgainstEnd();
    public abstract void SwordAgainstHurt();
}
