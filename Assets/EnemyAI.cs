﻿using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
    public float viewRange; //distance at which the object will turn and look toward the target
    public float attackRange; //distance at which the object will move toward the target
    public float speed; //speed at which the object will move toward the target
    public Transform target; //the target of the object - set in inspector
    public AudioClip warningSound; //sound the enemy plays when in view range of target
    public AudioClip attackSound; //sound the enemy plays when in attack range of target
    private float targetDistance;
    private Rigidbody rigidbody;
    private bool hasWarningGrowled = false;
    private bool hasAttackScreamed = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        targetDistance = Vector3.Distance(transform.position, target.transform.position);
        if (targetDistance < attackRange)
        {
            lookAtTarget();
            attackPlayer();
            //play attack sound
            //AudioSource.Stop();
            if (!hasAttackScreamed)
            {
                AudioSource.PlayClipAtPoint(attackSound, transform.position);
                hasAttackScreamed = true;
            }
            //hasWarningGrowled = false;
        }
        else if (targetDistance < viewRange) //if can see target (player)
        {
            lookAtTarget();
            //play warning clip
            if (!hasWarningGrowled)
            {
                AudioSource.PlayClipAtPoint(warningSound, transform.position);
                hasWarningGrowled = true;
            }
            //hasAttackScreamed = false;
        } else if (targetDistance > viewRange && hasWarningGrowled) {
            hasWarningGrowled = false;
            hasAttackScreamed = false;
        }
    }

    void attackPlayer()
    {
        transform.position += transform.forward * Time.deltaTime * speed; //move towards (already facing) target
    }

    void OnCollisionEnter(Collision other)
    {
        //action on reaching target
    }

    void lookAtTarget()
    {
//            if (transform.parent != null) //if has a parent
//                transform.localRotation = Quaternion.LookRotation(target.transform.position - transform.position); //lookAt was looking away from
//            else
                transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
    }
}
