using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Child class of EvilSpawner
 */

public class EvilThrowingSpwaner : EvilSpawner
{
    
    // Start is called before the first frame update
    void Start()
    {
        this.cicleCollider = GetComponent<CircleCollider2D>();
        setupLam = obj => {

            EvilThrowing spawnedObj = obj.GetComponent<EvilThrowing>();
            spawnedObj.SetupThrow(this.speedVelocity, this.direction, this.targetPlayer);
            spawnedObj.StartThrowing();

        };
        
    }

}
