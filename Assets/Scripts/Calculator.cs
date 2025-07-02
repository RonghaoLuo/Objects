using UnityEngine;

public class Calculator
{
    public float Sum(float a, float b)
    {
        return a + b;
    }

    public int Sum(int a, int b) 
    { 
        return a + b; 
    }

    public float Difference(float a, float b)
    {
        return a - b;
    }

    public float Product(float a, float b)
    {
        return a * b;
    }

    public float Ratio(float a, float b)
    {
        return a / b;
    }

    public float Square(float a)
    {
        return (a * a);
    }

    //public float Power(float baseNum, float exponent)
    //{
    //    return new float();
    //}
}
