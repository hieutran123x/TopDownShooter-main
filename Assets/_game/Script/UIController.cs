using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject healthBar;
    public static UIController instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateHealthBar()
    {
        int currentHP = Player.instance.hp;
        int index = 5 - currentHP;
        healthBar.transform.GetChild(transform.childCount-index).gameObject.SetActive(false);
    }
}
