using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilThrowingSpwaner : EvilObjects
{
    public bool useTriggerStart = true;
    public bool spwaning = false;
    public bool targetPlayer = false;
    public float spawnRatePerSecond = 1;
    public float speedVelocity = 1f;
    public Vector3 direction;

    public GameObject evilThrowPrefab;

    public List<EvilThrowing> spawedObjects;

    private bool notSpawning = false;
    private CircleCollider2D cicleCollider;
    // Start is called before the first frame update
    void Start()
    {
        spawedObjects = new List<EvilThrowing>();
        cicleCollider = GetComponent<CircleCollider2D>();
        direction = direction.normalized;

        if (!useTriggerStart)
        {
            StartSpwaning();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (useTriggerStart)
            {                
                StartSpwaning();
            }
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (notSpawning && spwaning)
        {
            StartSpwaning();
        }
    }

    void StartSpwaning()
    {
        spwaning = true;
        notSpawning = false;
        cicleCollider.enabled = false;
        SpawnObject();
    }

    void SpawnObject()
    {
        if (spwaning)
        {
            
            GameObject obj = Instantiate(evilThrowPrefab, transform.position, transform.rotation);
            EvilThrowing spawnedObj = obj.GetComponent<EvilThrowing>();            
            spawnedObj.SetupThrow(speedVelocity, direction, targetPlayer);
            spawnedObj.StartThrowing();
            spawedObjects.Add(spawnedObj);
            StartCoroutine(SpawnDely());          
        }
        else
        {
            notSpawning = true;
        }
    }

    IEnumerator SpawnDely()
    {
        yield return new WaitForSeconds(spawnRatePerSecond);
        SpawnObject();
    }

    void OnDrawGizmos()
    {
        if (!targetPlayer)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + (direction * 1.2f));
        }

    }
}
