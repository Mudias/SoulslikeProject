using UnityEngine;

namespace Ludias.Combat.StateMachines.Player
{
    public class ForceReciever : MonoBehaviour
    {
        [SerializeField] float drag = 0.3f;

        private Vector3 impact;
        private Vector3 dampingVelocity;
        public Vector3 MovementForce => impact;

        private void Update()
        {
            impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
        }

        public void AddForce(Vector3 force)
        {
            impact += force;
        }
    }
}
