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

    private Animator animator;

    public float gravity = -9.81f; // �߷� ������ ���� ����
    public Vector3 drags; // ���� �� ������ ���� ���� 

    private CharacterController characterController;

    private Vector3 move = Vector3.zero;

    private bool isGrounded = false;
    public LayerMask groundLayerMask;
    public float groundCheckDistance = 0.3f;

    private Vector3 calcVelocity;

    #endregion Variables
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();  // Animator ������Ʈ ��������
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = characterController.isGrounded;
        if (isGrounded && calcVelocity.y < 0)
        {
            calcVelocity.y = 0; // ���� ���� ���� ���̻� �߷°��� �ȹ޾Ƶ� ������
        }

        //Process move inputs
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.Move(move * Time.deltaTime * speed);

        if (move != Vector3.zero) // �Է��� ����ǰ� ������
        {
            transform.forward = move; // �Է��� �������� �����Ѵ�.
            animator.SetBool("Move", true); // Move �ִϸ��̼� Ȱ��ȭ
        }

        else
        {
            animator.SetBool("Move", false); // Move �ִϸ��̼� ��Ȱ��ȭ
        }

        //Process jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            calcVelocity.y += Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }
        //Process dash input
        if (Input.GetButtonDown("Dash")) // edit -> project settings���� Ű �Ҵ�
        {
            Vector3 dashVelocity = Vector3.Scale(transform.forward,
                dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * drags.x + 1)) / -Time.deltaTime),
                0,
                (Mathf.Log(1f / (Time.deltaTime * drags.z + 1)) / -Time.deltaTime)));
            calcVelocity += dashVelocity;
        }

        // Progress gravity
        calcVelocity.y += gravity * Time.deltaTime;

        //Process dash ground drags
        calcVelocity.x /= 1 + drags.x * Time.deltaTime;
        calcVelocity.y /= 1 + drags.y * Time.deltaTime;
        calcVelocity.z /= 1 + drags.z * Time.deltaTime;

        characterController.Move(calcVelocity * Time.deltaTime);
    }




    #region Helper Methods

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
#if UNITY_EDITOR//�浹 �˻縦 ���� ����׿� �׷��� ����
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f),
            transform.position + (Vector3.up * 0.1f) + (Vector3.down * groundCheckDistance));
#endif
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), // 0.1f�� ���ϴ� ������ �߳��� ������ ������ ������ üũ�� �� �ְ�
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