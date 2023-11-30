using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    private int bulletStrength = 0;

    private void Awake()
    {
        if (GameManager.Instance.WeaponType == 0)
        {
            bulletStrength = 30;
        }
        else if (GameManager.Instance.WeaponType == 1)
        {
            bulletStrength = 50;
        }
    }

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
