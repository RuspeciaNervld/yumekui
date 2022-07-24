using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace State {
    public class IdleState : IState {
        private FSM manager;
        private Parameter parameter;

        public IdleState(FSM manager) {
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
            if (parameter.target != null) {
                manager.TranslateState(StateType.Chase);
            }
        }
    }

    public class ChaseState : IState {
        private FSM manager;
        private Parameter parameter;

        public ChaseState(FSM manager) {
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
                manager.transform.position = Vector2.MoveTowards(manager.transform.position,
                     new Vector2(parameter.target.position.x, manager.transform.position.y),
                     parameter.moveSpeed * Time.deltaTime);
            }
            if (Physics2D.OverlapCircle(parameter.attackCenter.position, parameter.attackRadius, parameter.targetLayer)) {
                manager.TranslateState(StateType.Attack);
            }
        }
    }
    public class normalAttackState : IState {
        private int attackCount=0;
        private float attackTime;
        private FSM manager;
        private Parameter parameter;
        public GameObject nose;

        public normalAttackState(FSM manager) {
            this.manager = manager;
            this.parameter = manager.parameter;
        }
        public void OnEnter() {
            attackCount++;
            attackTime = 0;
            manager.creature.NormalAttack();
        }

        public void OnExit() {
           
        }

        public void OnUpdate() {
            attackTime += Time.deltaTime;
            if(attackCount % 5 == 0) {
                manager.TranslateState(StateType.Skill);
            }else if (attackTime >= 2) {
                manager.TranslateState(StateType.Chase);
            }
        }
    }
    public class SkillState : IState {
        private float attackTime;
        private bool attacked;
        private FSM manager;
        private Parameter parameter;

        public SkillState(FSM manager) {
            this.manager = manager;
            this.parameter = manager.parameter;
        }
        public void OnEnter() {
            attackTime = 0;
            attacked = false;
            Debug.Log("enter");
        }

        public void OnExit() {
            attacked = false;
        }

        public void OnUpdate() {
            if (!attacked && manager.creature.Skill()) {
                attacked = true;
                attackTime = 0;
            }

            
            if (attacked) {
                manager.transform.position = Vector2.MoveTowards(manager.transform.position,
                                  new Vector2(parameter.target.position.x, manager.transform.position.y),
                                   parameter.chaseSpeed * Time.deltaTime);
                attackTime += Time.deltaTime;
            }
            if (attackTime >= 2) {
                manager.TranslateState(StateType.Chase);
            }
        }
    }

}