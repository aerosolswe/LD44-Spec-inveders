using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pawn : MonoBehaviour {

    public PawnInfo pawnInfo;
    public PawnHealthBar healthBar;
    public Animator animator;

    [HideInInspector]
    public NavMeshAgent agent;

    [HideInInspector]
    public Transform moveTarget;

    private int health;

    private bool playedDeathAnimation = false;

    public void Init() {
        Health = BaseHealth;
        healthBar.pawn = this;
    }

    private void Update() {
        float speed = agent.velocity.magnitude;
        float normalizedSpeed = speed / agent.speed;
        normalizedSpeed = Mathf.Clamp(normalizedSpeed, 0, 1);
        animator.SetFloat("SpeedModifier", normalizedSpeed);
    }

    public void StartMoving() {
        MoveToPoint(moveTarget.position);
    }

    public void MoveToPoint(Vector3 point) {
        agent.destination = point;
    }

    public void Die() {
        if (playedDeathAnimation) {
            return;
        }
        playedDeathAnimation = true;

        PawnManager.instance.alivePawns.Remove(this);
        Destroy(this.gameObject);
        GameManager.instance.CheckIfLost();
    }

    public int BaseHealth {
        get {
            return pawnInfo.Health;
        }
    }

    public int Health {
        get {
            return health;
        }
        set {
            health = value;

            if (health <= 0) {
                health = 0;

                Die();
            }
        }
    }

    public bool Dead {
        get {
            return Health <= 0;
        }
    }
}
