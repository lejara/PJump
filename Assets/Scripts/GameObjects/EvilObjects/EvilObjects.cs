using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EvilObjects : MonoBehaviour
{
    public bool isActive = false;
    public bool activateOnDamage = true;
    public bool canDie = true;
    public bool canHurtPlayer = true;
    public int health = 1;
    public string[] hurtTags;

    protected delegate void Damage();
    protected event Damage OnDamage;
    protected delegate void Dead();
    protected event Dead OnDeath;
    // Start is called before the first frame update
    void Start()
    {        
        OnDamage += this.LoseHealth;
        OnDeath += this.Despwan;
        
    }

    //TODO fix thism and make it public
    protected virtual void Activate()
    {
        isActive = true;        
    }
    //TODO make sure this is called instend of making it false directly, and fix it to make it public
    protected virtual void Dectivate()
    {
        isActive = false;
    }

    protected void OnHitSelf()
    {
        if (activateOnDamage && !isActive)
        {
            Activate();
        }        
        if (canDie)
        {
            LoseHealth();
        }
        
    }

    protected void LoseHealth()
    {
        health--;
        CheckHealth();
    }

    protected void CheckHealth()
    {
        if (health <= 0)
        {
            Despwan();
        }
    }

    protected void Despwan()
    {
        Destroy(this.gameObject, 0);
    }

    protected bool CheckHitPlayer(Collision2D collision)
    {
        if (canHurtPlayer && collision.gameObject.tag.Equals("Player"))
        {            
            GameManager.instance.player.Hit();
            return true;
        }
        return false;
    }
    protected void CheckHitSelf(Collision2D collision)
    {
        foreach (string tag in hurtTags)
        {
            if (collision.gameObject.tag.Equals(tag))
            {
                OnHitSelf();
            }
        }
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {        
        if (!CheckHitPlayer(collision))
        {
            CheckHitSelf(collision);
        }
        
        
    }

}
