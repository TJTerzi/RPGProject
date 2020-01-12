using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources
{
    public class HealthDisplay : MonoBehaviour
    {
        Health1 health;

        private void Awake() 
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health1>();    
        }

        private void Update() 
        {
            GetComponent<Text>().text = String.Format("{0:0}%", health.GetPercentage());
        }
    }

}