using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private Vector3 offcet, posEnd, Smooth;

    private void Start()
    {
        offcet = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        posEnd = target.transform.position + offcet;
        Smooth = Vector3.Lerp(transform.position, posEnd, 0.125f);
        transform.position = Smooth;
    }
}
