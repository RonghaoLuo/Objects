using UnityEngine;

public class ClassTester : MonoBehaviour
{
    Health referenceToHealth;
    Person subject1;
    Calculator myCalculator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(FindObjectsByType<Character>(FindObjectsSortMode.None).Length.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
