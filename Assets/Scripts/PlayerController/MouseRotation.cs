using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotation : MonoBehaviour
{
    private float mouseX;
    [SerializeField] private float speedMouse = 20f;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rotation();
    }
    private void Rotation()
    {
        mouseX = Input.GetAxis("Mouse X");
        player.Rotate(mouseX * speedMouse * Vector3.up);
    }
}
