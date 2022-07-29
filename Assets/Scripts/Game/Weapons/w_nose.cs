using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class w_nose : IWeapon {
    public override void NormalAttackAnim() {
        transform.DOComplete();
        transform.DOLocalMoveX(-4f, 0.3f).SetDelay(1f).OnComplete(() => {
            
        });
    }

    public override void NormalAttackEnd() {
        c2d.enabled = false;
        transform.DOLocalMoveX(-1.25f, 0.3f);
        this.computedAttack = 0;
        //transform.DOComplete();
    }

    public override void NormalAttackHurt() {
        c2d.enabled = true;
        float computedAttack = user.attack + normalAttackPlus;
        this.computedAttack = computedAttack;
    }

    public override void SkillAttackAnim() {
        transform.DOComplete();
        //transform.localPosition = new Vector3(-1.25f, -0.01f, 0);
        //transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        transform.DOLocalMove(new Vector3(-3.27f, 1.16f, 0), 0.5f, false).OnComplete(() => {
            transform.DOLocalRotate(new Vector3(0, 0, -90), 0.5f).OnComplete(() => {
                
            });
        });
    }

    public override void SkillAttackEnd() {
        c2d.enabled = false;
        this.computedAttack = 0;
        transform.DOComplete();
        //transform.localPosition = new Vector3(-1.25f, -0.01f, 0);
        //transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        transform.DOLocalMove(new Vector3(-1.25f, -0.01f, 0), 0.5f, false).SetDelay(2f).OnComplete(() => {
            transform.DOLocalRotate(new Vector3(0, 0, 0), 0.5f).OnComplete(() => {
                user.canAttack = true;
            });
        });
    }

    public override void SkillAttackHurt() {
        c2d.enabled = true;
        float computedAttack = user.attack * skillMult;
        this.computedAttack = computedAttack;
        user.canAttack = false;
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
