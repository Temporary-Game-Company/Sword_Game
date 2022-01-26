using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Containers/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public FloatReference hp;
    public FloatReference max_hp;
    public FloatReference xp;
    public FloatReference level;

    private float xp_threshhold;

    public void SetStats(PlayerStats newStats)
    {
        this.hp.Value = newStats.hp;
        this.max_hp.Value = newStats.max_hp;
        this.xp.Value = newStats.xp;
        this.level.Value = newStats.level;

        this.xp_threshhold = ExperienceThreshhold();
    }

    public void LevelUp() 
    {
        this.level.Value += 1;
        this.xp.Value -= this.xp_threshhold;

        this.xp_threshhold = ExperienceThreshhold();
    }

    public void ApplyDamage(float amount)
    {
        this.hp.Value -= amount;
        if (this.hp.Value <= 0)
        {
            // **FIX** Add Game Over Unity Event
        }
    }

    public void GainXP(float amount)
    {
        this.xp.Value += amount;
        if (this.xp.Value > this.xp_threshhold)
        {
            LevelUp();
        }
    }

    public float ExperienceThreshhold()
    {
        return (this.level.Value * Mathf.Log(this.level.Value) * 100 + 100);
    }
}
