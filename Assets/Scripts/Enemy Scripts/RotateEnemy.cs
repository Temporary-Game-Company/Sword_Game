using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEnemy : MonoBehaviour
{
    Quaternion _rotation;
    void Awake()
    {
        _rotation = transform.rotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y,-0.75f);
        transform.rotation = Quaternion.Euler(-90,0,0);
    }
}
