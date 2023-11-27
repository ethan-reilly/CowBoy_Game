using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int weaponType;
    private int health = 100;
    public TextMeshProUGUI hpDisplay;

    private void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }

        if (hpDisplay != null)
        {
            hpDisplay.SetText("HEALTH: " + health);
        }

    }

    public void TakeDamage(int x)
    {
        health -= x;
    }
 

}
