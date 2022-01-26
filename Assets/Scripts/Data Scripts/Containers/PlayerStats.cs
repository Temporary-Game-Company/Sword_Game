using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Containers/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public FloatReference hp;
    public FloatReference max_hp;
    public FloatReference xp;
    public FloatReference level;

    public void SetStats(PlayerStats newStats)
    {
        this.hp.Variable.SetValue(newStats.hp);
        this.max_hp.Variable.SetValue(newStats.max_hp);
        this.xp.Variable.SetValue(newStats.xp);
        this.level.Variable.SetValue(newStats.level);
    }
}
