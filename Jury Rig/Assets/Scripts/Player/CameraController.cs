using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]private float _speed;
    [SerializeField][Range(0.1f , 1f)]private float _smooth = 0.25f;
    [SerializeField] private Vector3 _offset;

    [SerializeField] private bool _findPlayer;
    private GameObject _target;

    private Vector3 _desiredPos;


    private void Awake()
    {
        _target = FindObjectOfType<PlayerMovement>().gameObject;
    }

    void LateUpdate()
    {
        _desiredPos = _target.transform.position + _offset ;

        Vector3 smoothPos 
            = Vector3.Lerp(transform.position , _desiredPos , _smooth * _speed * Time.fixedDeltaTime);

        transform.position = smoothPos ;
        
    }
}
