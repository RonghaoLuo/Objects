using UnityEngine;

public abstract class Animal
{
    protected int health;
    protected float strength;

    public abstract void MakeNoise();

    public abstract void Move();

    public virtual void GiveBirth()
    {

    }
}

public class Bird : Animal, IFly, ILayEgg
{
    public void FlapWings()
    {
        
    }

    public void LayEgg()
    {
        throw new System.NotImplementedException();
    }

    public override void MakeNoise()
    {
        Debug.Log("Chirp Chirp");
    }

    public override void Move()
    {
        FlapWings();
    }

    
}

public class Mammal : Animal
{
    public override void MakeNoise()
    {
        Debug.Log("Moo Moo");
    }

    public override void Move()
    {
        throw new System.NotImplementedException();
    }
}

public class Fish : Animal
{
    public override void MakeNoise()
    {
        Debug.Log("Blub Blub");
    }

    public override void Move()
    {
        throw new System.NotImplementedException();
    }
}

public class Eagle : Bird
{
    public void Attack()
    {

    }
}

public class Dog : Mammal
{
    public override void MakeNoise()
    {
        Debug.Log("Woof Woof");
    }
}

public class Penguin : Bird, ILayEgg
{

}

public class Platypus : Mammal, ILayEgg
{
    public void LayEgg()
    {
        throw new System.NotImplementedException();
    }
}

public class Bat : Mammal, IFly
{
    public void FlapWings()
    {
        throw new System.NotImplementedException();
    }
}