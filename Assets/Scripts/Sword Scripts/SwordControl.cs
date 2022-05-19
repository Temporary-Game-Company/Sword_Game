using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TemporaryGameCompany
{
    public class SwordControl : StatBlock
    {
        [SerializeField] private SwordRuntimeContainer _container;

        public void Awake()
        {
            this.Initialize();
        }

        public void OnEnable()
        {
            _container.Add(this);
        }

        public void OnDisable()
        {
            _container.Remove(this);
        }

        public void LevelUp() 
        {
            this.GetStat(Stat.Level).Increase(1);
        }

        public void ApplyDamage(float amount)
        {
            this.GetStat(Stat.Health).Decrease(amount);
        }

        public void GainXP(float amount)
        {
            this.GetStat(Stat.XP).Increase(amount);
            if (this.GetStat(Stat.XP).Value > this.ExperienceThreshhold())
            {
                LevelUp();
            }
        }

        public float ExperienceThreshhold()
        {
            return (this.GetStat(Stat.Level).Value * Mathf.Log(this.GetStat(Stat.Level).Value) * 100 + 100);
        }
    }
}