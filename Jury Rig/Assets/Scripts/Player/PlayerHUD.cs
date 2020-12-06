using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{

    [Header("PlayerUi")]


    public GameObject Hud;
    //Image _interactionImg;
    Transform _progrBarParent;
    Image _progBar;


    [SerializeField] private GameObject _interactionPanel; // Main UI
    Text _interactionText;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        //HUD/SLIDERHOLDER/SLIDER
        _progrBarParent = Hud.transform.GetChild(0).GetComponent<Transform>();
        _progBar = _progrBarParent.transform.GetChild(0).GetComponent<Image>();

        //_interactionImg = interactionUI.transform.GetChild(1).GetComponent<Image>();

        _interactionText = _interactionPanel.transform.GetChild(0).GetComponent<Text>();


        _progrBarParent.gameObject.SetActive(false);
        _interactionPanel.SetActive(false);
    }




    public void FlipHUD()
    {
        Vector3 theScale = Hud.transform.localScale;
        theScale.x *= -1;
        Hud.transform.localScale = theScale;
    }

    public void UpdateWorkSlider(float value) => _progBar.fillAmount = value;

    //In player game hud
    public void OnMachineInteractionEnter(bool activity)
    {
        _progrBarParent.gameObject.SetActive(activity);
    }


    //in main ui
    public void OnInteractableCollitionEnter(string info)
    {
        _interactionPanel.gameObject.SetActive(true);
        _interactionText.text = info;
    }

    public void OnInteractableCollitionExit()
    {
        _interactionText.text = null;
        _interactionPanel.gameObject.SetActive(false);
    }


}
