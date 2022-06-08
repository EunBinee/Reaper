using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConditionBar : MonoBehaviour
{
    //플레이어가 뛸 때마다, 체력바가 부드럽게 떨어지도록..
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
        if(!playerController.condiZero) //false일때, 즉 체력이 바닥 나지않았을때.. 실행
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
        else if (playerController.condiZero) //true일때, 즉 체력이 바닥 났을때.. 실행
        {
            //자동으로 체력이 오르게
            //currentHP += 0.1f;
            conditionBar.value = currentHP / maxHP;

            if (currentHP >= maxHP) //만약 체력이 풀로 다 찼으면.!
            {
                currentHP = maxHP;
                playerController.condiZero = false; //다시 움직일 수 있도록
            }
        }

        
    }

    public void MaxHP()
    {
        currentHP = maxHP;
    }
}
