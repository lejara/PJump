using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EvilThrowing : EvilObjects
{
    public bool useStartingTrigger = false;
    public bool followPlayer = false;   
    public float velocitySpeed;
    public Vector3 direction;

    protected bool throwing = false;
    protected CircleCollider2D cicleCollider;
    protected Rigidbody2D rigidb;

    private bool readyForFollowPlayer = true;
    // Start is called before the first frame update
    void Start()
    {        
        direction = direction.normalized;

        if (!useStartingTrigger)
        {
            StartThrowing();
        }
    }

    public void SetupThrow(float velSpeed, Vector3 dir, bool follPlayer = false)
    {
        velocitySpeed = velSpeed;
        direction = dir;
        direction = direction.normalized;
        followPlayer = follPlayer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Player"))
        {
            if (throwing)
            {
                GameManager.instance.player.Hit();
            }
            else if(useStartingTrigger)
            {

                StartThrowing();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("CameraBounds"))
        {
            Destroy(this.gameObject, 1);
        }
    }

    void CheckFollowPlayer()
    {
        if (followPlayer)
        {
            if (readyForFollowPlayer)
            {
                direction = (GameManager.instance.player.transform.position - transform.position).normalized;
            }            
            readyForFollowPlayer = false;
        }
        else
        {
            readyForFollowPlayer = true;
        }
    }

   void FixedUpdate()
    {
        if (throwing)
        {
            CheckFollowPlayer();
            Throw();
        }
    }

    public void StartThrowing()
    {
        throwing = true;
        cicleCollider.enabled = false;
        CheckFollowPlayer();
    }

    protected void Throw()
    {
       
        rigidb.velocity = direction * velocitySpeed;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (direction * 1.2f));
    }
}
