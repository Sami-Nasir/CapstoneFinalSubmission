using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHandler : MonoBehaviour
{
    [SerializeField] private float speed;
    public float angle = 0;
    public GameObject bullet;
    private Rigidbody Bullet;
    private GameObject temp;
    public Transform leftGun;
    public Transform rightGun;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeRotation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ChangeRotation()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            angle += 90;
            transform.rotation = Quaternion.Euler(0, angle, 0);
            LeftGun();
            RightGun();
        }
    }
    private void LeftGun()
    {
        bullet.transform.position = leftGun.transform.position;
        Bullet = Instantiate(bullet).GetComponent<Rigidbody>();
        Vector3 rotation = bullet.transform.rotation.eulerAngles;
        bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);
        Bullet.AddForce(leftGun.right * speed, ForceMode.Impulse);
    }
    private void RightGun()
    {
        bullet.transform.position = rightGun.transform.position;
        Bullet = Instantiate(bullet).GetComponent<Rigidbody>();
        Vector3 rotation = bullet.transform.rotation.eulerAngles;
        bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);
        Bullet.AddForce(rightGun.right * speed, ForceMode.Impulse);
    }
}
