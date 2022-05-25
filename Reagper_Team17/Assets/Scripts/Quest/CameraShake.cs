using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float m_force = 0f;//어느정도 세기로 흔들릴 건지
    [SerializeField] Vector3 m_offset = Vector3.zero; //카메라가 흔들릴 방향을 결정하는 벡터

    Quaternion m_originRot; //카메라의 초기값을 저장할 Quaternion..

    public bool isShake = false;
    void Start()
    {
        m_originRot = transform.rotation;
        //카메라의 초기 회전값을 쿼터니온 변수에 넣어준다.
    }

    // Update is called once per frame
    void Update()
    {
        if (isShake)
        {
            StartShake();
            Invoke("EndShake", 5f);
        }

/*        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(ShakeCoroutine());
        }
        else if(Input.GetKeyDown(KeyCode.B))
        {
            StopAllCoroutines();
            StartCoroutine(Reset());
        }*/
    }

    public void StartShake()
    {
        m_originRot = transform.rotation;
        StartCoroutine(ShakeCoroutine());
        
    }
    void EndShake()
    {
        isShake = false;
        StopAllCoroutines();
        StartCoroutine(Reset());
    }
    IEnumerator ShakeCoroutine()
    {
        //카메라를 흔드는 코루틴

        Vector3 t_originEuler = transform.eulerAngles; //카메라의 오일러 초기값을 저장
        while (true)
        {
            float t_rotX = Random.Range(-m_offset.x, m_offset.x);
            float t_rotY = Random.Range(-m_offset.y, m_offset.y);
            float t_rotZ = Random.Range(-m_offset.z, m_offset.z);

            Vector3 t_RandomRot = t_originEuler + new Vector3(t_rotX, t_rotY, t_rotZ);
            //원래의 오일러 값에 랜덤값을 더해준다.

            Quaternion t_rot = Quaternion.Euler(t_RandomRot);
            //Queternion에서 쓸수있도록 vector3의 값을 변환시켜준다.

            while (Quaternion.Angle(transform.rotation,t_rot)>0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, t_rot, m_force * Time.deltaTime);
                yield return null;
                //반복문에 의해 랜덤하게 움직임
            }
            yield return null;
        }

    }

    IEnumerator Reset()
    {
        while (Quaternion.Angle(transform.rotation, m_originRot) > 0f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, m_originRot, m_force * Time.deltaTime);
            yield return null;
        }
    }
}
