using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerBrid : EvilThrowing
{

    void Awake()
    {
        this.rigidb = GetComponent<Rigidbody2D>();
        this.cicleCollider = GetComponent<CircleCollider2D>();
    }

}
