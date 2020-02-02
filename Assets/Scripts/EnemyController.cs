using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject shard;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.tag == "Player")
    //     {
    //         // Destroy(gameObject);
    //
    //     }
    //     else if (other.tag == "projectile")
    //     {
    //
    //         // Destroy(gameObject);
    //         // Instantiate(shard);
    //         // Instantiate(shard);
    //     }
    // }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Player")
        {
            Destroy(gameObject);

        }
        else
        if (other.transform.tag == "projectile")
        {
            Vector3 dir = other.contacts[0].point - transform.position;
            dir = -dir.normalized;
            GetComponent<Rigidbody>().AddForce(dir*15);
            StartCoroutine(deathCoroutine());
            
        }
    }
    IEnumerator deathCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.active = false;
        GameObject shards = Instantiate(shard,transform.position,Quaternion.identity);
        shards.GetComponent<Rigidbody>().AddRelativeForce(Random.onUnitSphere * 1);
        Destroy(gameObject,500);
    }
}