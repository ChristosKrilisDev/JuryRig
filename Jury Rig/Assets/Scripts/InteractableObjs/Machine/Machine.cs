using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Machine : MonoBehaviour
{
    #region Members
    [Header("BASE : Stats")]
    [SerializeField] protected float tmp_hp;
    [SerializeField] protected float _maxHp;
    [Space]
    [Header("BASE : UI")]
    [SerializeField] protected Image _hpSlider;

    [Space]
    [Header("BASE : AutoDestruction")]
    [SerializeField] [Range(0.0001f , 1f)] private float _autoDamage; //0.1hp dmg per t
    [SerializeField] private float _dmgTimerDelay; //0.1hp dmg per t
    [SerializeField] [Range(1f , 2f)] private float _repairDifficultyMultiplier;

    //local
    protected bool _isDead = true;
    private bool _needRepair = false;
    internal bool _isRepairing = false;

    //helpful vars
    //use it like trigger for the AytoDestruction function
    bool _flagOnce = false;
    #endregion

    #region Unity-GameMethods
    private void Awake()
    {
        Init();
    }

    void Start()
    {

    }

    protected virtual void Init()
    {
        tmp_hp = _maxHp;
        _hpSlider.fillAmount = _maxHp;

        _isDead = false;
        _needRepair = true;
    }

    void Update()
    {
        StartAutoDestruction();

        MachineStatusUpdate();
        HealthBarColorsUpdate();
        Action();
    }
    #endregion

    void StartAutoDestruction()
    {
        if(!_isDead  && !_flagOnce) //it call it only one time / repair
        {
            //_isRepairing = false;
            _flagOnce = true;
            _isDead = true;

            StartCoroutine(UpdateHPsliderValues());
        }
    }

    #region PublicMethods

    public bool NeedRepair => _needRepair;

    public void ForceStopRepairing()
    {
        _isDead = false;
        _isRepairing = false;
        _flagOnce = false;
    }

    public virtual void Repair()
    {
        StopCoroutine(UpdateHPsliderValues());
        StartCoroutine(RepairMachineHealth());
    }

    #endregion

    #region Base_Methods
    IEnumerator RepairMachineHealth()
    {
        Debug.Log("Here1");

        while(tmp_hp <= _maxHp)
        {
            yield return new WaitForSeconds(0.025f);
            _isRepairing = true;
            tmp_hp += 0.5f;
        }

        if(tmp_hp > _maxHp) //exp. tmp 105 and max 100
            tmp_hp = _maxHp;

        _isRepairing = false;
        _flagOnce = false; //allow to Destruction
        //_isMaxHp = false;
        Debug.Log("eko");

    }

    IEnumerator UpdateHPsliderValues()
    {
        ////First Wait | 1-5 sec
        //float randT = Random.Range(1 , 5);
        //yield return new WaitForSeconds(randT);
        //Debug.Log(randT);

        while(!_isRepairing)
        {
            //Debug.Log("Here2");
            yield return new WaitForSecondsRealtime(_dmgTimerDelay);

            tmp_hp -= _autoDamage;

            HealthBarColorsUpdate();

            if(_isDead)
                break;
        }
    }

    void HealthBarColorsUpdate()
    {
        if(tmp_hp >= _maxHp / 2 + 20)
            _hpSlider.color = Color.green;
        else if(tmp_hp < _maxHp / 2 + 20 && tmp_hp > _maxHp / 2 - 20)
            _hpSlider.color = Color.yellow;
        else
            _hpSlider.color = Color.red;
    }

    void MachineStatusUpdate()
    {
        //Update the health slide value 
        _hpSlider.fillAmount = tmp_hp / 100;

        _isDead = tmp_hp >= 0 ? false : true;

        if(!_isDead)
            if(tmp_hp >= 0 && (tmp_hp < _maxHp - 5))
                _needRepair = true;
            else if(_isRepairing)
                _needRepair = false;

            else
                _needRepair = false;
            
    }

    #endregion

    protected virtual void Action() { /*do somethin in child class */}

}
