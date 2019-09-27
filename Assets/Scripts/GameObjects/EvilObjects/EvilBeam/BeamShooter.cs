using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamShooter : EvilObjects
{

    public bool shootInOrder = false;
    public bool shootAll = true;
    public bool shootInInverals = true;

    public float shootIntervalTime = 0.5f;
    public BeamHurt[] beamHurts;

    private CircleCollider2D circleCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    protected override void Activate()
    {
        isActive = true;
        circleCollider2D.enabled = false;
        CheckNextInterval();
    }

    void ShootAll()
    {
        if (isActive)
        {
            foreach (BeamHurt beam in beamHurts)
            {
                beam.active = true;
            }

            StartCoroutine(IntervalShootDelay());
        }

    }

    void ShootInOrder()
    {
        //TODO: finish dis
    }

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
    IEnumerator IntervalShootDelay()
    {
        
        yield return new WaitForSeconds(shootIntervalTime);
        CheckNextInterval();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Activate();
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}
