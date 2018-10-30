using Managers;
using UnityEngine;

namespace Tools
{
    public class LogoTransition : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            ScenesManager.Instance.LoadTonesScene("Instructions");
        }
    }
}
