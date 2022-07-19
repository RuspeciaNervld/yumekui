using UnityEngine;

public class Player : ICreature {
    private Animator anim;
    private PlayerController moveController;

    private float skillAttack;
    private float skillHurtTime;
    private float skillEndTime;
    private float skillAttackRadius;

    private void Awake() {
        anim = GetComponent<Animator>();
        moveController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private void Update() {
        if(canBeHurt && beHurtController.isRecovering()) {
            anim.enabled = false;
            moveController.enabled = false;
        } else {
            anim.enabled = true;
            moveController.enabled = true;
        }
    }

    private void onSkillHurt() {
        //todo 找到受到伤害的生物，计算出自己的攻击，传给对方的受伤函数，最终由对方计算实际伤害
    }

    private void onSkillEnd() {
        //todo 结束动画,恢复状态，也可以调用敌人的受伤结束函数，产生击退或者处刑效果等
    }

    private void onPlayerHurt() {
        Debug.Log("受到伤害");

    }

    public void onPlayerSkill() {
        Debug.Log("技能发动");
        skillAttack = 2;
        skillHurtTime = 1.0f;
        skillEndTime = 1.4f;
        skillAttackRadius = 1.0f;

        if (this.attackController.doAttack(skillHurtTime, skillEndTime, onSkillHurt, onSkillEnd)) {
            //todo 播放攻击动画
        }
    }
}
