using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetWithoutCameraRotation : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _distance = 10;
    [SerializeField] private float _height = 5;
    [SerializeField] private float _heightAmortization = 2;

    private Vector3 _cameraPosition;

    //Place this script to the Camera Control group
    [AddComponentMenu("Camera-Control/FollowTarget")]

    void FixedUpdate()
    {
        if(!_target)
        {
            return;
        }

        CalculateCameraPosition(currentHeight: CalculateHeight());

        transform.position = _cameraPosition;
        transform.LookAt(_target);
    }

    private float CalculateHeight()
    {
        float wantedHeight = _target.position.y + _height;
        float currentHeight = transform.position.y;
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, _heightAmortization * Time.deltaTime);
        return currentHeight;
    }

    private void CalculateCameraPosition(float currentHeight)
    {
        _cameraPosition = _target.position;
        _cameraPosition -= Vector3.forward * _distance;

        _cameraPosition.y = currentHeight;
    }
}
