/*
 * Player aggregate class, for game logic
 * Author: lejara 
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    public bool isPlayerDead = false;
    public int health = 1;    
    //make this instance static so it can be used across scripts
    public static Player instance = null;
    public PlayerMovement playerMovement;

    private Animator animator;
    private void Awake()
    {
        //Set the instance only once.
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            //Enforces that there will always be one instance of a gameObject. This is for type errors prevention
            Destroy(gameObject);
            Debug.LogWarning("Another instance of Player have been created and destoryed!");
        }

        //Makes the gameobject not be unloaded when entering a new scene
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        GameLogicAnimationTriggerSets();
    }

    void CheckDeath()
    {
        if (health <= 0)
        {
            Dead();
        }
    }
    void Dead()
    {
        isPlayerDead = true;
        playerMovement.stopMoving = true;
        GameManager.instance.PlayerDied();
    }

    void GameLogicAnimationTriggerSets()
    {
        animator.SetBool("isDead", isPlayerDead);
    }

    public void Hit()
    {
        
        health--;
        CheckDeath();
    }
}
