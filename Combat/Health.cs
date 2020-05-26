using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _health = 100f;

        private bool _isDead = false;

        public bool IsDead()
        {
            return _isDead;
        }

        public void TakeDamage(float damage)
        {
            if (_isDead) return;

            // Using UnityEngine.Mathf instead of System.Math because
            // Unity uses float values everywhere and System.Math operates
            // on double values.
            _health = Mathf.Max(_health - damage, 0);

            if (_health == 0)
            {
                GetComponent<Animator>().SetTrigger("die");
                _isDead = true;
            }
        }
    }
}