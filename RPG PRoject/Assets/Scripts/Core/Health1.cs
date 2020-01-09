using UnityEngine;
using RPG.Saving;

namespace RPG.Core
{
    public class Health1 : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;


        bool isDead = false;



        public bool IsDead()
        {
            return isDead;
        }


        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            // This will trigger the Death() event.
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
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