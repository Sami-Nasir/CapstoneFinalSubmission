using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyEnemy : Enemy
{
    void Start()
    {
        StartCoroutine(ChangeRotation());
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Mummy");
        }
    }
}

