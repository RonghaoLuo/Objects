using UnityEngine;

public class ClassTester : MonoBehaviour
{
    Animal[] myFarm;
    GameObject powerUp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IPickable p = powerUp.GetComponent<IPickable>();
        p.PickUp();

        myFarm[0] = new Bird();
        myFarm[1] = new Mammal();
        myFarm[2] = new Fish();

        foreach (Animal animal in myFarm)
        {
            if (animal is IFly)
            {
                
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
