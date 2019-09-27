using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamHurt : MonoBehaviour
{
    public bool active = false;
    public bool hasWarning = true;
    public bool fireWarningShootOnly = false;
    public bool isShootingBeam = false;
    public float activeTime = 1f;
    public float warningTime = 1f;
    public delegate void BeamOn();
    public event BeamOn BeamOnEvent;
    public delegate void BeamOff();
    public event BeamOff BeamOffEvent;


    private SpriteRenderer spriteRenderer;
    private Collider2D col2D;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col2D = GetComponent<Collider2D>();

        col2D.enabled = false;
        spriteRenderer.enabled = false;

        BeamOnEvent += ReadyFire;
        BeamOffEvent += DeactivateBeam;

    }

    // Update is called once per frame
    void Update()
    {
        if (active && !isShootingBeam)
        {
            BeamOnEvent();
        }
    }

    void ReadyFire()
    {
        if (hasWarning)
        {
            WarningShoot();
        }
        else
        {
            ShootBeam();
        }
    }

    void WarningShoot()
    {
        var color = spriteRenderer.color;
        spriteRenderer.color = new Color(color.r, color.g, color.b, 0.2f);
        spriteRenderer.enabled = true;
        isShootingBeam = true;
        StartCoroutine(BeamWarningWait());
    }

    void ShootBeam()
    {
        var color = spriteRenderer.color;
        spriteRenderer.color = new Color(color.r, color.g, color.b, 0.8f);
        isShootingBeam = true;
        col2D.enabled = true;
        spriteRenderer.enabled = true;
        StartCoroutine(BeamShutOffDelay());
    }

    void DeactivateBeam()
    {
        isShootingBeam = false;
        col2D.enabled = false;
        spriteRenderer.enabled = false;
        active = false;
    }

    IEnumerator BeamShutOffDelay()
    {
        yield return new WaitForSeconds(activeTime);
        BeamOffEvent();
    }

    IEnumerator BeamWarningWait()
    {
        yield return new WaitForSeconds(warningTime);
        if (!fireWarningShootOnly)
        {
            ShootBeam();
        }
        else
        {
            BeamOffEvent();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            GameManager.instance.player.Hit();
        }
    }
}
