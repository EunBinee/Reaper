using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMove : MonoBehaviour
{
    public Transform target; //����ٴ� Ÿ��
    private Transform tr; //ī�޶� �ڽ��� transform
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
