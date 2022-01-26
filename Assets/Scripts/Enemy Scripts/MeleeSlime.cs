using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMovementAI;

public class MeleeSlime : Enemy
{
    [SerializeField] private float attack_delay;
    [SerializeField] private float attack_duration;
    [SerializeField] private float attack_distance;
    public bool attack_cooldown = false; // Attack cooldown.
    [SerializeField] private float attack_cooldown_duration;
    [SerializeField] private float pounce_force;
    [SerializeField] private float damage;
    [SerializeField] SteeringBasics steeringBasics; // Basic Steering Script.

    void Update() {
        // Checks if the slime is busy
        if (current_state == state.Ready){
            // Starts attacking if not busy and near player.
            if (Vector2.Distance(transform.position, target.position) < attack_distance && !attack_cooldown) {
                StartCoroutine("Attack", target);
            } 
            // Moves towards the player otherwise
            MoveTowardsPlayer();
            
        }
    }

    protected override IEnumerator Attack(Transform target) {
        // Set busy, cooldown, and direction towards target.
        attack_cooldown = true;
        Vector2 direction = ((Vector2)target.position - rigidBody.position).normalized;

        // Delay before attack.
        yield return new WaitForSeconds(attack_delay);
        current_state = state.Attacking;

        // Attack
        rigidBody.velocity += direction * pounce_force;

        // Delay and disable attacking state.
        yield return new WaitForSeconds(attack_duration);
        if (current_state == state.Attacking) current_state = state.Ready;

        // Delay and disable attack cooldown.
        yield return new WaitForSeconds(attack_cooldown_duration);
        attack_cooldown = false;
    }

    protected override void HitPlayer() {
        // Set busy, cooldown, and direction towards target.
        player_stats.ApplyDamage(damage);
        if (current_state == state.Attacking) current_state = state.Ready;

    }

    protected override void GetHit(Vector2 velocity) {
        // Bounce Back.
        rigidBody.velocity = velocity;

        // Take Damage.
        TakeDamage((int) velocity.magnitude);
    }

    private void MoveTowardsPlayer() {
        // Gets direction towards player.
        Vector3 accel = steeringBasics.Seek(target.position);

        // Moves towards player.
        steeringBasics.Steer(accel);
    }
}
