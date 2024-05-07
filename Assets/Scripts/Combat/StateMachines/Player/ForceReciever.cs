using UnityEngine;
using UnityEngine.AI;

namespace Ludias.Combat.StateMachines.Player
{
    public class ForceReciever : MonoBehaviour
    {
        [SerializeField] float drag = 0.3f;

        private Vector3 impact;
        private Vector3 dampingVelocity;
        public Vector3 MovementForce => impact;
        private NavMeshAgent agent;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);

            if (agent != null)
            {
                if (impact.sqrMagnitude < 0.2f * 0.2f)
                {
                    impact = Vector3.zero;
                    agent.enabled = true;
                }
            }
        }

        public void AddForce(Vector3 force)
        {
            impact += force;

            if (agent != null)
            {
                agent.enabled = false;
            }
        }
    }
}
