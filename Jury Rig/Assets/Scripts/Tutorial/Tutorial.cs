using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    private bool _startGame = false;

    [SerializeField] private GameObject[] stages;

    public enum STAGE 
    {
        Intro,
        CheckCoreMachine,
        TakeItem,
        RepairMachine,
        Begin
    }

    public STAGE _stage = STAGE.Intro;



    private void Awake()
    {
        gameObject.SetActive(true);   
    }


    void Start()
    {
        _stage = STAGE.Intro;

    }

    void Update()
    {
        if(contrinue)
            UpdateStage();

        if(Input.GetKey(KeyCode.T) && _stage== STAGE.CheckCoreMachine)
        {
            _stage = STAGE.TakeItem;
        }
    }

    bool canStartGame => _startGame;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //StageCheckMachine
        if(collision.tag == "Player")
        {
            _stage = STAGE.CheckCoreMachine;

            contrinue = true;


        }
    }

    bool contrinue;

    void UpdateStage()
    {
        //for(int i = 0; i <= stages.Length; i++)
        //    stages[i].gameObject.SetActive(false);

        contrinue = false;

        switch(_stage)
        {
            case STAGE.Intro:
            stages[0].gameObject.SetActive(true);
            break;
            case STAGE.CheckCoreMachine:
            stages[1].gameObject.SetActive(true);
            break;
            case STAGE.TakeItem:
            stages[2].gameObject.SetActive(true);
            break;
            case STAGE.RepairMachine:
            stages[3].gameObject.SetActive(true);
            break;
            case STAGE.Begin:
            //stages[4].gameObject.SetActive(true);
            break;
        }
    }


    void StageMoveToMachine()
    {

    }

    void StageMachineCheck()
    {

    }

    void StageTakeItem()
    {

    }

    void StageRepairMachine()
    {

    }

    void StageBegin()
    {

    }


}
