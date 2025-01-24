using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EnemyScript : MonoBehaviour
{
    protected int health;
    protected int damage;
    protected int xp;
    protected float speed;
    protected float attackInterval;
    // public virtual void Attack(GameObject gameObject, int damage)
    // {
    //     
    // }
    public void GiveXP(int xp)
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        player.score += xp;
    }
    abstract public void Move();
    abstract public void TakeDamage(int damage);
    public IEnumerator Attack(GameObject gameObject, int damage,float attackInterval)
    {
        gameObject.GetComponent<Player>().TakeDamage(damage);
        yield return new WaitForSeconds(attackInterval);
        StartCoroutine(Attack(gameObject, damage, attackInterval));
    }
    public IEnumerator SetBool(Animator animator)
    {
        yield return new WaitForSeconds(0.6f);
        animator.SetBool("isHit", false);
    }
}
