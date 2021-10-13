using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
   public enum AIStates
    {
        Patrol,
        Seek,
        Attack,
        Die
    }

    public AIStates state;
    public Transform target;
    public Transform player;
    public Transform WayPointParent;
    public Transform[] wayPoints;
    public NavMeshAgent agent;
    public float moveSpeed, attackRange, attackSpeed, sightRange, baseDamage;
    public Animator wolfAnim;
    public int curWaypoint;
    public bool isDead;
    public float distanceToPoint, changePoint;

    private void Start()
    {
        //get waypoints array from waypoints parent
        wayPoints = WayPointParent.GetComponentsInChildren<Transform>();
        //get navmesh agent from self
        agent = GetComponent<NavMeshAgent>();
        //set speed of agent
        agent.speed = moveSpeed;
        //get animator from self
        wolfAnim = GetComponentInChildren<Animator>();
        //set current waypoint
        curWaypoint = 1;
        //set patrol as default
        Patrol();
    }

    //use empty game objects with a marker for waypoints

    void Patrol()
    {
        //do not continue if no waypoints, dead or player in range
        if (wayPoints.Length <= 0 || Vector3.Distance(player.position, transform.position)<= sightRange || isDead)
        {
            return;
        }

        state = AIStates.Patrol;
        wolfAnim.SetBool("Walk", true);
        //set agent to target
        agent.destination = wayPoints[curWaypoint].position;
        //are we at the waypoint
        distanceToPoint = Vector3.Distance(transform.position, wayPoints[curWaypoint].position);
        if (distanceToPoint < changePoint)
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

    void Seek()
    {
        //if the player is out of our sight range or inside our attack range
        if (Vector3.Distance(player.position, transform.position) >= sightRange || Vector3.Distance(player.position, transform.position) <= attackRange)
        {
            //stop seeking
            return;
        }

        //Set Ai state
        state = AIStates.Seek;
        wolfAnim.SetBool("", true);
        //set animation
        //change speed 
        //target is player
    }

    void Attack()
    {
        //if player is in attack range then attack
        //Set Ai state
        //set animation
    }

    void Die()
    {
        //if we are alive
        //dont run this
        //else run this 
        //Set Ai state
        //set animation
        //is dead
        //stop moving 
    }
}
