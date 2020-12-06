using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    public Transform target;
    public float hideDistance;

    private bool m_FacingRight = true;

    void Update()
    {
        var dir = target.position - transform.position;

        if(dir.magnitude < hideDistance)
            SetChildrenActive(false);
        else
        {
            var angle = Mathf.Atan2(dir.y , dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle , Vector3.forward);
        }

    }

    void SetChildrenActive(bool value)
    {
        foreach(Transform child in transform)
            child.gameObject.SetActive(value);
    }

    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
