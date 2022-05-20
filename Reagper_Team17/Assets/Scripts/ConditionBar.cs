using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConditionBar : MonoBehaviour
{
    //�÷��̾ �� ������, ü�¹ٰ� �ε巴�� ����������..
    public Slider conditionBar;
    public float maxHP = 1000f;
    public float currentHP = 1000f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //conditionBar.value = Mathf.Lerp(conditionBar.value, currentHP / maxHP, Time.deltaTime );
        if(currentHP>=maxHP)
        {
            currentHP = maxHP;
        }
        conditionBar.value = currentHP / maxHP;
        
    }
}
