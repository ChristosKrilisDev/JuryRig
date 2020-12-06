using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    [SerializeField]private Portal _linkedPortal;
    public Transform my_spawnPoint;

    private void Awake()
    {
        my_spawnPoint = transform.GetChild(0);
    }

    //for RNG use
    public void setNextDoor(Transform newPorta)
    {
        _linkedPortal.transform.position = newPorta.position;
    }
  
    public Transform MoveNextDoorPos()
    {
        return _linkedPortal.my_spawnPoint;
    }

}
