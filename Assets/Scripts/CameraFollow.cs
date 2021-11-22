using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    Vector3 desiredPosition;
    public Transform target;

    private void OnEnable()
    {
        desiredPosition = transform.position - target.position;
    }

    private void LateUpdate()
    {
        if(Vector3.Distance(transform.position, target.position) >= 11)
        {
            transform.DOKill();
            transform.DOMove(target.position + desiredPosition, 0.5f);
        }    
    }
}
