using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private int bulletStrength = 50;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
            {
            other.gameObject.GetComponent<Enemy>().TakeDamage(bulletStrength);
            Destroy(gameObject);
        }

        if(other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Bullet hit ground");
            Destroy(gameObject);
        }
    
    }
}
