using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWith;

    void Start()
    {
        startPos= transform.position;
        repeatWith = GetComponent<BoxCollider>().size.x /2;

    }

    void Update()
    {
        if(transform.position.x<startPos.x-repeatWith)
        {
            transform.position=startPos;
        }
    }
}
