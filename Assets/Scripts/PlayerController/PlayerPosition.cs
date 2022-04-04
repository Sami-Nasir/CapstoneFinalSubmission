using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerPosition : MonoBehaviour
{
    [SerializeField]public Transform playerPos;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0f, 3f, 5.5f);
    }
    // Update is called once per frame
    void Update() 
    {
       
    }
    private void LateUpdate()
    {
        transform.position = playerPos.transform.position;
    }
}
