using System;
using UnityEngine;

public class Health
{
    private int value;
    private int maxValue;
    public Action OnHealthZero;
    public Action OnHealthChange;

    public Health(int initialHealth = 100)
    {
        value = initialHealth;
        maxValue = value;
    }
    public Health(int initialHealth, int maxHealth)
    {
        value = initialHealth;
        maxValue = maxHealth;
    }

    public void DisplayHealth()
    {
        Debug.Log(value);
    }

    public void Damage(int toDamage)
    {
        value -= toDamage;

        if (value <= 0)
        {
            value = 0;
            OnHealthZero?.Invoke();     // question mark: won't invoke if no listener
        }
        OnHealthChange?.Invoke();
        DisplayHealth();
    }

    public void Heal(int toHeal)
    {
        value += toHeal;
        if (value > maxValue)
        {
            value = maxValue;
        }
        OnHealthChange?.Invoke();
    }

    public bool IsHealthLow()
    {
        if (value < 10)
        {
            return true;
        }

        return false;
    }

    public int GetHealth()
    {
        return value;
    }

    public void Kill()
    {
        Damage(GetHealth());
    }

    public float GetHealthInFraction()
    {
        return (float) GetHealth() / maxValue;
    }
}
