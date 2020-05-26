using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform _followTarget;

        // Use LateUpdate to ensure the camera moves after the character has finished moving to prevent gitter
        void LateUpdate()
        {
            transform.position = _followTarget.position;
        }
    }
}
