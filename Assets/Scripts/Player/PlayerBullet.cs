using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float velocitySpeed;
    public Vector3 direction;

    private Rigidbody2D rigidb;
    // Start is called before the first frame update
    void Start()
    {
        //direction = Vector3.zero;
        //velocitySpeed = 0;
        rigidb = GetComponent<Rigidbody2D>();
    }

    public void SetBullet(float velSpeed, Vector3 dir)
    {
        
        velocitySpeed = velSpeed;
        direction = dir;
    }
    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {

        rigidb.velocity = direction * velocitySpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.tag.Equals("CameraBounds") && !collision.gameObject.tag.Equals("Player"))
        {

            Destroy(this.gameObject);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("CameraBounds"))
        {
            Destroy(this.gameObject);

        }
    }
}
