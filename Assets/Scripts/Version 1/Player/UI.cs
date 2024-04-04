using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{

    public static UI Instance;
    private TextMeshProUGUI hpDisplay;
    private TextMeshProUGUI ammoDisplay;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        hpDisplay = GameObject.Find("HPDisplay").GetComponent<TextMeshProUGUI>();
        ammoDisplay = GameObject.Find("AmmoDisplay").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public TextMeshProUGUI getHpDisplay()
    {
        return hpDisplay;
    }

    public TextMeshProUGUI getAmmoDisplay()
    {
        return ammoDisplay;
    }
}
