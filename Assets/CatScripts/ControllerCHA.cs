using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCHA : MonoBehaviour
{
    // Start is called before the first frame update
    #region Variables
    public float speed = 5f;
    public float jumpHeight = 2f;
    public float dashDistance = 5f;
    public float dashDuration = 0.2f;  // 대쉬 지속 시간 추가

    private Animator animator;

    public float gravity = -9.81f; // 중력 설정을 위한 변수
    public Vector3 drags; // 저항 값 설정을 위한 변수 

    private CharacterController characterController;

    private Vector3 move = Vector3.zero;

    private bool isGrounded = false;
    public LayerMask groundLayerMask;
    public float groundCheckDistance = 0.3f;

    private Vector3 calcVelocity;
    private bool isDashing = false;  // 대쉬 상태를 추적하는 변수

    #endregion Variables
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();  // Animator 컴포넌트 가져오기
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = characterController.isGrounded;
        if (isGrounded && calcVelocity.y < 0)
        {
            calcVelocity.y = 0; // 땅에 있을 때는 더이상 중력값을 안받아도 됨으로
        }

        //Process move inputs
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.Move(move * Time.deltaTime * speed);

        if (move != Vector3.zero && !isDashing) // 이동 중이며 대쉬 중이 아닌 경우
        {
            transform.forward = move; // 입력한 방향으로 설정한다.
            animator.SetBool("Move", true); // Move 애니메이션 활성화
        }
        else
        {
            animator.SetBool("Move", false); // Move 애니메이션 비활성화
        }

        //Process jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            calcVelocity.y += Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }

        //Process dash input
        if (Input.GetButtonDown("Dash") && !isDashing) // 대쉬 중이 아닌 상태에서 Dash 입력 감지
        {
            StartCoroutine(Dash());
        }

        // Progress gravity
        calcVelocity.y += gravity * Time.deltaTime;

        //Process dash ground drags
        calcVelocity.x /= 1 + drags.x * Time.deltaTime;
        calcVelocity.y /= 1 + drags.y * Time.deltaTime;
        calcVelocity.z /= 1 + drags.z * Time.deltaTime;

        characterController.Move(calcVelocity * Time.deltaTime);
    }

    // Dash 코루틴 추가
    IEnumerator Dash()
    {
        isDashing = true;  // 대쉬 상태 활성화
        animator.SetBool("Dash", true);  // Dash 애니메이션 활성화

        Vector3 dashVelocity = Vector3.Scale(transform.forward,
            dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * drags.x + 1)) / -Time.deltaTime),
            0,
            (Mathf.Log(1f / (Time.deltaTime * drags.z + 1)) / -Time.deltaTime)));
        calcVelocity += dashVelocity;

        yield return new WaitForSeconds(dashDuration);  // 대쉬 지속 시간만큼 대기

        animator.SetBool("Dash", false);  // Dash 애니메이션 비활성화
        isDashing = false;  // 대쉬 상태 비활성화
    }

    #region Helper Methods

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
#if UNITY_EDITOR // 충돌 검사를 위한 디버그용 그래픽 설정
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f),
            transform.position + (Vector3.up * 0.1f) + (Vector3.down * groundCheckDistance));
#endif
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), // 0.1f를 곱하는 이유는 발끝이 지형에 묻혀도 원할히 체크할 수 있게
            Vector3.down, out hitInfo, groundCheckDistance, groundLayerMask))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    #endregion Helper Methods
}
