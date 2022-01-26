using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordControl : MonoBehaviour
{
    [SerializeField] PlayerStats stats;
    [SerializeField] PlayerStats original_stats;
    [SerializeField] private GameEvent gameover_event;

    void Start() {
        Reset_Stats();
    }

    void Reset_Stats() {
        stats.SetStats(original_stats);
    }

    void Update() {
        // Game Over if hp falls to 0.
        if (stats.hp <= 0) gameover_event.Raise();
    }
}
