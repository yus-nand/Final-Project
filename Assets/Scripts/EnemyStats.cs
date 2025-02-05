using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : EnemyBase
{
    private NavMeshAgent enemy;
    private GameObject player;
    private void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        enemy.SetDestination(player.transform.position);
        enemy.speed = speed;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            StartCoroutine(Attack());
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        DestroyOnKilled();
    }
    private void DestroyOnKilled()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Attack()
    {
        // DealDamage();
        yield return new WaitForSeconds(1);
    }
}
