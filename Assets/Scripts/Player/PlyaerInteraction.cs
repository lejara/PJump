using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyaerInteraction : MonoBehaviour
{
    public bool canIntereact = true;
    public bool shoot;
    public float shootVelocitySpeed;
    public GameObject BulletSpawn;
    public GameObject PlayerBullet;

    private bool respawn_Input = false;
    // Start is called before the first frame update
    void Start()
    {
        
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
                (Mouse.instance.gameObject.transform.position - transform.position).normalized);
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
