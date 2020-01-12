using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    public class CinematicsControlRemover : MonoBehaviour
    {

        GameObject player;
        
        
        private void Start() 
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
            GameObject player = GameObject.FindWithTag("Player");
        }

        private void DisableControl(PlayableDirector playableDirector)
        {
           
           player.GetComponent<ActionScheduler>().CancelCurrentAction();
           player.GetComponent<PlayerController>().enabled = false; 
        }

        private void EnableControl(PlayableDirector playableDirector)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}
