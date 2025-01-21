using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float damage = 10f;
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
                Debug.Log("We hit " + hit.transform.name);
                hit.transform.GetComponent<EnemyStats>().TakeDamage(damage);
            }
        }
    }
}
