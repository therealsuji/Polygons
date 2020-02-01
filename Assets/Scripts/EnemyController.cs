using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void setStopDistance(float stoppingDist)
    {
        agent.stoppingDistance = stoppingDist;
    }

    void moveToPlayer()
    {
        agent.SetDestination(GameManager.instance.player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        moveToPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.player.GetComponent<playerAim>().setHealth(10);
            Destroy(gameObject);
        }else if (other.tag == "projectile")
        {
            Destroy(gameObject);
        }
    }
}