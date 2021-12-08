using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSlime : Enemy
{
    [SerializeField]
    private float attack_delay;
    [SerializeField]
    private float attack_duration;
    [SerializeField]
    private float attack_distance;
    public bool attack_cooldown = false; // Attack cooldown.
    [SerializeField]
    private float attack_cooldown_duration;
    [SerializeField]
    private float pounce_force;
    [SerializeField]
    private float damage;

    void Update() {        
        if (!(is_attacking || is_hit)){
            if (Vector2.Distance(transform.position, target.position) < attack_distance && !attack_cooldown) {
                StartCoroutine("attack", target);
            } 
            moveTowardsPlayer();
            
        }
    }

    public override IEnumerator attack(Transform target) {
        // Set busy, cooldown, and direction towards target.
        attack_cooldown = true;
        Vector2 direction = ((Vector2)target.position - rigidBody.position).normalized;

        // Delay before attack.
        yield return new WaitForSeconds(attack_delay);
        is_attacking = true;

        // Attack
        rigidBody.velocity += direction * pounce_force;

        // Delay and disable busy state.
        yield return new WaitForSeconds(attack_duration);
        is_attacking = false;

        // Delay and disable attack cooldown.
        yield return new WaitForSeconds(attack_cooldown_duration);
        attack_cooldown = false;
    }
    public override void hitPlayer() {
        // Set busy, cooldown, and direction towards target.
        PlayerControls.damagePlayer((int)damage);
        is_attacking=false;

    }
}
