using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    public static int player_health = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void damagePlayer(int damage) {
        player_health -= damage;
        if (player_health <= 0) GameControls.GameOver();
        Debug.Log(player_health);
    }
}
