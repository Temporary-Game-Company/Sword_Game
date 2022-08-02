using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TemporaryGameCompany
{
    public class SwordMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private FloatReference idle_distance;
        [SerializeField] private FloatReference original_rotate_speed;
        private float rotate_speed;

        [SerializeField] private FloatReference acceleration;
        [SerializeField] private FloatReference deceleration;
        [SerializeField] private FloatReference max_move_speed;

        private Vector3 _floatingConstant = new Vector3(0,1.8f,0);

        private float move_speed = 0f;

        private bool wall_trigger = false;

        Plane plane = new Plane(Vector3.back, Vector3.zero);
        private UnityEngine.RaycastHit _collHit;
        
        private Vector3 _distance;
        private Vector3 _direction;

        // Start is called before the first frame update
        void Start()
        {
            rotate_speed = original_rotate_speed;
        }

        // Update is called once per frame
        void Update()
        {
            bool move = false;

            // Cast ray, hitting all colliders.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            // Calculate nearest navmesh point near ray.
            if (Physics.Raycast(ray, out _collHit) && NavMesh.SamplePosition(_collHit.point, out NavMeshHit navHit, 100, NavMesh.AllAreas))
            {
                _distance = navHit.position - transform.position; // Direction to move in.

                // Check if destination outside idle distance.
                if (_distance.magnitude >= idle_distance)
                {
                    move = true;

                    _direction = _distance + _floatingConstant;

                    // Slerp rotation around y-axis.
                    if (Mathf.Abs(_direction.y) <= 0.1)
                    {
                        float angle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;

                        Quaternion rotation = Quaternion.AngleAxis(angle,new Vector3(0,1,0));
                        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotate_speed * Time.deltaTime);
                    }
                    // Look towards new direction.
                    else
                    {
                        Vector3 newDirection = Vector3.RotateTowards(transform.forward, _direction, rotate_speed * Time.deltaTime, 0.0f);

                        transform.rotation = Quaternion.LookRotation(newDirection, Vector3.up);
                    }

                    // Increase move speed.
                    move_speed += acceleration;
                    if (move_speed > max_move_speed.Value) move_speed = max_move_speed.Value;
                }
            } 
            
            // Decelerate if no valid movement.
            if (!move) 
            { 
                move_speed -= deceleration;
                if (move_speed < 0f) move_speed = 0f;
            }

            // Update velocity
            rigidBody.velocity = transform.forward * move_speed;
        }
        
        IEnumerator OnTriggerEnter(Collider collider) {
            // Collision with wall.
            if(collider.tag == "Wall" && !wall_trigger) {
                // Turn player to face opposite way.
                wall_trigger = true;
                transform.RotateAround(transform.position, Vector3.up, 180f);
                // transform.RotateAround(transform.position, Vector3.right, 180f);
                yield return rampUpRotate();
            }
        }

        // Ramps player rotation speed from 0 back to original.
        IEnumerator rampUpRotate() {
            rotate_speed = 0f;
            yield return new WaitForSeconds (0.02f);
            wall_trigger = false;
            while (rotate_speed < original_rotate_speed) {
                rotate_speed += original_rotate_speed/15;
                yield return new WaitForSeconds (0.1f);
            }
            yield return null;
        }
    }
}