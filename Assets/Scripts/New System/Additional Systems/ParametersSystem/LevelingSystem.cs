using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingSystem : MonoBehaviour
{
    [SerializeField] private int _level;
    [SerializeField] private long _experience;
    [SerializeField] private long _experienceToLevel;
    [SerializeField] private GameObject _levelingEffect;

    private int _effectDuration = 2;

    // Use this for initialization
    void Start()
    {
        _levelingEffect.SetActive(false);
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
        _experience -= _experienceToLevel;
        _level++;
        _experienceToLevel = RecountExperienceToLevel();
        EnableLevelingEffect();
    }

    private long RecountExperienceToLevel()
    {
        return (long)Mathf.Pow(_level, 2) + (_level * 100);
    }

    private void EnableLevelingEffect()
    {
        _levelingEffect.SetActive(true);
        StartCoroutine(TurnOffLevelingEffect());
    }

    private IEnumerator TurnOffLevelingEffect()
    {
        yield return new WaitForSeconds(_effectDuration);
        _levelingEffect.SetActive(false);
    }
}
