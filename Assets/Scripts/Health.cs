using UnityEngine;

public class Health
{
    private int _healthPoint;

    public Health(int initialHealth = 100)
    {
        _healthPoint = initialHealth;
    }

    public void DisplayHealth()
    {
        Debug.Log(_healthPoint);
    }

    public void Damage(int toDamage)
    {
        _healthPoint -= toDamage;
        DisplayHealth();
    }

    public void Heal(int toHeal)
    {
        _healthPoint += toHeal;
    }

    public bool IsHealthLow()
    {
        if (_healthPoint < 10)
        {
            return true;
        }

        return false;
    }

    public bool IsDead()
    {
        return _healthPoint <= 0;
    }
}
