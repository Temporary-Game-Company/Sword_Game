using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    Vector3 initial_scale;

    void Start() {
        initial_scale = transform.localScale;
    }


    // Update is called once per frame
    public void update(int health, int max_health)
    {
        transform.localScale = new Vector3(initial_scale.x * health / max_health, initial_scale.y, initial_scale.z);
    }
}
