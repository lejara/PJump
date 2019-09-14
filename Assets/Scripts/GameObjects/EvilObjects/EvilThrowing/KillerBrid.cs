using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerBrid : EvilThrowing
{
    public bool isPartOfHive = true;

    void Awake()
    {
        this.rigidb = GetComponent<Rigidbody2D>();
        this.cicleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }



}
