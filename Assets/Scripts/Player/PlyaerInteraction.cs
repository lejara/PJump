using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyaerInteraction : MonoBehaviour
{
    public bool shoot;
    public float shootVelocitySpeed;
    public GameObject BulletSpawn;
    public GameObject PlayerBullet;

    private Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        MouseCursorUpdate();
        CheckShoot();
        
    }

    void MouseCursorUpdate()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void CheckShoot()
    {
        if (shoot)
        {
            GameObject obj = Instantiate(PlayerBullet, BulletSpawn.transform.position, this.transform.rotation);
            obj.GetComponent<PlayerBullet>().SetBullet(shootVelocitySpeed, (mousePos - transform.position).normalized);
        }
    }

    void CheckInput()
    {
        shoot = Input.GetButtonDown("Fire1");
    }
}
