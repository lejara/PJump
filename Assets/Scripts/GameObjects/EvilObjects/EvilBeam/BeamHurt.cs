using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamHurt : MonoBehaviour
{
    public bool active = false;
    public bool hasWarning = true;
    public bool isShootingBeam = false;
    public float activeTime = 1f;
    public float warningTime = 1f;

    private SpriteRenderer spriteRenderer;
    private Collider2D collider2D;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();

        collider2D.enabled = false;
        spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active && !isShootingBeam)
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
        collider2D.enabled = true;
        spriteRenderer.enabled = true;
        StartCoroutine(BeamShutOffDelay());
    }

    void DeactivateBeam()
    {
        isShootingBeam = false;
        collider2D.enabled = false;
        spriteRenderer.enabled = false;
        active = false;
    }

    IEnumerator BeamShutOffDelay()
    {
        yield return new WaitForSeconds(activeTime);
        DeactivateBeam();
    }

    IEnumerator BeamWarningWait()
    {
        yield return new WaitForSeconds(warningTime);
        ShootBeam();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            GameManager.instance.player.Hit();
        }
    }
}
