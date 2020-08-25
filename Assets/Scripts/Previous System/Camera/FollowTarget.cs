using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _distance = 10;
    [SerializeField] private float _height = 5;
    [SerializeField] private float _heightAmortization = 2;
    [SerializeField] private float _rotationAmortization = 3;

    private Vector3 _cameraPosition;
    
    //Place this script to the Camera Control group
    [AddComponentMenu("Camera-Control/FollowTarget")]

    void LateUpdate()
    {
        Quaternion currentRotation = CalculateRotation();
        float currentHeight = CalculateHeight();
        CalculateCameraPosition(currentRotation, currentHeight);

        transform.position = _cameraPosition;
        transform.LookAt(_target);
    }

    private Quaternion CalculateRotation()
    {
        float wantedRotationAngle = _target.eulerAngles.y;
        float currentRotationAngle = transform.eulerAngles.y;
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, _rotationAmortization * Time.deltaTime);
        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
        return currentRotation;
    }

    private float CalculateHeight()
    {
        float wantedHeight = _target.position.y + _height;
        float currentHeight = transform.position.y;
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, _heightAmortization * Time.deltaTime);
        return currentHeight;
    }

    private void CalculateCameraPosition(Quaternion currentRotation, float currentHeight)
    {
        _cameraPosition = _target.position;
        _cameraPosition -= currentRotation * Vector3.forward * _distance;

        _cameraPosition.y = currentHeight;
    }
}
