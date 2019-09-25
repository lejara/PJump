using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EvilNPC : EvilObjects
{
    public bool isActive = false;
    public bool followPlayer;
    public bool followPath;

    public GameObject startingPoint;
    public GameObject endPoint;

    private AIDestinationSetter ai_Dest_Setter;
    private AIPath aiPath;
    private CircleCollider2D collderCicle;
    // Start is called before the first frame update
    void Start()
    {
        ai_Dest_Setter = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();
        collderCicle = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isActive = true;
            collderCicle.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (followPlayer)
            {
                ai_Dest_Setter.target = GameManager.instance.player.transform;
            }
            else if (followPath)
            {
                if (aiPath.reachedDestination)
                {
                    ai_Dest_Setter.target = ai_Dest_Setter.target == startingPoint.transform ? endPoint.transform : startingPoint.transform;
                }
                else if (ai_Dest_Setter.target != startingPoint.transform && ai_Dest_Setter.target != endPoint.transform)
                {
                    ai_Dest_Setter.target = startingPoint.transform;
                }
            }
        }        
    }

    void OnDrawGizmos()
    {
        if (followPath)
        {
            Gizmos.color = Color.grey;
            Gizmos.DrawLine(transform.position, startingPoint.transform.position);
            Gizmos.DrawLine(transform.position, endPoint.transform.position);
        }

    }
}
