using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TemporaryGameCompany
{
    public class DamagePopup : MonoBehaviour
    {
        [SerializeField] private TextMeshPro textMesh; // The text script of the popup.
        private float dissapearTime; // The time it takes before the popup begins disappearing.
        private float dissapearSpeed; // The speed at which the popup actually disappears.
        private Color textColor; // The color object of the text.
        private Vector3 randomDirection; // The direction in which the text floats away.

        // Called every frame.
        void Update() {
            transform.position += randomDirection * Time.deltaTime; // Moves the popup in the random direction.

            // Waits on disappear timer and then begins disappearing at the given disappear speed.
            dissapearTime -= Time.deltaTime;
            if (dissapearTime < 0f) {
                textColor.a -= dissapearSpeed * Time.deltaTime; // Decrease color alpha
                textMesh.color = textColor;
                if (textColor.a <= 0f) { // Destroys popup when alpha falls to 0.
                    Destroy(gameObject);
                }
            }
        }

        public void Setup(int damageTaken, float moveSpeed=1f, float dissapearTime=1f, float dissapearSpeed=2f, float scale=1 , Color? color=null) {
            // Sets text of the textmesh.
            textMesh.SetText(damageTaken.ToString());

            // Sets textmesh properties.
            if (!(color==null)) textMesh.color = (Color) color;
            this.transform.localScale *= scale;

            // Rotate towards camera.
            this.transform.rotation = Camera.main.transform.rotation;

            // Sets disappear timer and speed.
            this.dissapearTime = dissapearTime;
            this.dissapearSpeed = dissapearSpeed;

            // Sets local color variable
            textColor = textMesh.color;

            // Sets a random direction for the popup to go in.
            randomDirection = (Vector3) Random.insideUnitCircle.normalized * moveSpeed;
        }
    }
}