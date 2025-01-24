using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallGuy : EnemyScript
{
    public bool isHit = false;
    private NavMeshAgent navMesh;
    private Animator enemyAnimator;
    private new Renderer renderer;
    private void Start()
    {
        damage = 2;
        health = 20;
        speed = 5f;
        xp = 20;
        attackInterval = 0.5f;
        navMesh = GetComponent<NavMeshAgent>();
        renderer = GetComponent<Renderer>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            StartCoroutine(Attack(collider.gameObject, damage, attackInterval));
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            StopAllCoroutines();
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
        navMesh.speed = speed;
        navMesh.SetDestination(GameObject.Find("Player").transform.position);
    }
    public override void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(SetBool(enemyAnimator));
    }
}
