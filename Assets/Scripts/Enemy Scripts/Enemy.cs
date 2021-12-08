using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMovementAI;

abstract public class Enemy : MonoBehaviour
{
    [SerializeField]
    public int health = 40;

    [SerializeField]
    private float collision_delay = 1f; // How long between collisions with player.

    protected bool is_hit = false; // Collision with player on cooldown.
    protected bool is_attacking = false; // Is currently in a state of attacking.


    [HideInInspector]
    public Rigidbody2D rigidBody; // Enemy rigid body.
    Collider2D e_collider; // Enemy collider.

    SteeringBasics steeringBasics; // Basic Steering Script.

    protected Transform target; // Transform object of target.

    abstract public IEnumerator attack(Transform target); // Function to attempt attacking.
    abstract public void hitPlayer(); // Function to attempt attacking.

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        e_collider = GetComponent<Collider2D>();
        steeringBasics = GetComponent<SteeringBasics>();
        target = GameObject.FindGameObjectWithTag("Player").transform;        
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_hit){
            moveTowardsPlayer();
        }
    }

    void FixedUpdate() 
    {

    }

    // Collisions
    IEnumerator OnTriggerStay2D(Collider2D collider) {
        // Collision with player.
        if(collider.tag == "Player") {
            Vector2 velocity = collider.attachedRigidbody.velocity;

            if (is_attacking) hitPlayer();

            if (!is_hit && velocity.magnitude > 3f) {
                // Get Hit.
                getHit(velocity);

                // Collision cooldown on
                Physics2D.IgnoreCollision(collider, e_collider, true);
                is_hit = true;

                // Wait for cooldown
                yield return new WaitForSeconds (collision_delay);

                // Collision cooldown off
                Physics2D.IgnoreCollision(collider, e_collider, false);
                is_hit = false;
            }
        }
    }

    void getHit(Vector2 velocity) {
        // Bounce Back.
        rigidBody.velocity = velocity;

        // Take Damage.
        takeDamage((int) velocity.magnitude);
    }

    protected void moveTowardsPlayer() {
        Vector3 accel = steeringBasics.Seek(target.position);

        steeringBasics.Steer(accel);
    }

    public void takeDamage(int damage) {
        health -= damage;

        if(health <= 0) {
            Destroy(gameObject);
            EventControls.enemyKilled();
        }
    }
}
