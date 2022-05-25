using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float m_force = 0f;//������� ����� ��鸱 ����
    [SerializeField] Vector3 m_offset = Vector3.zero; //ī�޶� ��鸱 ������ �����ϴ� ����

    Quaternion m_originRot; //ī�޶��� �ʱⰪ�� ������ Quaternion..

    public bool isShake = false;
    void Start()
    {
        m_originRot = transform.rotation;
        //ī�޶��� �ʱ� ȸ������ ���ʹϿ� ������ �־��ش�.
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
        //ī�޶� ���� �ڷ�ƾ

        Vector3 t_originEuler = transform.eulerAngles; //ī�޶��� ���Ϸ� �ʱⰪ�� ����
        while (true)
        {
            float t_rotX = Random.Range(-m_offset.x, m_offset.x);
            float t_rotY = Random.Range(-m_offset.y, m_offset.y);
            float t_rotZ = Random.Range(-m_offset.z, m_offset.z);

            Vector3 t_RandomRot = t_originEuler + new Vector3(t_rotX, t_rotY, t_rotZ);
            //������ ���Ϸ� ���� �������� �����ش�.

            Quaternion t_rot = Quaternion.Euler(t_RandomRot);
            //Queternion���� �����ֵ��� vector3�� ���� ��ȯ�����ش�.

            while (Quaternion.Angle(transform.rotation,t_rot)>0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, t_rot, m_force * Time.deltaTime);
                yield return null;
                //�ݺ����� ���� �����ϰ� ������
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
