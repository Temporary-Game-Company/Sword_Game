using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TemporaryGameCompany
{
    public class RandomSpawn : MonoBehaviour
    {
        [SerializeField]
        SpriteRenderer spriteRenderer; // Sprite Renderer

        [SerializeField]
        int spawn_distance;

        // Start is called before the first frame update
        void Start()
        {
            // Teleports enemy in a random direction.
            transform.position = (Vector2)transform.position + Random.insideUnitCircle.normalized * spawn_distance;

            // Makes the enemy visible after teleporting.
            spriteRenderer.enabled = true;
        }
    }
}