using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigGuy : EnemyScript
{
    public bool isHit = false;
    [SerializeField] private Animator enemyAnimator;
    private NavMeshAgent navMesh;
    // private new Renderer renderer;
    private bool isDead = false;
    private void Start()
    {
        Move();
        health = 50;
        damage = 10;
        speed = 1f;
        xp = 100;
        attackInterval = 1.5f;
        navMesh = GetComponent<NavMeshAgent>();
        // renderer = GetComponent<Renderer>();
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
            navMesh.speed = Mathf.Lerp(0f, 2f, 1);
            // Player.Instance.score += xp;
            // enemyAnimator.SetBool("seekingPlayer", false);
            // enemyAnimator.SetBool("isNearPlayer", true);
            // enemyAnimator.SetBool("isHit", false);
            enemyAnimator.SetBool("isDead", true);
            GiveXP(xp);
            isDead = true;
            navMesh.isStopped = true;
            Destroy(gameObject, 1.25f);
        }
    }
    public override void Move()
    {
        navMesh.speed = speed;
        navMesh.SetDestination(GameObject.Find("Player").transform.position);
        if(navMesh.pathPending)
        {
            enemyAnimator.SetBool("seekingPlayer", true);
            audioSource.Play();
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
