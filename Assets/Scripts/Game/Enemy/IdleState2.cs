using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace State2 {
    public class IdleState : IState {
        private FSM2 manager;
        private Parameter parameter;

        public IdleState(FSM2 manager) {
            this.manager = manager;
            this.parameter = manager.parameter;
        }
        public void OnEnter() {
            //manager.transform.DOShakeScale(1f).SetLoops(-1);
        }

        public void OnExit() {
            //manager.transform.DOComplete();
        }

        public void OnUpdate() {
            Collider2D c2d = Physics2D.OverlapCircle(parameter.seeCenter.position, parameter.seeRadius, parameter.targetLayer);
            if (c2d != null) {
                parameter.target = c2d.transform;
            }
            if (parameter.target != null) {
                manager.TranslateState(StateType.Chase);
            }
        }
    }

    public class ChaseState : IState {
        private FSM2 manager;
        private Parameter parameter;

        public ChaseState(FSM2 manager) {
            this.manager = manager;
            this.parameter = manager.parameter;
        }

        public void OnEnter() {
           
        }

        public void OnExit() {
            
        }

        public void OnUpdate() {
            manager.FlipTo(parameter.target);
            if (parameter.target) {
                if(manager.transform.position.x - parameter.target.position.x > 2|| manager.transform.position.x - parameter.target.position.x < -2) {
                    manager.transform.position = Vector2.MoveTowards(manager.transform.position,
                     new Vector2(parameter.target.position.x, manager.transform.position.y),
                     parameter.moveSpeed * Time.deltaTime);
                }
            }
            if (Physics2D.OverlapCircle(parameter.attackCenter.position, parameter.attackRadius, parameter.targetLayer)) {
                manager.TranslateState(StateType.Attack);
            }
        }
    }
    public class normalAttackState : IState {
        private int attackCount=0;
        private float attackTime;
        private FSM2 manager;
        private Parameter parameter;
        public GameObject nose;
        private bool againsted;


        public normalAttackState(FSM2 manager) {
            this.manager = manager;
            this.parameter = manager.parameter;
        }
        public void OnEnter() {
            attackTime = 0;
            if (manager.creature.canAttack && manager.creature.NormalAttack()) {
                //manager.creature.weapon.sr.DOComplete();
                manager.creature.weapon.sr.DOColor(new Color(0.66f, 0.83f, 0.24f), 1f).OnComplete(() => {
                    manager.creature.weapon.sr.DOColor(new Color(0.57647f, 0.57647f, 0.57647f), 0f);
                });

                againsted = false;
                attackCount++;
            } else {
                manager.TranslateState(StateType.Chase);
            }
        }

        public void OnExit() {
           
        }

        public void OnUpdate() {
            attackTime += Time.deltaTime;
            if(attackTime < 0.7f) {
                return;
            }
            //! 普攻弹反，有点离谱
            if (!againsted &&  manager.creature.weapon.touchWeapon) {
                //todo 被弹反
                Debug.Log("弹反");
                SpecialEManager.Instance.DoBulletTime(0.1f, 0.3f);
                AudioManager.Instance.playSoundEffect("knifeCollision.wav");
                manager.creature.beHurtController.beHurt(manager.creature.weapon.touchedWeapon.computedAttack * 2);


                manager.creature.weapon.transform.localPosition = new Vector3(-1.25f,-0.01f,0);
                //manager.creature.weapon.transform.DOShakePosition(0.3f, 0.6f).OnComplete(() => {

                //});
                againsted = true;
                manager.creature.attackController.EndAttack();
                //! 先把结束动画放出来再瞬间完成动画
                manager.creature.weapon.transform.DOComplete();


            }



            if (attackCount == RusRandomer.randNum(4,6)) {
                attackCount = 0;
                manager.TranslateState(StateType.Skill);
            }else if (attackTime >= 2) {
                //! 弹反不弹反都是2s结束
                manager.TranslateState(StateType.Chase);
            }
        }
    }
    public class SkillState : IState {
        private float attackTime;
        private bool attacked;
        private FSM2 manager;
        private Parameter parameter;
        //private bool right;
        private int wallCount;
        private bool counted;
        public SkillState(FSM2 manager) {
            this.manager = manager;
            this.parameter = manager.parameter;
        }
        public void OnEnter() {
            manager.creature.weapon.skillEndTime = 99;
            //manager.FlipTo(parameter.target);
            attackTime = 0;
            attacked = false;
            wallCount = 0;
        }

        public void OnExit() {
        }

        public void OnUpdate() {
            if (!attacked && manager.creature.Skill()) {
                attacked = true;
                attackTime = 0;
                //if (manager.transform.position.x < parameter.target.transform.position.x) {
                //    right = false;
                //} else {
                //    right = true;
                //}
                manager.FlipTo(parameter.target);
            }
            if (attacked) {
                attackTime += Time.deltaTime;
                //manager.transform.position = Vector2.MoveTowards(manager.transform.position,
                //                  new Vector2(parameter.target.position.x, manager.transform.position.y),
                //                   parameter.chaseSpeed * Time.deltaTime);

                //manager.transform.position = new Vector3(manager.transform.position.x + (right ? 1 : -1) * parameter.chaseSpeed * Time.deltaTime, manager.transform.position.y,manager.transform.position.z);
                if (attackTime < 1f) {
                    return;
                }
                if (5.9 - manager.transform.position.x < 0.1f) {
                    if (!counted) {
                        wallCount++;
                        counted = true;
                    }
                    //right = false;
                    manager.transform.localScale = new Vector3(1, 1, 1);
                }else if(manager.transform.position.x + 5.9 < 0.1f) {
                    if (!counted) {
                        wallCount++;
                        counted = true;
                    }
                    //right = true;
                    manager.transform.localScale = new Vector3(-1, 1, 1);
                } else {
                    counted = false;
                }
                if (wallCount == 5 || manager.creature.weapon.touchPlayer) {
                    manager.creature.attackController.EndAttack();
                    manager.TranslateState(StateType.Chase);
                }
                parameter.r2d.velocity = new Vector2(-parameter.chaseSpeed * manager.transform.localScale.x, parameter.r2d.velocity.y);
                
                if (manager.creature.weapon.touchWeapon) {
                    //todo 被弹反
                    Debug.Log("技能弹反");
                    manager.creature.beHurtController.beHurt(manager.creature.weapon.touchedWeapon.computedAttack*2);
                    AudioManager.Instance.playSoundEffect("knifeCollision.wav");
                    SpecialEManager.Instance.DoBulletTime(0.12f, 0.25f);
                    manager.creature.attackController.EndAttack();
                    parameter.r2d.velocity = new Vector2(parameter.chaseSpeed * manager.transform.localScale.x, 5);
                    manager.TranslateState(StateType.Chase);
                }
            }
            //if (attackTime >= 3) {
            //    manager.TranslateState(StateType.Chase);
            //}
        }
    }

}