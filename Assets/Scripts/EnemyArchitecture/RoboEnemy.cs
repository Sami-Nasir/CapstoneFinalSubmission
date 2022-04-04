using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboEnemy : Enemy
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(ChangeRotation());
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    public new void Movement()
    {
        pos = Vector3.forward;
        transform.Translate(pos * speed * Time.deltaTime);
        anim.SetBool("isWalking", true);
    }
    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Robot");
        }
    }
}