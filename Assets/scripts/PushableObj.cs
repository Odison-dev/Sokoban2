using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PushableObj : MonoBehaviour
{
    public Vector3 posOffset = Vector3.zero;

    private void Awake()
    {
        transform.position = new Vector3 (Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
    }

    private void Update()
    {
    }

    
}
