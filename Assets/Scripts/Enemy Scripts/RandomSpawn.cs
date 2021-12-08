using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer; // Sprite Renderer

    [SerializeField]
    int spawn_distance;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = (Vector2)transform.position + Random.insideUnitCircle.normalized * spawn_distance;
        spriteRenderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
