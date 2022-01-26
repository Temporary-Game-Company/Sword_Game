using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CorruptedSmileStudio.Spawn;

public class EventControls : MonoBehaviour
{

    private static int enemiesKilled = 0;

    // Increases number of enemies killed and tracks waves.
    public static void enemyKilled() {
        Spawner spawner = GameAssets.assets.spawner;

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
