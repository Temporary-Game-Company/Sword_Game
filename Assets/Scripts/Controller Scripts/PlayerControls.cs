using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private static int health = 100;

    private static int level = 1;
    private static int xp = 0;
    private static int xp_threshhold = 100;

    public static void GainXP(int xpGain) {
        xp += xpGain;
        if (xp > xp_threshhold) LevelUp();
    }

    public static void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) GameControls.GameOver();
        Debug.Log(health);
    }

    public static void LevelUp() {
        level += 1;
        Debug.Log(level);

        xp_threshhold = (int) (level * Mathf.Log(level) * 100 + 100);
        xp = 0;
    }
}
