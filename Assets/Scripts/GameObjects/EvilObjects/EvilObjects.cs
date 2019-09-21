using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EvilObjects : MonoBehaviour
{
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

    // Update is called once per frame
    void Update()
    {
        
    }
    protected void OnHitSelf()
    {
        LoseHealth();
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

    protected bool CheckPlayerHit(Collision2D collision)
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
        if (canDie)
        {
            foreach (string tag in hurtTags)
            {
                if (collision.gameObject.tag.Equals(tag))
                {
                    OnHitSelf();
                }
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!CheckPlayerHit(collision))
        {
            CheckHitSelf(collision);
        }
        
        
    }

}
