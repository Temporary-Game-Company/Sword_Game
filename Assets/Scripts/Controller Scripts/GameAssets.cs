using System.Reflection;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets assets = null;

    void Awake() {
        if (assets==null) {
            assets = this;
        }
    }

    public Spawner spawner;
}
