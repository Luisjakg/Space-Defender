using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private int timeToAppear = 5;
    private bool move = false;

    private void Start()
    {
        StartCoroutine(WaitAndMove());
    }

    private void Update()
    {
        if (move)
        {
            transform.Translate(Vector2.down * Time.deltaTime * moveSpeed);
        }
    }






    IEnumerator WaitAndMove()
    {
        yield return new WaitForSeconds(timeToAppear);
        move = true; 
    }
}
