using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{

    public Collider2D _topHitBox;
    public float _speed = 6;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            _topHitBox.isTrigger = true;
            collision.GetComponent<Rigidbody2D>().gravityScale = 0f;

            if(Input.GetKey(KeyCode.W))
                collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0 , _speed);

            else if(Input.GetKey(KeyCode.S))
                collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0 , -_speed);

        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Rigidbody2D>().gravityScale = 2.5f;
            _topHitBox.isTrigger = false;
        }
    }


    public void DisableColider()
    {
        _topHitBox.isTrigger = true;
    }

}
