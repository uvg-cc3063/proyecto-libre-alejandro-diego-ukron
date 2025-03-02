﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int currentPatrolPoint;

    public NavMeshAgent agent;

    public Animator anim;
    public float wakeUpTimer = 1f;

    private Rigidbody rb;
    

    public enum AIState
    {        
        isIdle,
        isPatrolling,
        isChasing,
        isAttacking,
        isDeath,
        isSleeping,
        isBeingHited
    };

    public enum EnemyType
    {
        CyborgSkeleton,
        RoboticBat
    }

    public AIState currentState;
    public EnemyType eType;

    public float waitAtPoint = 2f;
    private float waitCounter;

    public float chaseRange;

    public float attackRange = 1f;
    public float timeBetweenAttacks = 2f;    
    private float attackCounter;
    private float NormalAgentSpeed;

    public bool isDeath = false;
    public bool Hitted = false;

    public GameObject hurtBox;

    //public GameObject EnemyAnimatedBody;
    // Start is called before the first frame update
    void Start()
    {
        waitCounter = waitAtPoint;
        NormalAgentSpeed = agent.speed;
        rb = GetComponent<Rigidbody>();
        /*if(eType == EnemyType.CyborgSkeleton)
        {
            transform.position = new Vector3(EnemyAnimatedBody.transform.position.x, -1, EnemyAnimatedBody.transform.position.z);
        }     */   
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController_s.instance.transform.position);

        switch (currentState)
        {
            case AIState.isSleeping:
                anim.SetBool("isSleeping", true);
                agent.velocity = Vector3.zero;
                //agent.isStopped = true;
                if(distanceToPlayer <= chaseRange)
                {
                    anim.SetBool("isSleeping", false);
                    StartCoroutine(WakeUpChaseTimer());
                }
                break;
            case AIState.isIdle:
                Hitted = false;
                anim.SetBool("IsMoving", false);
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
                if (waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;
                }
                else
                {
                    currentState = AIState.isPatrolling;
                    agent.SetDestination(patrolPoints[currentPatrolPoint].position);
                    rb.isKinematic = false;
                }

                if (distanceToPlayer <= chaseRange)
                {
                    currentState = AIState.isChasing;
                    anim.SetBool("IsMoving", true);
                    rb.isKinematic = false;
                }

                break;
            case AIState.isPatrolling:
                //agent.SetDestination(patrolPoints[currentPatrolPoint].position);

                if (agent.remainingDistance <= .2f)
                {
                    currentPatrolPoint++;
                    if (currentPatrolPoint >= patrolPoints.Length)
                    {
                        currentPatrolPoint = 0;
                    }
                    //agent.SetDestination(patrolPoints[currentPatrolPoint].position);
                    currentState = AIState.isIdle;
                    waitCounter = waitAtPoint;
                }

                if (distanceToPlayer <= chaseRange)
                {
                    currentState = AIState.isChasing;
                }

                anim.SetBool("IsMoving", true);

                break;
            case AIState.isChasing:
                agent.SetDestination(PlayerController_s.instance.transform.position);
                if (distanceToPlayer <= attackRange)
                {
                    //agent.speed = agent.speed * 500;
                    //Debug.Log("agent.speed: " + agent.speed);
                    currentState = AIState.isAttacking;
                    float ran = Random.Range(0, 2);
                    if(ran == 0)
                    {
                        anim.SetTrigger("Attack1");
                    }
                    else if(ran == 1)
                    {
                        anim.SetTrigger("Attack2");
                    }                    

                    anim.SetBool("IsMoving", false);
                    //agent.speed = NormalAgentSpeed;
                    //agent.velocity = Vector3.zero;
                    //agent.isStopped = true;

                    attackCounter = timeBetweenAttacks;
                }

                if (distanceToPlayer > chaseRange)
                {
                    currentState = AIState.isIdle;
                    waitCounter = waitAtPoint;
                    //agent.velocity = Vector3.zero;
                    agent.SetDestination(transform.position);
                }

                break;
            case AIState.isAttacking:

                transform.LookAt(PlayerController_s.instance.transform, Vector3.up);
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

                attackCounter -= Time.deltaTime;
                if (attackCounter <= 0)
                {
                    if (distanceToPlayer < attackRange)
                    {
                        anim.SetTrigger("Attack2");
                        attackCounter = timeBetweenAttacks;
                    }
                    else
                    {
                        currentState = AIState.isIdle;
                        waitCounter = waitAtPoint;
                        agent.isStopped = false;
                    }
                }
                break;
            case AIState.isBeingHited:
                
                agent.velocity = Vector3.zero;
                //agent.isStopped = true;
                anim.SetTrigger("Hit");     
                currentState = AIState.isIdle;
                //Vector3 knockDirection = new Vector3();

                if (!Hitted)
                {
                    rb.AddForce(PlayerController_s.instance.transform.forward * 200, ForceMode.Impulse);
                    Hitted = true;
                    
                }
                
                //agent.gameObject.transform.s
                break;
            case AIState.isDeath:
                if (isDeath == false)
                {
                    rb.AddForce(PlayerController_s.instance.transform.forward * 5, ForceMode.Impulse);
                    StartCoroutine(StopRbForce());
                    hurtBox.gameObject.SetActive(false);
                    agent.velocity = Vector3.zero;
                    agent.isStopped = true;
                    agent.speed = 0;
                    agent.acceleration = 0;
                    anim.SetBool("isDeath", true);
                    anim.SetTrigger("Death");
                    isDeath = true;
                    for (int i=0;i< GetComponents<Collider>().Length; i++)
                    {
                        GetComponents<Collider>()[i].enabled = false;
                    }
                    
                }
                /*if (eType == EnemyType.CyborgSkeleton)
                {
                    transform.position.Set(EnemyAnimatedBody.transform.position.x, -1f, EnemyAnimatedBody.transform.position.z);
                }*/
                break;
        }
    }

    public IEnumerator WakeUpChaseTimer()
    {
        yield return new WaitForSeconds(wakeUpTimer);
        currentState = AIState.isChasing;
        anim.SetBool("IsMoving", true);
    }

    public IEnumerator StopRbForce()
    {
        yield return new WaitForSeconds(1);        
        rb.isKinematic = true;
    }

}
