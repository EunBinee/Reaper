using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public Transform target;
    public PlayerController playerController;
    private Transform tr;
    float cameraSpeed;

    int curFloor;
    int preFloor;

    bool changeFloor;
    public Vector3 Floor1F;
    public Vector3 Floor2F;
    void Start()
    {
        tr = GetComponent<Transform>();
        cameraSpeed = playerController.movementSpeed;

        curFloor = playerController.GetFloor();
        preFloor = playerController.GetFloor();
    }
    private void Update()
    {
        curFloor = playerController.GetFloor();
        if (preFloor != curFloor)
        {
            preFloor = playerController.GetFloor();
        }

    }

    private void LateUpdate()
    {
        if (curFloor == 1)
        {
            //1類曖 唳辦
            tr.position = new Vector3(target.position.x - 0.52f, Floor1F.y, Floor1F.z);
        }
        else if(curFloor==2)
        {
            //2類曖 唳辦
            tr.position = new Vector3(target.position.x - 0.52f, Floor2F.y, Floor2F.z);
        }
    }


}
    

