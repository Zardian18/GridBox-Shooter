using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    float speed=15f;
    [SerializeField]
    float timeToLive= 5f;
    [SerializeField]
    int damage = 10;
    int enemyID;


   /* private void Awake()
    {
       
    }*/
    void Start()
    {
       // enemyID = transform.GetComponentInParent<Shooting>().enemyID;
        Vector3 parentPos = transform.parent.position+ new Vector3(0f,3.8f,0f);
        transform.parent = null;
        transform.position = parentPos;
        //DamageCalculate();

    }

    void Update()
    {
        Movement();
        
       
    }

    
    void Movement()
    {
        transform.Translate(new Vector3(0, 0, speed)*Time.deltaTime);
        Destroy(gameObject, timeToLive);
        
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Hit Player: "+ damage);
            other.GetComponent<Health>().DealDamage(damage);
        }
        //Debug.Log("Enemy Bullet hit something");
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
    
/*
    void DamageCalculate()
    {
        switch (id)
        {
            case 0:
                damage = 40;
                break;
            case 1:
                damage = 25;
                break;
            case 2:
                damage = 50;
                break;
            case 3:
                damage = 120;
                break;
        }


        
    }
*/
    void CheckLayerMask()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),out hit, Mathf.Infinity, LayerMask.GetMask("Platform")))
        {
            Destroy(gameObject);
        }
    }
    
}
