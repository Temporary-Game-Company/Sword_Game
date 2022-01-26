using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordControl : MonoBehaviour
{
    [SerializeField] PlayerStats stats;
    [SerializeField] PlayerStats original_stats;

    private float xp_threshhold;

    void Start() {
        Reset_Stats();
    }

    void Reset_Stats() {
        xp_threshhold = (int) (stats.level * Mathf.Log(stats.level) * 100 + 100); // Set initial xp threshhold.
        stats.SetStats(original_stats);
    }

    void Update() {
        // Level up if xp over threshhold.
        if (stats.xp > xp_threshhold) {
            LevelUp();
        }

        // Game Over if hp falls to 0.
        if (stats.hp <= 0) GameControls.GameOver();
    }

    public void LevelUp() {
        stats.level.Variable.ApplyChange(1f); // Increase level by 1.
        xp_threshhold = (int) (stats.level * Mathf.Log(stats.level) * 100 + 100); // Calculate new xp threshhold.
        stats.xp.Variable.SetValue(0f); // Resets xp to 0.
    }
}
