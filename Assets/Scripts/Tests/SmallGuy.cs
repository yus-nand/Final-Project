using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallGuy : EnemyScript
{
    public bool isHit = false;
    private NavMeshAgent navMesh;
    private void Start()
    {
        damage = 2;
        health = 20;
        xp = 20;
        navMesh = GetComponent<NavMeshAgent>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Attack(collision.gameObject, damage);
        }
    }
    private void Update()
    {
        Move();
        if(health <= 0)
        {
            // Player.Instance.score += xp;
            GiveXP(xp);
            Destroy(gameObject);
        }
    }
    public override void Move()
    {
        navMesh.speed = 10f;
        navMesh.SetDestination(GameObject.Find("Player").transform.position);
    }
    public override void TakeDamage(int damage)
    {
        health -= damage;
    }
}
