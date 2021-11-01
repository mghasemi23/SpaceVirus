using UnityEngine;

namespace Assets.Scripts.Levels
{
    public class ParticleContainer : MonoBehaviour
    {
        public ParticleSystem[] Particles;

        public void Play()
        {
            for (var i = 0; i < Particles.Length; i++)
            {
                Particles[i].Play(false);
            }
        }
    }
}
