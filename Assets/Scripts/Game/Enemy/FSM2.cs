using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace State2 {
    public enum StateType {
        Idle, Patrol, Chase, React, Attack, Skill, Die
    }

    [System.Serializable]
    public class Parameter {
        public int health;
        public float moveSpeed;
        public float chaseSpeed;
        public float idleTime;
        public Rigidbody2D r2d;
        public Transform[] patrolPoints;
        public Transform[] chasePoints;
        public Animator animator;
        public Transform target;
        public LayerMask targetLayer;
        public Transform attackCenter;
        public float attackRadius;
        public Transform seeCenter;
        public float seeRadius;
    }

    public class FSM2 : MonoBehaviour {
        public Parameter parameter;
        public GameObject nose;
        public ICreature creature;
        private IState currentState;

        private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();


        // Start is called before the first frame update
        void Start() {
            nose = GetComponentInChildren<IWeapon>().gameObject;

            states.Add(StateType.Idle, new IdleState(this));
            states.Add(StateType.Attack, new normalAttackState(this));
            states.Add(StateType.Chase, new ChaseState(this));
            states.Add(StateType.Skill, new SkillState(this));

            TranslateState(StateType.Idle);
        }

        // Update is called once per frame
        void Update() {
            
            Debugger.Instance.logs[1].text = "BossÑªÁ¿£º" + creature.hp;
            if(creature.hp <= 0) {
                OnDie();
                return;
            }
            currentState.OnUpdate();
        }

        public void TranslateState(StateType type) {
            if (currentState != null) {
                currentState.OnExit();
            }
            currentState = states[type];
            currentState.OnEnter();
        }

        public void FlipTo(Transform target) {
            if (transform.position.x > target.position.x) {
                transform.localScale = new Vector3(1, 1, 1);
            } else {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(parameter.attackCenter.position, parameter.attackRadius);
            Gizmos.DrawWireSphere(parameter.seeCenter.position, parameter.seeRadius);
        }


        private void OnDie() {
            Destroy(gameObject);
        }
    }

}
