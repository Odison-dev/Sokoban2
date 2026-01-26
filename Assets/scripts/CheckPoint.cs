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
    
}
