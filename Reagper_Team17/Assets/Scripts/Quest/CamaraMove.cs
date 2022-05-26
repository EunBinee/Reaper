using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMove : MonoBehaviour
{
    public Transform target; //따라다닐 타겟
    private Transform tr; //카메라 자신의 transform
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
       // tr.position= new Vector3(t)
        
    }
}
