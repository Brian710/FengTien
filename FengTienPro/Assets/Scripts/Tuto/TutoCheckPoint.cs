using UnityEngine;
using UnityEngine.Events;

namespace MinYanGame.Core
{
    public class TutoCheckPoint : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem particle;

        public float force = 3f;

        public UnityEvent onTriggerEnter;

        private void Start()
        {
            if (particle.isPlaying)
                particle.Stop();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "TutoObj")
            {
                onTriggerEnter.Invoke();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.attachedRigidbody)
                other.attachedRigidbody.AddForce(Vector3.up * force, ForceMode.Acceleration);
        }

        public void ShowParticle(bool value)
        {
            if (value) particle.Play(true);
            else particle.Stop(true);
        }
    }
}