using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    int health = 150;
    Shooting getShootingInfo;
    UIManager uiManager;
    PlayerMovement player;
    EnemiesKilled kills;
    //bool isPlayer;
    void Start()
    {
        getShootingInfo = GetComponent<Shooting>();
        if (getShootingInfo.isPlayer)
        {
            uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
            //player = GameObject.FindObjectOfType<PlayerMovement>();
        }
        kills = GameObject.Find("Agent").GetComponent<EnemiesKilled>();
    }

    void Update()
    {
        
    }

    public void DealDamage(int damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            

            if (getShootingInfo.isPlayer)
            {
                health = 0;
                Debug.Log("Player Died");
            }
            

            else
            {
                kills.kills++;
                Destroy(gameObject);
            }
            
        }
        if (getShootingInfo.isPlayer)
        {
            uiManager.UpdateHealth(health);
        }
        
    }
}
