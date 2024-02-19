using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bat : MonoBehaviour
{
    //we need to control basic movement
    //and when the animation plays

    public NavMeshAgent agentMrBean;
    public Animator animator;

    public float moveSpeed;

    public bool isMoving;

    //Targetting
    public Transform currentTarget;
    public float targetRange;

    public moveStates currentMoveState;
    public enum moveStates 
    {
        CHASE,
        FLEE,
        ATTACK,
        DIE    
    }


    //Target function
    public void Targetting()
    {
        Debug.DrawRay(this.transform.position, transform.forward * targetRange, Color.red);
        //Debug.DrawRay(this.transform.position, -transform.right * 5 + transform.forward * targetRange, Color.red);
        //Debug.DrawRay(this.transform.position, transform.right * 5 + transform.forward * targetRange, Color.red);
        //Debug.DrawRay(this.transform.position, -transform.right * 10 + transform.forward * targetRange, Color.red);
        Debug.DrawRay(this.transform.position, transform.right * 10 + transform.forward * targetRange, Color.red);

        //we just want to start with a simple raycast
        RaycastHit hitInfo;
        /*
        if (Physics.Raycast(this.transform.position, transform.forward, out hitInfo, targetRange) ||
            Physics.Raycast(this.transform.position, -transform.right * 5 + transform.forward * targetRange, out hitInfo, targetRange) ||
            Physics.Raycast(this.transform.position, transform.right * 5 + transform.forward * targetRange, out hitInfo, targetRange) ||
            Physics.Raycast(this.transform.position, -transform.right * 10 + transform.forward * targetRange, out hitInfo, targetRange) ||
            Physics.Raycast(this.transform.position, transform.right * 10 + transform.forward * targetRange, out hitInfo, targetRange))
        */

        if (Physics.Raycast(this.transform.position, transform.forward, out hitInfo, targetRange) ||
            Physics.Raycast(this.transform.position, transform.right * 10 + transform.forward * targetRange, out hitInfo, targetRange))
        {
            //we need to check if the raycast hit something and if it was what we want to find
            //in this case the bat needs to find the player

            //we can use many ways to find the player but this is likely the simplest.
            //first we have to make a tag in Unity, I will show you now
            if (hitInfo.transform.tag == "Player")
            {
                //now we simply set the object we hit with the raycast that we now know is the player
                //to the current target
                currentTarget = hitInfo.transform;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agentMrBean = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //it needs to be run from a game loop, or it wont ever be called at all
        Targetting();

        //FSM LOGIC TO SWITCH STATES
        if (currentTarget != null)
        {
            currentMoveState = moveStates.CHASE;
        }

        FSM();

    }

    void FSM()
    {
        switch (currentMoveState)
        {
            case moveStates.CHASE:
                //logic goes here 
                agentMrBean.SetDestination(currentTarget.position);
                break;
            case moveStates.FLEE:
                break;
            case moveStates.ATTACK:
                break;
            case moveStates.DIE:
                break;
        }
    }
}
