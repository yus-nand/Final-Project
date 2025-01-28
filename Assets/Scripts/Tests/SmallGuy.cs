using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallGuy : EnemyScript
{
    public bool isHit = false;
    [SerializeField] private Animator enemyAnimator;
    private NavMeshAgent navMesh;
    private new Renderer renderer;
    private bool isDead = false;
    private void Start()
    {
        damage = 2;
        health = 20;
        speed = 3f;
        xp = 20;
        attackInterval = 0.5f;
        navMesh = GetComponent<NavMeshAgent>();
        renderer = GetComponent<Renderer>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            navMesh.isStopped = true;
            enemyAnimator.SetBool("isNearPlayer", true);
            enemyAnimator.SetBool("seekingPlayer", false);
            StartCoroutine(Attack(collider.gameObject, damage, attackInterval));
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            navMesh.isStopped = false;
            enemyAnimator.SetBool("isNearPlayer", false);
            enemyAnimator.SetBool("seekingPlayer", true);
            StopAllCoroutines();
        }
    }
    private void Update()
    {
        Move();
        if(health <= 0 && !isDead)
        {
            navMesh.speed = Mathf.Lerp(0f, 5f, 1.5f);
            // Player.Instance.score += xp;
            enemyAnimator.SetBool("isDead", true);
            GiveXP(xp);
            isDead = true;
            navMesh.isStopped = true;
            Destroy(gameObject);
        }
    }
    public override void Move()
    {
        navMesh.speed = speed;
        navMesh.SetDestination(GameObject.Find("Player").transform.position);
        if(navMesh.pathPending)
        {
            enemyAnimator.SetBool("seekingPlayer", true);
        }
    }
    public override void TakeDamage(int damage)
    {
        navMesh.isStopped = true;
        enemyAnimator.SetBool("isHit", true);
        health -= damage;
        StartCoroutine(SetBool(enemyAnimator));
        navMesh.isStopped = false;
    }
}
