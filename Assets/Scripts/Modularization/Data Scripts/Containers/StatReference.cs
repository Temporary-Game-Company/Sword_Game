using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TemporaryGameCompany
{
    public enum Stat
    {
        Health,
        Strength,
        Speed,
        Level,
        XP
    }
    
    [Serializable]
    public class StatReference
    {
        public Stat stat; // The associated stat.

        [SerializeField] private FloatReference _initialValue;
        [SerializeField] private GameEvent _belowZeroEvent;
        private float _value;

        // Getter for stat value.
        public float Value
        {
            get { return _value; }
            private set { _value = value; }
        }


        // Getter for stat initial value.
        public float InitialValue
        {
            get { return _initialValue.Value; }
        }

        // Sets a stat to its initial value.
        public void Initialize()
        {
            _value = _initialValue.Value;
        }

        // Used to increment a stat. False if result below 0.
        public bool Increase(float amount)
        {
            float result = _value + amount;

            if (result >= 0f) {
                _value = result;
                return true;
            }

            _value = 0f;
            belowZero();
            return false;
        }

        // Used to decrement a stat. False if result below 0.
        public bool Decrease(float amount)
        {
            float result = _value - amount;

            if (result >= 0f) {
                _value = result;
                return true;
            }
            
            _value = 0f;
            belowZero();
            return false;
        }

        // Raises event in case of a stat falling below zero.
        private void belowZero()
        {
            if (_belowZeroEvent != null)
                _belowZeroEvent.Raise();
        }

        // Changes by amount for duration.
        public IEnumerator tempChange(float amount, float duration)
        {
            float temp = _value;

            if (!Increase(amount))
                amount = -temp;

            yield return new WaitForSeconds(duration);

            Decrease(amount);
        }
    }
}
