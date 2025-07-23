using System;
using UnityEngine;

public class Health
{
    private int value;
    public Action OnHealthZero;
    public Action<int> OnHealthChange;

    public Health(int initialHealth = 100)
    {
        value = initialHealth;
    }

    public void DisplayHealth()
    {
        Debug.Log(value);
    }

    public void Damage(int toDamage)
    {
        value -= toDamage;

        OnHealthChange?.Invoke(value);

        if (value <= 0)
        {
            OnHealthZero?.Invoke();     // question mark: won't invoke if no listener
        }
        DisplayHealth();
    }

    public void Heal(int toHeal)
    {
        value += toHeal;
        OnHealthChange?.Invoke(value);
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
}
