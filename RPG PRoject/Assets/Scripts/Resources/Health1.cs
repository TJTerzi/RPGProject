using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using System;
using GameDevTV.Utils;

namespace RPG.Resources
{
    public class Health1 : MonoBehaviour, ISaveable
    {

        [SerializeField] float regenerationPercentage = 70;

        LazyValue<float> healthPoints;


        bool isDead = false;

        private void Awake() 
        {
            healthPoints = new LazyValue<float>(GetInitalHealth);
        }

        private float GetInitalHealth()
        {
           return GetComponent<BaseStats>().GetStat(Stat.Health1);
        }

        private void Start() 
        {
            healthPoints.ForceInit();
        }

        private void OnEnable() 
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;

        }

        private void OnDisable() 
        {
            GetComponent<BaseStats>().onLevelUp -= RegenerateHealth;

        }

        public bool IsDead()
        {
            return isDead;
        }


        public void TakeDamage(GameObject instigator, float damage)
        {
            print(gameObject.name + " took damage: " + damage);
            
            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);
            if (healthPoints.value == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        public float GetHealthPoints()
        {
            return healthPoints.value;
        }

        public float GetMaxHealthPoints()
        {
           return GetComponent<BaseStats>().GetStat(Stat.Health1);
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints.value / GetComponent<BaseStats>().GetStat(Stat.Health1));

        }



        private void Die()
        {
            if (isDead) return;

            isDead = true;
            // This will trigger the Death() event.
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AwardExperience(GameObject instigator)
        {
           Experience experience = instigator.GetComponent<Experience>();
           if(experience == null) return;

           experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        private void RegenerateHealth()
        {
            healthPoints.value = GetComponent<BaseStats>().GetStat(Stat.Health1);
           // float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health1) * (regenerationPercentage / 100);
           // healthPoints = Mathf.Max(healthPoints, regenHealthPoints);
        }


        public object CaptureState()
        {
            return healthPoints.value;
        }

        public void RestoreState(object state)
        {
            healthPoints.value = (float)state;
            
            if (healthPoints.value == 0)
            {
                Die();
            }
        }

    }

}