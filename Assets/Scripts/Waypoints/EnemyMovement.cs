using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : GradientHealth
{
   public enum AIStates
    {
        Patrol,
        Seek,
        Attack,
        Die
    }

    public AIStates state;
    //public Transform target;
    public Transform player;
    public Transform WayPointParent;
    public Transform[] wayPoints;
    public NavMeshAgent agent;
    public float walkSpeed, runSpeed, attackRange, attackSpeed, sightRange, baseDamage;
    public Animator wolfAnim;
    public int curWaypoint, difficulty;
    public bool isDead;
    public float distanceToPoint, changePoint;
    public float stopFromPlayer;

    public override void Start()
    {
        base.Start();

        //get waypoints array from waypoints parent
        wayPoints = WayPointParent.GetComponentsInChildren<Transform>();
        //get navmesh agent from self
        agent = GetComponent<NavMeshAgent>();
        //set speed of agent
        agent.speed = walkSpeed;
        //get animator from self
        wolfAnim = GetComponent<Animator>();
        //set current waypoint
        curWaypoint = 1;
        //set patrol as default
        Patrol();
    }

    public override void Update()
    {
        base.Update();

        wolfAnim.SetBool("Walk", false);
        wolfAnim.SetBool("Run", false);
        wolfAnim.SetBool("Attack", false);

        Patrol();
        Seek();
        Attack();
        Die();
    }

    public void Patrol()
    {
        //do not continue if no waypoints, dead or player in range
        if (wayPoints.Length <= 0 || Vector3.Distance(player.position, transform.position)<= sightRange || isDead)
        {
            return;
        }

        agent.stoppingDistance = 0;
        agent.speed = walkSpeed;

        state = AIStates.Patrol;
        wolfAnim.SetBool("Walk", true);
        //set agent to target
        agent.destination = wayPoints[curWaypoint].position;
        //are we at the waypoint
        distanceToPoint = Vector3.Distance(transform.position, wayPoints[curWaypoint].position);
        if (distanceToPoint <= changePoint)
        {
            //if so go to next waypoint
            if (curWaypoint <wayPoints.Length-1)
            {
                curWaypoint++;
            }
            //if at end of patrol go to start
            else
            {
                curWaypoint = 1;
            }
        }

    }

    public void Seek()
    {
        //if the player is out of our sight range or inside our attack range
        if (Vector3.Distance(player.position, transform.position) > sightRange || Vector3.Distance(player.position, transform.position) < attackRange || isDead)
        {
            //stop seeking
            return;
        }

        agent.stoppingDistance = 0;
        //Set Ai state
        state = AIStates.Seek;
        //set animation
        wolfAnim.SetBool("Run", true);
        //change speed
        agent.speed = runSpeed;
        //target is player
        agent.destination = player.position;
    }

    //this method/function/behaviour can be overridden by any class that inherits from this class
    public virtual void Attack()
    {
        //if player is out of attack range, or we, or they are dead
        if (Vector3.Distance(player.position, transform.position) > attackRange || isDead || PlayerHandler.isDead)
        {
            //dont attack
            return;
        }

        agent.stoppingDistance = 2.5f;

        //Set Ai state
        state = AIStates.Attack;
        //set animation
        wolfAnim.SetBool("Attack", true);
    }

    public void Die()
    {
        //if we are alive
        if (attributes[0].currentValue>0 || isDead)
        {
            //dont run this
            return;
        }
        else
        {
            //Set Ai state
            state = AIStates.Die;
            //set animation
            wolfAnim.SetTrigger("Die");
            //is dead
            isDead = true;
            //stop moving 
            agent.destination = transform.position;
            agent.speed = 0;
            
            //drop loot (if/when inventory system is set up)
        }
    }
}
