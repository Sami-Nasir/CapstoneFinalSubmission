using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private float speed;
    private float angle = 0;
    private float pos;
    private float offset = 0.67f;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position.y;
        StartCoroutine(Dance());
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }
    private void Rotate()
    {
        angle += 25f;
        transform.rotation = Quaternion.Euler(0,angle, 0);
    }
    IEnumerator Dance()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            transform.Translate(0,-offset,0);
            yield return new WaitForSeconds(0.5f);
            transform.Translate(0,offset,0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
