using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using System;

namespace RPG.Resources
{
    public class Health1 : MonoBehaviour, ISaveable
    {

        [SerializeField] float regenerationPercentage = 70;

        float healthPoints = -1f;


        bool isDead = false;

        private void Start() 
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
            if(healthPoints < 0)
            {
                healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health1);
            }
        }


        public bool IsDead()
        {
            return isDead;
        }


        public void TakeDamage(GameObject instigator, float damage)
        {
            print(gameObject.name + " took damage: " + damage);
            
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        public float GetHealthPoints()
        {
            return healthPoints;
        }

        public float GetMaxHealthPoints()
        {
           return GetComponent<BaseStats>().GetStat(Stat.Health1);
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health1));

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
            healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health1);
           // float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health1) * (regenerationPercentage / 100);
           // healthPoints = Mathf.Max(healthPoints, regenHealthPoints);
        }


        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            
            if (healthPoints == 0)
            {
                Die();
            }
        }

    }

}