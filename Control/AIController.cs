using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float _chaseDistance = 5f;

        private GameObject _player;
        private Fighter _fighter;

        private void Start()
        {
            _player = GameObject.FindWithTag("Player");
            _fighter = GetComponent<Fighter>();
        }

        private void Update()
        {
            if (IsInChaseRange() && _fighter.CanAttack(_player))
            {
                _fighter.Attack(_player);
            }
            else
            {
                _fighter.Cancel();
            }
        }

        private bool IsInChaseRange()
        {

            float distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);
            return distanceToPlayer < _chaseDistance;
        }
    }

}
