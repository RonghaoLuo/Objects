using UnityEngine;

public class Health
{
    private int value;

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
        DisplayHealth();
    }

    public void Heal(int toHeal)
    {
        value += toHeal;
        DisplayHealth();
    }

    public bool IsHealthLow()
    {
        if (value < 10)
        {
            return true;
        }

        return false;
    }
}
