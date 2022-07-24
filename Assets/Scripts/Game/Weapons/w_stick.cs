using DG.Tweening;
using UnityEngine;

public class w_stick : IWeapon {
    public TrailRenderer tr;

    public override void NormalAttackAnim() {
        tr.enabled = true;
        c2d.enabled = true;
        transform.DOScale(new Vector3(1.5002F, 0.09955693F, 1), 0f);
        Debug.Log("播放攻击动画");
        transform.DORotate(new Vector3(0, 0, user.transform.localScale.x * 75), 0.2f, RotateMode.Fast).SetEase(Ease.InExpo);

    }

    public override void NormalAttackEnd() {

        transform.DORotate(new Vector3(0, 0, 0), 0.1f, RotateMode.Fast).SetEase(Ease.InExpo).OnComplete(() => {
            transform.DOScale(new Vector3(0, 0, 0), 0f);
            tr.enabled = false;
            c2d.enabled = false;
        });
    }

    public override void NormalAttackHurt() {
        float computedAttack = user.attack + normalAttackPlus;

        this.computedAttack = computedAttack;
    }

    public override void SkillAttackAnim() {
        throw new System.NotImplementedException();
    }

    public override void SkillAttackEnd() {
        throw new System.NotImplementedException();
    }

    public override void SkillAttackHurt() {
        //! 用了这个强转就只能作为主角武器了
        float computedAttack = user.attack*((Player)user).nearMult * normalAttackMult;

        //todo 一种情况：抓到一堆人全部欧拉一遍(调用beHurt)
        

        //todo 另一种：让别人自己通过碰撞器检测是否收到攻击，只呈递数值
        this.computedAttack = computedAttack;
    }

    public override void SwordAgainstAnim() {
        throw new System.NotImplementedException();
    }

    public override void SwordAgainstEnd() {
        throw new System.NotImplementedException();
    }

    public override void SwordAgainstHurt() {
        throw new System.NotImplementedException();
    }
}
