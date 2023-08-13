using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip footSteps, shootingSound,hitSound, sniperSound;
    static AudioSource aud;


    void Start()
    {
        footSteps = Resources.Load<AudioClip>("Footsteps02");
        shootingSound = Resources.Load<AudioClip>("Shot");
        hitSound = Resources.Load<AudioClip>("Hit");
        sniperSound = Resources.Load<AudioClip>("explosion_sound");
    }

    
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "Footsteps":
                if (!aud.isPlaying)
                {
                    aud.PlayOneShot(footSteps);
                }
                break;
            case "Shoot":
                if (!aud.isPlaying)
                {
                    aud.PlayOneShot(shootingSound);
                }
                break;
            case "Sniper":
                aud.PlayOneShot(sniperSound);
                break;
            case "Hit":
                aud.PlayOneShot(hitSound);
                break;


        }
    }
}
