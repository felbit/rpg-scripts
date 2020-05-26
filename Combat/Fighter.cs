using UnityEngine;
using RPG.Core;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float _weaponDamage = 5f;
        [SerializeField] private float _weaponRange = 2f;
        [SerializeField] private float _timeBetweenAttacks = 1f;

        private Health _target;
        private float _timeSinceLastAttack = 0f;

        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;

            if (_target == null || _target.IsDead()) return;

            if (IsInRange())
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
            else
            {
                GetComponent<Mover>().MoveTo(_target.transform.position);
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(_target.transform);

            if (_timeSinceLastAttack >= _timeBetweenAttacks)
            {
                // prevent attack from glitching after the player moved away
                // from an earlier encounter.
                GetComponent<Animator>().ResetTrigger("stopAttack");

                // this will trigger the Hit() event
                GetComponent<Animator>().SetTrigger("attack");
                _timeSinceLastAttack = 0f;


            }
        }

        // Animation Event
        private void Hit()
        {
            if (_target != null) // _target can become null between Update() and Hit()
                _target.TakeDamage(_weaponDamage);
        }

        private bool IsInRange()
        {
            return Vector3.Distance(_target.transform.position, transform.position) < _weaponRange;
        }

        public bool CanAttack(GameObject target)
        {
            if (target == null) return false;

            Health targetUnderTest = target.GetComponent<Health>();
            return targetUnderTest != null && !targetUnderTest.IsDead();
        }

        public void Attack(GameObject target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            _target = target.GetComponent<Health>();
        }

        public void Cancel()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
            _target = null;
        }
    }
}
