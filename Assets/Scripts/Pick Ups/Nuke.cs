using UnityEngine;
using System.Collections.Generic;

public class Nuke : PowerUp
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void ActivatePowerUp()
    {
        base.ActivatePowerUp();
        List<Enemy> targets = new List<Enemy>(Enemy.allSpawnedEnemies);
        foreach (Enemy target in targets)
        {
            if (target == null) continue;
            target.health.Kill();
        }
    }
}
