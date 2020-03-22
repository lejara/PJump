using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public bool canIntereact = true;
    public bool shoot;
    public float shootVelocitySpeed;
    public GameObject BulletSpawn;
    public GameObject PlayerBullet;
    public AudioClip shoot_sound;

    
    private bool respawn_Input = false;
    public AudioSource audioSource;
    void Awake()
    {
        //audioSource.GetComponent<AudioSource>();

        //TODO: make this line support for multipul different clips
        audioSource.clip = shoot_sound;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckRespawn();

        if (canIntereact)
        {            
            CheckShoot();            
        }

    }

    void CheckShoot()
    {
        if (shoot)
        {
            GameObject obj = Instantiate(PlayerBullet, BulletSpawn.transform.position, this.transform.rotation);

            obj.GetComponent<PlayerBullet>().SetBullet(shootVelocitySpeed, 
                (Mouse.instance.gameObject.transform.position - BulletSpawn.gameObject.transform.position).normalized);
            audioSource.Play();
        }
    }

    void CheckRespawn()
    {
        if (respawn_Input)
        {
            GameManager.instance.PlayerRespawn();
        }
    }

    void CheckInput()
    {
        shoot = Input.GetButtonDown("Fire1");
        respawn_Input = Input.GetButtonDown("Respawn");
    }
}
