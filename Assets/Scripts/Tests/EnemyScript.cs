using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EnemyScript : MonoBehaviour
{
    protected int health;
    protected int damage;
    protected int xp;
    public virtual void Attack(GameObject gameObject, int damage)
    {
        gameObject.GetComponent<Player>().TakeDamage(damage);
    }
    public void GiveXP(int xp)
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        player.score += xp;
    }
    abstract public void Move();
    abstract public void TakeDamage(int damage);
}
