using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    public class GetTrigger : MonoBehaviour
    {
        [Tooltip("Triggered Object"), SerializeField]
        private TriggeredControl objectTriggered;

        public TriggeredControl GetTriggeredObject => objectTriggered;
    }
}