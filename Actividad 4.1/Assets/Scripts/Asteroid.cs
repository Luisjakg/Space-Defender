using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float xPush = 2f;
    [SerializeField] private float yPush = 4f;
    [SerializeField] private GameObject explosionVfxPrefab; 


    private Rigidbody2D myRigidBody2D;
    
    // Start is called before the first frame update
    private void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myRigidBody2D.velocity = new Vector2(xPush * Random.Range(0,2)*2-1 , yPush * Random.Range(1,10));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0f,100f) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var Player = other.GetComponent<Player>();

        if (Player)
        {
            var explosion = Instantiate(explosionVfxPrefab, 
                transform.position,
                transform.rotation);
        }
        
        if (myRigidBody2D.velocity.x > 0)
        {
            myRigidBody2D.velocity = new Vector2(-xPush, yPush);
        }
        else if (myRigidBody2D.velocity.y < 0)
        {
            myRigidBody2D.velocity = new Vector2(xPush, yPush);
        }
    }

    public void Randomize()
    {
        myRigidBody2D.velocity = new Vector2(xPush * Random.Range(0,2)*2-1 , yPush);
    }
}
