using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class AudioEvents : MonoBehaviour
    {
        [SerializeField]
        private AudioClip[] _footSteps;

        private AudioSource _audioSource;

        void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        
        public void WalkFootstep()
        {
            _audioSource.pitch = 1f;
            _audioSource.PlayOneShot(_footSteps[Random.Range(0, _footSteps.Length)]);
        }

        public void RunFootstep()
        {
            _audioSource.pitch = 0.75f;
            _audioSource.PlayOneShot(_footSteps[Random.Range(0, _footSteps.Length)]);
        }
    }
}