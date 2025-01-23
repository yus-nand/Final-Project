using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public EnemyScript hitEnemy;
    public int damage = 10;
    public float gunRange = 100f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * gunRange);
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, gunRange))
        {
            if(hit.transform.tag == "Enemy")
            {
                hitEnemy = hit.transform.GetComponent<EnemyScript>();
                if(hitEnemy is BigGuy)
                {
                    hit.transform.GetComponent<BigGuy>().TakeDamage(damage);
                    Debug.Log("We hit " + hit.transform.name);
                }
                else if(hitEnemy is SmallGuy)
                {
                    hit.transform.GetComponent<SmallGuy>().TakeDamage(damage);
                    Debug.Log("We hit " + hit.transform.name);
                }
            }
        }
    }
}
