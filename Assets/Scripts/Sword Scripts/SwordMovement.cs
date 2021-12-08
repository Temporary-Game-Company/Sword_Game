using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMovement : MonoBehaviour
{
    Rigidbody2D rigidBody;
    public float idle_distance;
    public float original_rotate_speed;
    private float rotate_speed;

    public float acceleration;
    public float deceleration;
    public float max_move_speed;

    private int angle_tune = 90;
    private float move_speed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rotate_speed = original_rotate_speed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (direction.magnitude >= idle_distance){ // Mouse outside idle
            // Rotate towards mouse.
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + angle_tune;
            Quaternion rotation = Quaternion.AngleAxis(angle,Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotate_speed * Time.deltaTime);

            // Increase move speed.
            move_speed += acceleration;
            if (move_speed > max_move_speed) move_speed = max_move_speed;

            
        } 
        else { // Mouse inside idle
            // Decrease move speed.
            move_speed -= deceleration;
            if (move_speed < 0f) move_speed = 0f;
        }
        // Update velocity
        rigidBody.velocity = -(transform.up) * move_speed;
    }
    
    IEnumerator OnTriggerEnter2D(Collider2D collider) {
        // Collision with player.
        if(collider.tag == "Wall") {
            transform.RotateAround(transform.position, Vector3.up, 180f);
            transform.RotateAround(transform.position, Vector3.right, 180f);
            // transform.rotation = Quaternion.AngleAxis(180, -transform.up);
            yield return rampUpRotate();
        }
    }

    IEnumerator rampUpRotate() {
        rotate_speed = 0f;
        while (rotate_speed < original_rotate_speed) {
            rotate_speed += original_rotate_speed/15;
            yield return new WaitForSeconds (0.1f);
        }
        yield return null;
    }
}
