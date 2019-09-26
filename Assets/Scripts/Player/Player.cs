using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Player aggregate class, for game logic
 * Author: lejara 
 * 
 */
public class Player : MonoBehaviour
{

    public bool isPlayerDead = false;
    public int health = 1;
    [HideInInspector]
    public int currentHealth;
    public PlayerMovement playerMovement;
    public PlayerInteraction playerInterection;
    //make this instance static so it can be used across scripts
    public static Player instance = null;

    private Animator animator;

    void Awake()
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
        currentHealth = health;
    }

    
    public void Respawn(Vector3 respawnLocation)
    {
        isPlayerDead = false;
        playerInterection.canIntereact = true;
        playerMovement.stopMoving = false;
        currentHealth = health;
        this.transform.position = respawnLocation;
    }

    public void Hit()
    {
        currentHealth--;
        CheckDeath();
    }

    void Update()
    {
        GameLogicAnimationTriggerSets();
    }

    void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            Dead();
        }
    }
    void Dead()
    {
        playerInterection.canIntereact = false;
        isPlayerDead = true;
        playerMovement.stopMoving = true;
        GameManager.instance.PlayerDied();
    }

    void GameLogicAnimationTriggerSets()
    {
        animator.SetBool("isDead", isPlayerDead);
    }

}
