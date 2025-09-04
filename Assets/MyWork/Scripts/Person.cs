using UnityEngine;

public class Person
{
    public string name;
    public int age;
    public float height;
    public Health health;

    public Person(string name, int age = 0, float height = 1.6f)
    {
        health = new Health();
        this.name = name;
        this.age = age;
        this.height = height;
    }

    public void Introduce()
    {
        Debug.Log("Hello! My name is " + name + ".");
    }
}
