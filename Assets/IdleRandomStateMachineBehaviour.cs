using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleRandomStateMachineBehaviour : StateMachineBehaviour
{
    #region Variables
    public int numberOfStates = 2; // 
    public float minNormTime = 0f; // idle 최소 실행시간
    public float maxNormTime = 5f; // idle 최대 실행시간

    public float randomNormalTime;

    readonly int hashRandomIdle = Animator.StringToHash("RandomIdle"); // 파라미터로 설정한 랜덤 idle에 접근하기 위해 string hash 추가, string 비교연산과 int형 비교 연산에 대한 비용은 int형 연산이 훨씬 적기때문에 string hash를 되도록 이용



    #endregion Variables
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) //Animator에서 기본적인 상태에 발생되는 함수, 
    {
        //Randomly decide a time at which to transition.
        randomNormalTime = Random.Range(minNormTime, maxNormTime); // minNormTime, maxNormTime 사이의 랜덤 값을 가지게 한다. 
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) // 처음에 기본 상태로 진입한 이후에 업데이트 될 때 호출 되는 함수
    {
        //If trasnsitioning away from this state rest the radnom idel parameter to -1.
        if (animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).fullPathHash == stateInfo.fullPathHash) // animator.IsInTransition(0)-> Baselayer는 0이다. 즉 Base layer에 있거나 현재 상태의 이름과 틀리기 때문에
        {
            animator.SetInteger(hashRandomIdle, -1);  // hashRandomIdle을 -1로 설정 -> 현재 idle 상태 머신에 들어와 있지 않기 때문에 아무것도 하지 않는다.
        }

        //If the state is beyon the randomly decide normalised time and not yet transitioning
        if (stateInfo.normalizedTime > randomNormalTime && !animator.IsInTransition(0))
        {
            animator.SetInteger(hashRandomIdle, Random.Range(0, numberOfStates + 1)); // 1과 2일때 각각 다른  idle로 가므로 +1을 해줘야 값이 나온다 안그러면 0과1만 나옴
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
