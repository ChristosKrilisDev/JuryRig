using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    public GameObject interactionUI;

    public void ActivateHUD(bool activity)
    {
        interactionUI.gameObject.SetActive(activity);
    }
}
