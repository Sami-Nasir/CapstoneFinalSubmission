using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector3 pos;
    public float speed = 2f;
    public float angle = 0f;

    public void Movement()
    {
        pos = Vector3.forward;
        transform.Translate(pos * speed * Time.deltaTime);
    }
    public virtual void Kill()
    {
        //body
    }
    public IEnumerator ChangeRotation()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            angle += 90;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy Class");
        }
    }
}
