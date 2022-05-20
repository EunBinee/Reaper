using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConditionBar : MonoBehaviour
{
    //�÷��̾ �� ������, ü�¹ٰ� �ε巴�� ����������..
    PlayerController playerController;

    public Slider conditionBar;
    public float maxHP = 1000f;
    public float currentHP = 1000f;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerController.condiZero) //false�϶�, �� ü���� �ٴ� �����ʾ�����.. ����
        {
            //conditionBar.value = Mathf.Lerp(conditionBar.value, currentHP / maxHP, Time.deltaTime );
            if(currentHP<=0)
            {
                playerController.condiZero = true;
            }
            if (currentHP >= maxHP)
            {
                currentHP = maxHP;
            }
            conditionBar.value = currentHP / maxHP;
        }
        else if (playerController.condiZero) //true�϶�, �� ü���� �ٴ� ������.. ����
        {
            //�ڵ����� ü���� ������
            //currentHP += 0.1f;
            conditionBar.value = currentHP / maxHP;

            if (currentHP >= maxHP) //���� ü���� Ǯ�� �� á����.!
            {
                currentHP = maxHP;
                playerController.condiZero = false; //�ٽ� ������ �� �ֵ���
            }
        }

        
    }
}
