using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravBoots : MonoBehaviour
{
    bool gravMode = true;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) { Change(); }
    }
    void Change()
    {
        gravMode = !gravMode;

        if (gravMode)
        {
            GetComponentInParent<Rigidbody2D>().gravityScale = 1;
            GetComponentInParent<Player>().transform.localScale = new Vector3(5, 5, 1);
        }
        //
        else
        {
            GetComponentInParent<Rigidbody2D>().gravityScale = -1;
            GetComponentInParent<Player>().transform.localScale = new Vector3(5, -5, 1);
        }
    }
}
