using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMachine : Machine
{
    [Header("SUB : MEMBERS TO DMG THE CORE")]
    [Space]
    [SerializeField] private float _drainAmmountDmg = 0.1f;
    [SerializeField] private float _delay = 15f;
    [SerializeField] private bool _canDrainEnergy = true;

    [SerializeField] private float _directAttackDamage = 10; //reduce the Core's max hp
    //flag
    private bool _doesHaveDirectAttack = true;

    [SerializeField] private CoreMachine coreMachine;

    /// <summary>
    /// Drain energy for core machine(hp/time)
    /// </summary>


    protected override void Init()
    {
        base.Init();

        //add more Init validations
        //dmg , hp drain
        StartCoroutine(Drain());
    }

    //Each repair reset the direct atk for the small machines
    public override void Repair()
    {
        base.Repair();

        ScoreManager.Instance.AddPoints(10);

        //reset
        _doesHaveDirectAttack = true;
        StartCoroutine(Drain());
    }

    //--Child

    #region ChildActions
    protected override void Action()
    {
        base.Action();

        if(_isDead)
        {
            _canDrainEnergy = false;
            StopCoroutine(Drain());

            if(_doesHaveDirectAttack)
                DamageCoreMachine();
        }
    }

    IEnumerator Drain()
    {
        while(_canDrainEnergy)
        {
            yield return new WaitForSeconds(_delay);

            coreMachine.ReduceEnergy(_drainAmmountDmg);
        }
    }
    void DamageCoreMachine()
    {
        _doesHaveDirectAttack = false;
        coreMachine.TakePermantDamage(_directAttackDamage);
    }

    #endregion

}
