using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider HPSlider;
    public Slider EaseSlider;
    public float maxHP;
    public float HP;
    private float EaseLerp = .05f; //speed of ease change
    private float HPlerp = .2f; //quickly change current hp smoothly

    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;//initialized hp to max before starting
        HPSlider.maxValue = maxHP;
        EaseSlider.maxValue = maxHP;
        //this should set the max value of sliders to value initiated in the parent object(player)
        HPSlider.value = HP;
    }

    // Update is called once per frame
    void Update()
    {
        if (HPSlider.value != HP)
        {
            HPSlider.value = Mathf.Lerp(HPSlider.value, HP, HPlerp); //(slider value, current hp, value change speed
        }
        if (HPSlider.value != EaseSlider.value)
        {
            EaseSlider.value = Mathf.Lerp(EaseSlider.value, HP, EaseLerp); //slider value, hp, smoothly change slider value to hp value
        }
        if (HP > maxHP)
        {
            HP = maxHP; //prevents overcapping health
        }

    }
    public void Damage(int amount) //damage taken method
    {
        HP -= amount;
        if (HP <= 0) //prevents going to negative hp by forcing 0 hp
        {
            HP = 0;
            HPSlider.value = 0;
            //game over popup should be added around here
        }
    }
}
