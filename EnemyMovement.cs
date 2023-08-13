using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Transform agent;
    NavMeshAgent meshAgent;
    public bool detectPlayer = false;
    AudioSource aud;
    [SerializeField]
    AudioClip footsteps;

    void Start()
    {
        meshAgent = FindObjectOfType<NavMeshAgent>();
        agent = GameObject.Find("Agent").GetComponent<Transform>();
        if (agent != null)
        {
            aud = GetComponent<AudioSource>();
            aud.clip = footsteps;
        }
        
    }
    private void FixedUpdate()
    {
        DetectPlayer();
    }

    void Update()
    {
        MoveTowardsPlayer();
       
    }

    void MoveTowardsPlayer()
    {
        meshAgent.SetDestination(agent.transform.position);
    }

    private float Square(float n)
    {
        return n * n;
    }

    void DetectPlayer()
    {
        Vector3 playerPos = agent.transform.position;
        float distance= Mathf.Sqrt(Square(playerPos.x-transform.position.x)+ Square(playerPos.y - transform.position.y)+ Square(playerPos.z - transform.position.z));
        if (distance <= 80f)
        {
            detectPlayer = true;
            if (!aud.isPlaying)
            {
                aud.PlayOneShot(footsteps);
            }
        }

        else if (distance > 100f)
        {
            detectPlayer = false;
            aud.Stop();
        }
        
    }
    
    

}
