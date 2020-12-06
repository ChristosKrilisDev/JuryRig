using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerMovement : MonoBehaviour
{

    #region Members
    [Header("Stats")]
    [SerializeField] private float _runSpeed = 40f;

    private float _horizontalMove = 0;
    private bool _isJumping = false;
    private bool _isCrouching = false;


    private CharacterController2D _controller;
    private Animator _animator;


    //Items
    [Header("Item Members UI")]
    [Space]
    private Item _item;
    private bool _isNearItem;
    //Items UI
    [SerializeField] private Item _holdingItem;
    [SerializeField] private Image _itemBorder;


    //machine
    private Machine _machine;
    bool _isNearMachine;
    bool _isRepairing;

    ////portal
    //private Portal _portal;
    //bool _isNearPortal = false;

    //work members
    float _workTime = 5f;
    float _workCounterTime = 0f;

    //player interaction ui
    //[Header("PlayerUi")]
    //private InteractableObject _interactable;
    //Image _progBar;
    //Image _interactionImg;

    //flag
    //helpful vars
    //use it like trigger for the AutoDestruction function
    bool _isCompletePressing = false;


    //wall detection
    //private bool _isCloseToWall = false;
    private PlayerHUD HUD;

    #endregion

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _controller = gameObject.GetComponent<CharacterController2D>();
        _animator = gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>();
        HUD = GetComponent<PlayerHUD>();

        //_interactable = GetComponent<InteractableObject>();
        //_progBar = _interactable.interactionUI.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        //_interactionImg = _interactable.interactionUI.transform.GetChild(1).GetComponent<Image>();
        //_progText = _interactable.interactionUI.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();

        _isJumping = false;
        _isCrouching = false;

        //items
        //_isHoldingItem = false;
        _holdingItem = null;
        ResetItemUI();
    }

    private void Update()
    {
        //if(_isCloseToWall)
        //    return;
        PlayerInputs();
        getUpdatedAnimaationsParametres();
    }

    private void FixedUpdate()
    {
        //Move
        _controller.Move(_horizontalMove * Time.fixedDeltaTime , _isCrouching , _isJumping);
        //_isJumping = false;
    }

    void PlayerInputs()
    {
        PlayerActionsInputs();

        //while the player repair something cant move or drop the item
        if(_isRepairing)
            return;

        MoveInput();
    }

    void PlayerActionsInputs()
    {
        #region Machine
        if(_machine != null)
        {
            if(Input.GetKeyUp(KeyCode.E)) //cancel action
            {
                MachineActionReset();

                _machine.ForceStopRepairing();

                //flag
                _isCompletePressing = false;
            }

            if(Input.GetKey(KeyCode.E) && _machine.NeedRepair && _isNearMachine && !_isCompletePressing && _holdingItem)
            {

                _horizontalMove = 0; //0 movement to avoid getting stack with a value in the Move()

                _machine._isRepairing = _isRepairing = true; //Lock the playerMovement and the AutoKill machince()

                if(_workCounterTime <= _workTime)   //Counting pressed time
                {
                    //count the work time
                    _workCounterTime += +3f * +0.5f * Time.deltaTime;

                    //Progression UI visual
                    //_interactable.ActivateHUD(true);
                    HUD.OnMachineInteractionEnter(true);
                    //add sfx

                    //bar has value 0-1
                    //if(_progBar.gameObject.activeSelf)
                    HUD.UpdateWorkSlider(_workCounterTime / _workTime);

                    //call some vfs/sfx to the machine here !!
                }
                else if(_workCounterTime >= _workTime) // player have reached the max work time, repair the machine
                {

                    MachineActionReset();

                    //remove item // maybe give it to the machine
                    UseItem();

                    //flag
                    _isCompletePressing = true;

                    _machine.Repair();

                }
            }

        }
        #endregion

        #region ItemInteraction
        if(_isNearItem && !_isRepairing)
        {
            if(Input.GetKeyDown(KeyCode.E) && _item != null)
            {
                PickUpItem(_item);
            }
        }
        #endregion
    }


    void MachineActionReset()
    {
        //Reset
        _workCounterTime = 0;

        HUD.OnMachineInteractionEnter(false);

        _isRepairing = false;//UnLock the playerMovement
    }

    #region Movement
    void MoveInput()
    {
        //movement
        _horizontalMove = Input.GetAxisRaw("Horizontal") * _runSpeed;

    }

    void getUpdatedAnimaationsParametres()
    {
        _animator.SetFloat("Speed" , Mathf.Abs(_horizontalMove));
        _animator.SetBool("Repair" , _isRepairing);
        // _animator.SetBool("IsJumping" , true);
    }
    public void getUpdatedHUDRotation()
    {
        HUD.FlipHUD();
    }

    #endregion

    #region ItemMethods
    void PickUpItem(Item newItem)
    {
        //Debug.Log("Picking Item");

        if(_holdingItem != null)
            DropItem();

        //then
        _holdingItem = newItem;
        _itemBorder.color = _holdingItem.getColor;

        //set ui
        _itemBorder.transform.GetChild(0).GetComponent<Image>().gameObject.SetActive(true);
        _itemBorder.transform.GetChild(0).GetComponent<Image>().sprite = _holdingItem.sprite;


        //on pick up hide the item and add the payer as parent to use later on drop fuction
        newItem.HideItem();
        newItem.transform.SetParent(transform);
    }

    void DropItem()
    {
        if(_holdingItem == null)
            return;
        //else
        _holdingItem.transform.position = transform.position;
        _holdingItem.transform.parent = null;
        _holdingItem.gameObject.SetActive(true);

        _holdingItem = null;
        //reset UI 
        ResetItemUI();
    }

    void UseItem()
    {
        if(_holdingItem == null)
            return;

        Destroy(_holdingItem);

        _holdingItem = null;

        ResetItemUI();
    }

    void ResetItemUI()
    {

        _itemBorder.color = Color.white;
        _itemBorder.transform.GetChild(0).GetComponent<Image>().gameObject.SetActive(false);



    }
    #endregion

    #region Collitions
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if(collision.gameObject.GetComponent<InteractableObject>()) //Activate the UI border, exclluding machines
        {
            

            //if it is an interactable item activate the interactionUI
            InteractableObject interactableObject = collision.gameObject.GetComponent<InteractableObject>();
            interactableObject.ActivateHUD(true);

            if(interactableObject.gameObject.GetComponent<Machine>())    //MACHINE
            {
                _isNearMachine = true;
                _machine = interactableObject.GetComponent<Machine>();

                if(_holdingItem &&!_isRepairing)
                    HUD.OnInteractableCollitionEnter("Hold 'E' to repair...");
                else //if he is repairing hide it 
                    HUD.OnInteractableCollitionExit();

            }

            if(interactableObject.gameObject.GetComponent<Item>())     //Item
            {
                _isNearItem = true;
                _item = interactableObject.GetComponent<Item>();

                HUD.OnInteractableCollitionEnter("Press 'E' to pick item...");

            }

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //player hud
        if(collision.gameObject.GetComponent<InteractableObject>()) // Player
            HUD.OnInteractableCollitionExit();


        if(collision.gameObject.GetComponent<Machine>())    //MACHINE
        {
            _isNearMachine = false;
            _machine = null;
        }

        if(collision.gameObject.GetComponent<Item>())     //Item
        {
            _isNearItem = false;
            _item = null;

            //hide item interaction ui
            collision.gameObject.GetComponent<InteractableObject>().ActivateHUD(false);
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        //Ladder Use
        if(collision.gameObject.GetComponent<Ladder>() && Input.GetKey(KeyCode.S))
            collision.gameObject.GetComponent<Ladder>().DisableColider();

    }

    #endregion


}
