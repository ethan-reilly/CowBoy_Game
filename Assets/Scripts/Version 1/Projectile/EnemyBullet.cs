using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private int bulletStrength = 20;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().TakeDamage(bulletStrength);
            Destroy(gameObject);
            //Debug.Log("Bullet hit player");
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("Bullet hit ground");
            Destroy(gameObject);
        }
        
        if(other.CompareTag("Enemy"))
        {
            Physics.IgnoreCollision(other.GetComponent<Collider>(), GetComponent<Collider>());
            //Debug.Log("Bullet hit enemy");
        }

        if (other.gameObject.CompareTag("Untagged"))
        {
            Destroy(gameObject);
        }

    }
}
