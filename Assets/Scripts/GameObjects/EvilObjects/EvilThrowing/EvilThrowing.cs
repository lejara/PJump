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

    protected override void Activate()
    {

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

    void FixedUpdate()
    {
        if (throwing)
        {
            CheckFollowPlayer();
            Throw();
        }
    }

    /*
     * Overriding Detection when to start throwing, or when this object has hit the player
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckHitSelf(collision);

        if (collision.gameObject.tag.Equals("Player"))
        {
            if (throwing && canHurtPlayer)
            {                
                GameManager.instance.player.Hit();
                Despwan();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (useStartingTrigger)
            {
                StartThrowing();
            }
        }
    }

    /*
     * Destory the object when its out of the world bounds
     * Leo Note: if lag becomes a problem, 
     * need to add a recycling system of this object
     */
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("CameraBounds"))
        {
            Despwan();
        }
    }

    /*
     * Checks if the followPlayer bool is true. 
     * If so change the direction to the current player's location.
     * Does not update the direction afterwards
     */
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (direction * 1.2f));
    }
}
