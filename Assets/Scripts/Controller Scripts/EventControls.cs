using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CorruptedSmileStudio.Spawn;

public class EventControls : MonoBehaviour
{
    [SerializeField]
    static Spawner spawner;

    private static int enemiesKilled = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GetComponentInChildren<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void enemyKilled() {
        enemiesKilled++;
        switch (enemiesKilled) {
            case int n when n==spawner.totalWaves+1:
                spawner.unitLevel=UnitLevels.Medium;
                spawner.Reset();
                spawner.StartSpawn();
                break;
            case int n when n==(spawner.totalWaves+1)*2:
                spawner.unitLevel=UnitLevels.Hard;
                spawner.Reset();
                spawner.StartSpawn();
                break;
            case int n when n==(spawner.totalWaves+1)*3:
                spawner.unitLevel=UnitLevels.Boss;
                spawner.Reset();
                spawner.StartSpawn();
                break;
            case int n when n==(spawner.totalWaves+1)*4:
                GameControls.Victory();
                break;
        }
    }
}
