using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FinishEffects : MonoBehaviour
{
   public AudioSource audioSource;
   public ParticleSystem completeParticleSystem;
   bool winner = false;
   void Start()
   {
       completeParticleSystem = GetComponentInChildren<ParticleSystem>();
       audioSource = this.gameObject.GetComponent<AudioSource>();
   }


   void OnCollisionEnter(Collision other)
   {
       if (other.gameObject.tag == "Player" && winner == false)
       {
           winner = true;
           completeParticleSystem.GetComponent<ParticleSystemRenderer>().material = other.gameObject.GetComponent<Renderer>().material;
           completeParticleSystem.Play();
           audioSource.Play();
       }
   }
}