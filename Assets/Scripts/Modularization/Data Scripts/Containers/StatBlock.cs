using System.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TemporaryGameCompany
{

    [Serializable]
    public class StatBlock : MonoBehaviour
    {
        [SerializeField] private List<StatReference> Stats;

        // Returns a requested stat reference from the stat block.
        public StatReference GetStat(Stat requestedStat)
        {
            foreach (StatReference stat in Stats)
            {
                if (stat.stat == requestedStat)
                    return stat;
            }
            return null;
        }

        public void Initialize()
        {
            foreach (StatReference stat in Stats)
            {
                stat.Initialize();
            }
        }
    }
}