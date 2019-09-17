using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Abstract class for spawning anything
 * 
 * Setup: child class must; set setupLam, and cicleCollider
 * 
 */

public abstract class EvilSpawner : EvilObjects
{

    public bool useTriggerStart = true;
    public bool spawning = false;
    public bool targetPlayer = false;
    public float spawnRatePerSecond = 1;
    public float speedVelocity = 1f;    
    public GameObject spawnPrefab;
    public GameObject directionObj;
    //public List<GameObject> spawedObjects;

    protected Vector3 direction;
    protected Action<GameObject> setupLam;
    protected CircleCollider2D cicleCollider;

    private bool notSpawning = false;
    

    // Start is called before the first frame update
    void Start()
    {
        //spawedObjects = new List<GameObject>();       

        if (!useTriggerStart)
        {
            StartSpawning();
        }
    }

    // Update is called once per frame
    void Update()
    {
        direction = (directionObj.transform.position - transform.position).normalized;
        
        if (notSpawning && spawning)
        {
            StartSpawning();
        }
    }

    /*
     * Circle Collider  Trigger to start the spawning when it detects the player.
     * if useTriggerStart is true
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (useTriggerStart)
            {
                StartSpawning();
            }

        }
    }

    void StartSpawning()
    {
        spawning = true;
        notSpawning = false;
        cicleCollider.enabled = false;
        SpawnObject();
    }

    void SpawnObject()
    {

        if (spawning)
        {
            GameObject obj = Instantiate(spawnPrefab, 
                                        transform.position, 
                                        transform.rotation);
            setupLam(obj);
            //spawedObjects.Add(obj);
            StartCoroutine(SpawnNext());
        }
        else
        {
            notSpawning = true;
        }
    }

    IEnumerator SpawnNext()
    {
        yield return new WaitForSeconds(spawnRatePerSecond);
        SpawnObject();
    }

    void OnDrawGizmos()
    {
        if (!targetPlayer)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, 
                            directionObj.transform.position);
        }

    }
}
