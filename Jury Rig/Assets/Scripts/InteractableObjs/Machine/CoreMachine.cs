using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreMachine : Machine
{


    internal void ReduceEnergy(float _drainAmmount)
    {
        tmp_hp -= _drainAmmount;
        //Debug.Log("CORE : Oh shit son...I got sucked :P ");
    }

    internal void TakePermantDamage(float directAttackDamage)
    {
        _maxHp -= directAttackDamage;

        //Debug.Log("CORE : Ouhc :P ");
        StartCoroutine(UpdateHPBar(tmp_hp - directAttackDamage));
    }

    IEnumerator UpdateHPBar(float newHp)
    {
        while(tmp_hp >= newHp)
        {
            yield return new WaitForSeconds(0.01f);
            tmp_hp -= 1f;
        }


    }

    protected override void Action()
    {
        base.Action();
        if(tmp_hp <=0)
        {
            //gameover
            GameOver();
        }
    }

    public GameObject deathMenu;
    void GameOver()
    {
        deathMenu.SetActive(true);
        Time.timeScale = 0;
    }

}
