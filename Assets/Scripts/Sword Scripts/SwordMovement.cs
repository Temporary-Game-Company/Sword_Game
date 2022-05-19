using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TemporaryGameCompany
{
    public class SwordMovement : MonoBehaviour
    {
        Rigidbody2D rigidBody;
        [SerializeField] private FloatReference idle_distance;
        [SerializeField] private FloatReference original_rotate_speed;
        private float rotate_speed;

        [SerializeField] private FloatReference acceleration;
        [SerializeField] private FloatReference deceleration;
        [SerializeField] private FloatReference max_move_speed;

        private int angle_tune = 270;
        private float move_speed = 0f;

        private bool wall_trigger = false;

        Plane plane = new Plane(Vector3.back, Vector3.zero);
        float distance;
        Vector2 direction;

        // Start is called before the first frame update
        void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            rotate_speed = original_rotate_speed;
        }

        // Update is called once per frame
        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance)) {
                direction = ray.GetPoint(distance) - transform.position;
            }
            // Vector2 direction = Camera.main.ScreenPointToRay(Input.mousePosition) - transform.position;
            if (direction.magnitude >= idle_distance){ // Mouse outside idle
                // Rotate towards mouse.
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + angle_tune;
                Quaternion rotation = Quaternion.AngleAxis(angle,Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotate_speed * Time.deltaTime);

                // Increase move speed.
                move_speed += acceleration;
                if (move_speed > max_move_speed.Value) move_speed = max_move_speed.Value;
            } 
            else { // Mouse inside idle
                // Decrease move speed.
                move_speed -= deceleration;
                if (move_speed < 0f) move_speed = 0f;
            }
            // Update velocity
            rigidBody.velocity = transform.up * move_speed;
        }
        
        IEnumerator OnTriggerEnter2D(Collider2D collider) {
            // Collision with wall.
            if(collider.tag == "Wall" && !wall_trigger) {
                // Turn player to face opposite way.
                wall_trigger = true;
                transform.RotateAround(transform.position, Vector3.up, 180f);
                transform.RotateAround(transform.position, Vector3.right, 180f);
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