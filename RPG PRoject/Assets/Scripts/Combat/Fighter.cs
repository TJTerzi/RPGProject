﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
    
     [SerializeField] float weaponRange = 2f;
     [SerializeField] float timeBetweenAttacks = 1f;
     [SerializeField] float weaponDamage = 5f;
     
     Health1 target;
     float timeSinceLastAttack = Mathf.Infinity;
    
     private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if(target == null) return;

            if(target.IsDead()) return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);

            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }

        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                // This will trigger the Hit() event.
                TriggerAttack();
                timeSinceLastAttack = 0;

            }

        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        // Animation Event
        void Hit()
        {
            if(target == null) { return; }
            target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

    public bool CanAttack(GameObject combatTarget)
    {
        if(combatTarget == null) {return false;}
        Health1 targetToTest = combatTarget.GetComponent<Health1>();
        return targetToTest != null && !targetToTest.IsDead();
    }

        public void Attack(GameObject combatTarget)
      {
          GetComponent<ActionScheduler>().StartAction(this);
          target = combatTarget.GetComponent<Health1>();
          print("Take that you short, squat peasant!");
      }



      public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

    }

}