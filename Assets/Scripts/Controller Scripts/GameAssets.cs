using System.Reflection;
using UnityEngine;

namespace TemporaryGameCompany
{
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
}