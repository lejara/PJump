using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EvilNPC : EvilObjects
{
    public bool followPlayer;
    public bool followPath;

    public GameObject startingPoint;
    public GameObject endPoint;

    private AIDestinationSetter ai_Dest_Setter;
    private AIPath aiPath;
    // Start is called before the first frame update
    void Start()
    {
        ai_Dest_Setter = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();
        
    }

    // Update is called once per frame
    void Update()
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
        print(aiPath.reachedDestination);
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
