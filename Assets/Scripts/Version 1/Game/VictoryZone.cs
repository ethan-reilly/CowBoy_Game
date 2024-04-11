using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered victory zone");
        }

        if (collision.gameObject.CompareTag("Player") && GameManager.Instance.GetEnemiesDefeated())
        {
            GameManager.Instance.NextLevel();
        }
    }
}
