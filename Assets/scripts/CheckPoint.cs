using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CheckPoint : MonoBehaviour
{

    public Vector3 posOffset;
    public bool boxOnTop;
    public LayerMask Colliderable;
    
    private void Awake()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z) + posOffset;
    }

    //public void Check()
    //{
    //    Collider2D hit = Physics2D.OverlapBox(transform.position + posOffset, .5f * Vector3.one, 0f, Colliderable);
    //    if (hit .transform.CompareTag("Pushable"))
    //    {
    //        boxOnTop = true;
    //    }
    //    else
    //    {
    //        boxOnTop = false;
    //    }
    //}
    
}
