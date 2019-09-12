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

    public PlayerMovement playerMovement;

    public int health = 4;
    //make this instance static so it can be used across scripts
    public static Player instance = null;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
