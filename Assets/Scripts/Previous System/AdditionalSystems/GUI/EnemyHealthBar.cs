using Assets.Scripts.AdditionalSystems.GUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Texture2D _healthBarFrame;
    [SerializeField] private Rect _healthBarFramePosition;

    [SerializeField] private Texture2D _healthBarPoint;
    [SerializeField] private Rect _healthBarPointPosition;
    [SerializeField] [Range(0, 1)] private float _horizontalDistance;
    [SerializeField] [Range(0, 1)] private float _verticalDistance;
    [SerializeField] [Range(0, 1)] private float _pointWidth;
    [SerializeField] [Range(0, 1)] private float _pointHeight;

    private float _frameWidthPercentage;
    private float _frameHeightPercentage;

    private GameObject _player;
    private GameObject _opponent;
    private float _healthPercentage;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        _opponent = _player.GetComponent<Fight>().Opponent;
        if (_opponent != null)
        {
            _healthPercentage = _opponent.GetComponent<HealthSystemDeprecated>().GetCurrentHealth / _opponent.GetComponent<HealthSystemDeprecated>().MaximalHealth;
        }
        else
        {
            _healthPercentage = 0;
            _opponent = null;
        }
    }

    private void OnGUI()
    {
        if(!_opponent.Equals(null))
        {
            DrawHealthBarFrame();
            DrawHealthBar();
        }
    }

    private void DrawHealthBarFrame()
    {
        //Debug.Log($"Screen width: {Screen.width}\nScreen height: {Screen.height}");
        _healthBarFramePosition = _healthBarFramePosition.AdjustToScreenWidth();

        _frameWidthPercentage = _healthBarFramePosition.width / Screen.width;
        _healthBarFramePosition.width = Screen.width * _frameWidthPercentage;

        _frameHeightPercentage = _healthBarFramePosition.height / Screen.height;
        _healthBarFramePosition.height = Screen.height * _frameHeightPercentage;
        GUI.DrawTexture(_healthBarFramePosition, _healthBarFrame);
    }

    private void DrawHealthBar()
    {
        _healthBarFramePosition.CopyParameters(destination: ref _healthBarPointPosition);

        _healthBarPointPosition.x += _healthBarFramePosition.width * _horizontalDistance;
        _healthBarPointPosition.y += _healthBarFramePosition.height * _verticalDistance;

        _healthBarPointPosition.width *= _pointWidth * _healthPercentage;
        _healthBarPointPosition.height *= _pointHeight;

        GUI.DrawTexture(_healthBarPointPosition, _healthBarPoint);
    }
}