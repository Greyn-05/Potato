using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    #region Field
    [SerializeField] private float healthChangeDelay = .5f;

    private CharacterStatusHandler _statusHandler;
    private float _healthLastChange = float.MaxValue;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => _statusHandler.CurrentStatus.maxHealth;

    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    #endregion

    #region Init
    private void Awake()
    {
        _statusHandler = GetComponent<CharacterStatusHandler>();
    }
    private void Start()
    {
        CurrentHealth = _statusHandler.CurrentStatus.maxHealth;
    }
    #endregion

    #region ChangeHealthEvent
    public bool ChangeHealth(float change)
    {
        if (change == 0 || _healthLastChange < healthChangeDelay) return false;


        _healthLastChange = 0f;
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        if(change < 0) OnDamage?.Invoke();
        else OnHeal?.Invoke();

        if(CurrentHealth <= 0f)
        {
            OnDeath?.Invoke();
        }
        return true;
    }
    #endregion
}
