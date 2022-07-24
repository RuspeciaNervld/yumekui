using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class w_nose : IWeapon {
    public override void NormalAttackAnim() {
        transform.DOLocalMoveX(-1.6f, 0.5f).OnComplete(() => {
            transform.DOLocalMoveX(1.0295f, 0.5f);
        });
    }

    public override void NormalAttackEnd() {
        transform.DOComplete();
    }

    public override void NormalAttackHurt() {
        float computedAttack = user.attack + normalAttackPlus;

        this.computedAttack = computedAttack;
    }

    public override void SkillAttackAnim() {
        Debug.Log("dotween");
        transform.DOLocalMove(new Vector3(-1.08f, 1.18f, 0), 0.5f, false).OnComplete(() => {
            transform.DOLocalRotate(new Vector3(0, 0, -90), 0.5f).OnComplete(() => {
                transform.DOLocalMove(new Vector3(1.0295f, -0.01f, 0), 0.5f, false).SetDelay(1f).OnComplete(() => {
                    transform.DOLocalRotate(new Vector3(0, 0, 0), 0.5f);
                });
            });
        });
    }

    public override void SkillAttackEnd() {
        transform.DOComplete();
    }

    public override void SkillAttackHurt() {
        float computedAttack = user.attack * normalAttackMult;
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
