using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamShooter : EvilObjects
{
    public bool useTriggerStart = true;
    public bool shootInOrder = false;
    public bool shootAll = true;
    public bool shootInInverals = true;
    public float shootIntervalTime = 0.5f;
    public BeamHurt[] beamHurts;

    private int curretShootIndex = 0;
    private CircleCollider2D circleCollider2D;

    void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();

        foreach (BeamHurt beam in beamHurts)
        {
            beam.BeamOffEvent += ShootNextOrder;
        }

        if (!useTriggerStart)
        {
            Activate();
        }

    }

    protected override void Activate()
    {
        isActive = true;
        circleCollider2D.enabled = false;
        CheckNextInterval();
    }

    /* 
     * Shoots all the beam in the beamhurts array
     */
    void ShootAll()
    {
        if (isActive)
        {
            foreach (BeamHurt beam in beamHurts)
            {
                beam.active = true;
            }
            if (shootInInverals)
            {
                StartCoroutine(IntervalShootDelay());
            }
            
        }

    }

    void ShootInOrder()
    {
        curretShootIndex = 0;
        ShootNextOrder();        
    }

    void ShootNextOrder()
    {
        if (isActive && shootInOrder)
        {
            if (curretShootIndex < beamHurts.Length)
            {
                beamHurts[curretShootIndex].active = true;
                curretShootIndex++;
            }
            else if(shootInInverals)
            {               
                StartCoroutine(IntervalShootDelay());

            }
        }
    }

    /*
     * Called when and what to shoot on the shoot interval
     */
    void CheckNextInterval()
    {
        if (shootInOrder)
        {
            ShootInOrder();
        }
        else if (shootAll)
        {
            ShootAll();
        }
    }
    /*
     * Wait for the next shoot interval
     */
    IEnumerator IntervalShootDelay()
    {        
        yield return new WaitForSeconds(shootIntervalTime);
        CheckNextInterval();
    }

    /*
     * Detection when to activate this gmaeobject
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && useTriggerStart)
        {
            Activate();
        }
    }
}
