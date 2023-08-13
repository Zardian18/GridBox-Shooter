using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesKilled : MonoBehaviour
{
    public int kills = 0;
    UIManager uIManager;
    void Start()
    {
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update()
    {
        
    }
}
