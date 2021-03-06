using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TemporaryGameCompany
{
    abstract public class Enemy : MonoBehaviour
    {
        protected state current_state = state.Ready;
        protected enum state {Ready, Attacking, Is_Hit};

        [SerializeField] protected int health = 40;
        private int max_health;
        [SerializeField] private int xp_worth = 30;
        [SerializeField] protected SwordRuntimeContainer _swordContainer;

        [SerializeField] private float collision_delay = 1f; // How long between collisions with player.

        [SerializeField] protected Rigidbody rigidBody; // Enemy rigid body.
        [SerializeField] Collider e_collider; // Enemy collider.
        [SerializeField] Transform damagePopup; // Enemy collider.
        [SerializeField] GameEvent killed_event;

        public Transform target; // Transform object of target (the player).

        abstract protected IEnumerator Attack(Transform target); // Function to attempt attacking.
        abstract protected void HitPlayer(); // Function to attempt attacking.
        abstract protected void GetHit(Vector3 velocity); // Function to attempt attacking.

        void Start() 
        {
            max_health = health;
        }

        // Collisions
        IEnumerator OnTriggerEnter(Collider collider) 
        {
            // Collision with player.
            if(collider.tag == "Player") {
                Vector3 incomingVelocity = collider.attachedRigidbody.velocity;

                if (current_state == state.Attacking) 
                    HitPlayer();

                if (incomingVelocity.magnitude > 3f) {
                    // Collision cooldown on
                    Physics.IgnoreCollision(collider, e_collider, true);
                    current_state = state.Is_Hit;

                    // Get Hit.
                    GetHit(incomingVelocity);

                    // Wait for cooldown
                    yield return new WaitForSeconds (collision_delay);

                    // Collision cooldown off
                    Physics.IgnoreCollision(collider, e_collider, false);
                    if (current_state == state.Is_Hit) current_state = state.Ready;
                }
            }
        }

        public void TakeDamage(int damage) 
        {
            // Instantiates a damage popup.
            DamagePopup damagePopupScript = Instantiate(damagePopup, transform.position, Quaternion.identity).GetComponent<DamagePopup>();

            // Checks for crit and creates appropriate damage popup.
            int crit = Random.Range(0, 10);
            if (crit==9) 
                damagePopupScript.Setup(damage*2, scale:1.5f, color:new Color(255/255.0f, 23/255.0f, 25/255.0f, 255/255.0f));
            else 
                damagePopupScript.Setup(damage);

            // Applies damage and checks for death.
            health -= damage;
            if(health <= 0) {
                Die();
            }
        }

        public void Die() 
        {
            _swordContainer.Get().GainXP(xp_worth);
            killed_event.Raise();
            Destroy(gameObject);
        }
    }
}