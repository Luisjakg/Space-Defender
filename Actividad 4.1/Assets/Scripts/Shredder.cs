using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var Asteroid = collision.GetComponent<Asteroid>();
        var Goal = collision.GetComponent<Goal>();
        if (!Goal)
        {
            Destroy(collision.gameObject);
        }

    }
}
