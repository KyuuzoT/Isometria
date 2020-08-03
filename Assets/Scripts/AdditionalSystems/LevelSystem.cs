using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] private long _experience;
    [SerializeField] private int _level;
    [SerializeField] private long _experienceToLevel;
    [SerializeField] private int _healthForLevelUp;
    [SerializeField] private int _damageForLevelUp;
    [SerializeField] private GameObject _graphicalEffect;

    // Use this for initialization
    void Start()
    {
        _graphicalEffect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(_experience >= _experienceToLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        _level++;
        _experience -= _experienceToLevel;
        _experienceToLevel = (long)Mathf.Pow(_level, 2) + 100;
        LevelUpEffect();
    }

    public void AddExperience(long exp)
    {
        _experience += exp;
    }

    private void LevelUpEffect()
    {
        transform.GetComponent<HealthSystem>().MaximalHealth += _healthForLevelUp;
        transform.GetComponent<Fight>().Damage = transform.GetComponent<Fight>().Damage + _damageForLevelUp;
        _graphicalEffect.SetActive(true);
        StartCoroutine(TurnOffLevelUpEffect());
    }

    private IEnumerator TurnOffLevelUpEffect()
    {
        yield return new WaitForSeconds(2);
        _graphicalEffect.SetActive(false);
    }

    private void TurnOnLights()
    {
    }
}
