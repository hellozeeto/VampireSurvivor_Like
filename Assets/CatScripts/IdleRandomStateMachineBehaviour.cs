using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleRandomStateMachineBehaviour : StateMachineBehaviour
{
    #region Variables
    public int numberOfStates = 2; // 
    public float minNormTime = 0f; // idle �ּ� ����ð�
    public float maxNormTime = 5f; // idle �ִ� ����ð�

    public float randomNormalTime;

    readonly int hashRandomIdle = Animator.StringToHash("RandomIdle"); // �Ķ���ͷ� ������ ���� idle�� �����ϱ� ���� string hash �߰�, string �񱳿���� int�� �� ���꿡 ���� ����� int�� ������ �ξ� ���⶧���� string hash�� �ǵ��� �̿�



    #endregion Variables
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) //Animator���� �⺻���� ���¿� �߻��Ǵ� �Լ�, 
    {
        //Randomly decide a time at which to transition.
        randomNormalTime = Random.Range(minNormTime, maxNormTime); // minNormTime, maxNormTime ������ ���� ���� ������ �Ѵ�. 
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) // ó���� �⺻ ���·� ������ ���Ŀ� ������Ʈ �� �� ȣ�� �Ǵ� �Լ�
    {
        //If trasnsitioning away from this state rest the radnom idel parameter to -1.
        if (animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).fullPathHash == stateInfo.fullPathHash) // animator.IsInTransition(0)-> Baselayer�� 0�̴�. �� Base layer�� �ְų� ���� ������ �̸��� Ʋ���� ������
        {
            animator.SetInteger(hashRandomIdle, -1);  // hashRandomIdle�� -1�� ���� -> ���� idle ���� �ӽſ� ���� ���� �ʱ� ������ �ƹ��͵� ���� �ʴ´�.
        }

        //If the state is beyon the randomly decide normalised time and not yet transitioning
        if (stateInfo.normalizedTime > randomNormalTime && !animator.IsInTransition(0))
        {
            animator.SetInteger(hashRandomIdle, Random.Range(0, numberOfStates + 1)); // 1�� 2�϶� ���� �ٸ�  idle�� ���Ƿ� +1�� ����� ���� ���´� �ȱ׷��� 0��1�� ����
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
