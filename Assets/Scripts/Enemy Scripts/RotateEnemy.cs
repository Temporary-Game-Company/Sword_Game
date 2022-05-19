using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TemporaryGameCompany
{
    [ExecuteAlways]
    public class RotateEnemy : MonoBehaviour
    {
        [SerializeField] private FloatReference _rotationDegrees;

        // Start is called before the first frame update
        void Start()
        {
            transform.position = new Vector3(transform.position.x,transform.position.y,-0.75f);
            transform.rotation = Quaternion.Euler(_rotationDegrees,0,0);
        }
    }
}