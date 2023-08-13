using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    Ray ray;
    EnemyMovement enemyMovement;
    public bool isPlayer;
    [SerializeField]
    float fireRate=0.5f;
    float canFire = 0f;
    [SerializeField]
    GameObject[] gunActive;
    public int gunId;
    public int ammo;
    public int maxAmmo;
    [SerializeField]
    float fov = 60f, fovAK = 30f,fovSniper=15f;
    [SerializeField]
    ParticleSystem bulletEffects;
    float bornTime;
    [SerializeField]
    GameObject bulletPrefab;
    //public int enemyID;
    int headDamage;
    int bodyDamage;
    [SerializeField]
    float minFireRate=0f;
    [SerializeField]
    float maxFireRate=5f;
    [SerializeField]
    int[] currentAmmo;
    bool canShoot = true;
    float animTime;
    [SerializeField]
    AudioSource audShoot;
    [SerializeField]
    AudioClip shootingClip;
    Animator anim;
    UIManager uiManager;
    [SerializeField]
    ParticleSystem bloodFX;
    
    private void Awake()
    {
        if (isPlayer)
        {
            gunActive[0].SetActive(true);
            for (int i = 1; i < gunActive.Length; i++)
            {
                gunActive[i].SetActive(false);
                anim = GetComponent<Animator>();
                
            }
            gunId = 0;

        }
        
        bornTime = Time.time;
    }
    void Start()
    {
        /*audShoot = GetComponent<AudioSource>();
        if (audShoot != null)
        {
            audShoot.clip = shootingClip;
        }*/
        if (!isPlayer)
        {
            enemyMovement = GetComponent<EnemyMovement>();
        }
        else if (isPlayer)
        {
            uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        }
    }

    void Update()
    {
        ShootingBullet();
        if (isPlayer &&uiManager.isAlive)
        {
            ChangeGun();
            Ads();
            Reload();
            DamageModifiers(gunId);
            uiManager.AmmoDisplay(currentAmmo[gunId], maxAmmo);
        }
        
    }

    void ShootingBullet()
    {
        if (isPlayer)
        {
            if (Input.GetMouseButton(0) && Time.time>canFire && canShoot &&uiManager.isAlive)
            {
                //if (!aud.isPlaying)
                if (gunId == 0)
                {
                    anim.SetBool("pistolShooting", true);
                }
                else if (gunId == 1)
                {
                    anim.SetBool("akShooting", true);
                }
                else if (gunId == 2)
                {
                    anim.SetBool("smgShooting", true);
                }
                {
                    audShoot.PlayOneShot(shootingClip);
                }
                if (currentAmmo[gunId] > 0)
                {
                    if (!bulletEffects.isPlaying)
                    {
                        bulletEffects.Play();
                    }
                    canFire = Time.time + fireRate;
                    ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                    RaycastHit hitInfo;
                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        Debug.Log("Hit " + hitInfo.transform.name);
                        if (hitInfo.transform.name == "Head")
                        {
                            Instantiate(bloodFX, hitInfo.transform.position, Quaternion.identity);
                            bloodFX.Play();
                            GameObject enemy = hitInfo.transform.parent.gameObject;
                            enemy.GetComponent<Health>().DealDamage(headDamage);



                        }
                        else if (hitInfo.transform.name == "Body")
                        {
                            Instantiate(bloodFX, hitInfo.transform.position, Quaternion.identity);
                            bloodFX.Play();
                            GameObject enemy = hitInfo.transform.parent.gameObject;
                            enemy.GetComponent<Health>().DealDamage(bodyDamage);
                        }

                        ammo--;
                        currentAmmo[gunId]--;
                        
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                anim.SetBool("pistolShooting", false);
                anim.SetBool("akShooting", false);
                anim.SetBool("smgShooting", false);
            }
            
        }

        else if (!isPlayer&& enemyMovement.detectPlayer)
        {

            fireRate = Random.Range(minFireRate, maxFireRate);
            Vector3 pos = transform.position;
            if ((Time.time - bornTime) > canFire)
            {
                //if (!aud.isPlaying)
                {
                    audShoot.PlayOneShot(shootingClip);
                }
                if (!bulletEffects.isPlaying)
                {
                    
                    bulletEffects.Play();
                }
                GameObject instBullet= Instantiate(bulletPrefab, Vector3.zero, transform.rotation);
                instBullet.transform.parent = transform;
                instBullet.transform.localPosition = transform.localPosition;
                

                //Debug.Log("Bullet at position: " + bulletPrefab.transform.position);
               // Debug.Log("Bullet barer at position: " + transform.position);

                canFire = (Time.time - bornTime) + fireRate;
                
            }
        }
    }
    void ChangeGun()
    {
        if (Input.mouseScrollDelta == Vector2.up)
        {
            Camera.main.fieldOfView = fov;
            gunId++;
            if (gunId > gunActive.Length-1)
            {
                gunId = 0;
            }
            
            for (int i = 0; i < gunActive.Length; i++)
            {
                gunActive[i].SetActive(false);
            }
            gunActive[gunId].SetActive(true);
            
        }
        else if (Input.mouseScrollDelta == Vector2.down)
        {
            Camera.main.fieldOfView = fov;
            gunId--;
            if (gunId < 0)
            {
                gunId = gunActive.Length-1;
            }
            
            for (int i = 0; i < gunActive.Length; i++)
            {
                gunActive[i].SetActive(false);
            }
            gunActive[gunId].SetActive(true);
            
        }
        ammo = currentAmmo[gunId];
        
    }

    void Ads()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if(gunId==1)
            Camera.main.fieldOfView = fovAK;

            else if (gunId == 3)
            {
                Camera.main.fieldOfView = fovSniper;
            }
        }

        else if (Input.GetMouseButtonUp(1))
        {
            
            Camera.main.fieldOfView = fov;
        }
    }

    void DamageModifiers(int gunId)
    {
       // ammo = currentAmmo[gunId];
        if (gunId == 0)
        {
            headDamage = 130;
            bodyDamage = 55;
            fireRate = 0.5f;
            maxAmmo = 8;
            animTime = 2f;

        }
        else if (gunId == 1)
        {
            headDamage = 160;
            bodyDamage = 40;
            fireRate = 0.15f;
            maxAmmo = 25;
            animTime = 2.5f;
        }
        else if (gunId == 2)
        {
            headDamage = 75;
            bodyDamage = 25;
            fireRate = 0.075f;
            maxAmmo = 30;
            animTime = 1.5f;
        }
        else if (gunId == 3)
        {
            headDamage = 255;
            bodyDamage = 150;
            fireRate = 2f;
            maxAmmo = 5;
            animTime = 3f;
        }
    }

    void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {

            StartCoroutine(ReloadAnim(animTime));
        }

        else if (ammo == 0)
        {

            StartCoroutine(ReloadAnim(animTime));
        }
    }

    IEnumerator ReloadAnim(float t)
    {
        canShoot = false;
        uiManager.ReloadingTextVisibility(true);
        yield return new WaitForSeconds(t);
        canShoot = true;
        ammo = maxAmmo;
        currentAmmo[gunId] = maxAmmo;
        uiManager.ReloadingTextVisibility(false);
    }
}
