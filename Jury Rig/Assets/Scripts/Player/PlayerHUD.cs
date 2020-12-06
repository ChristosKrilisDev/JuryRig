using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{

    [Header("PlayerUi")]
    public GameObject interactionUI;
    Image _progBar;
    Image _interactionImg;


    private void Awake()
    {
        _progBar = interactionUI.transform.GetChild(0).GetComponent<Image>();
        _interactionImg = interactionUI.transform.GetChild(1).GetComponent<Image>();
    }


    public void ActivateHUD(bool activity)
    {
        interactionUI.gameObject.SetActive(activity);
    }


    public void OnMachineInteraction(bool value)
    {
        _progBar.gameObject.SetActive(value);

    }

    public void UpdateWorkSlider(float value)
    {
        _progBar.fillAmount = value;
    }

    public void OnItemInteraction(bool value)
    {
        _interactionImg.gameObject.SetActive(value);
    }

}
