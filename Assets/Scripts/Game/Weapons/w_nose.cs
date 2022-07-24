using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class w_nose : IWeapon {
    public override void NormalAttackAnim() {
        transform.DOMoveX(-7.24f,1f);
        throw new System.NotImplementedException();
    }

    public override void NormalAttackEnd() {
        transform.DOMoveX(-4.06f,1f);
        throw new System.NotImplementedException();
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
        throw new System.NotImplementedException();
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
