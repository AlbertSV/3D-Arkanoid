using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    public class MenuClose : StateMachineBehaviour
    {
        public GameObject menuPanelUI;

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            menuPanelUI.SetActive(false);
        }
  
    }
}