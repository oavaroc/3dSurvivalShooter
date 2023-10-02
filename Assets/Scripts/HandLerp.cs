using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLerp : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _speed;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position, _speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(_target.rotation.eulerAngles);
    }
}
