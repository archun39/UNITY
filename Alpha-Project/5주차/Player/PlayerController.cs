using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : NetworkBehaviour
{
    public float MoveSpeed;
    public float Smoothness = 0.12f;
    public float SpeedChangeRate = 10.0f;

    public bool Grounded = true;
    public float GroundedOffset = -0.14f;
    public float GroundedRadius = 0.28f;
    public LayerMask GroundLayers;

    public float JumpHeight = 1.2f;
    public float JumpTimeout = 0.5f;
    public float FallTimeout = 0.15f;

    public float Gravity = -15f;

    public GameObject cameraFollowTarget;

    //public GameObject nearTrash;

    //public List<GameObject> Trashs;

    private Camera _camera;
    private Animator _animator;
    private CharacterController _controller;

    private float _speed;
    private float _targetRotation;

    private bool _jump;

    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;

    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            _camera.GetComponentInParent<CameraController>().target = cameraFollowTarget.transform;
        }
    }

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _camera = Camera.main;
        _controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        _jumpTimeoutDelta = JumpTimeout;
        _fallTimeoutDelta = FallTimeout;
    }


    private void Update()
    {
        if (IsLocalPlayer)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _jump = true;
            }

            JumpAndGravity();
            GroundedCheck();
            Move();

            //GrapTrash();
        }
    }
    
    /*
    private void GrapTrash()
    {
        if (Input.GetButtonDown("Action"))
        {
            if (nearTrash != null && nearTrash.tag == "Trash")
            {
                Trash trash = nearTrash.GetComponent<Trash>();
                Trashs[trash.Id].SetActive(true);

                Destroy(trash.gameObject);
            }
        }
    }
    */


    /*
    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Trash"))
        {
            nearTrash = other.gameObject;
        }
        
        if (other.CompareTag("BePolice"))
        {
            this.tag = "BePolice";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Trash"))
        {
            nearTrash = null;
        }
    }
    */
    private void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(
            transform.position.x,
            transform.position.y - GroundedOffset,
            transform.position.z
        );

        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);

        _animator.SetBool("grounded", Grounded);
    }


    private void JumpAndGravity()
    {
        if (Grounded)
        {
            _fallTimeoutDelta = FallTimeout;

            _animator.SetBool("jump", false);

            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            if (_jump && _jumpTimeoutDelta <= 0.0f)
            {
                _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
                _animator.SetBool("jump", true);
            }

            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        } else
        {
            _jumpTimeoutDelta = JumpTimeout;

            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }

            _jump = false;
        }

        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += Gravity * Time.deltaTime;
        }
    }


    private void Move()
    {
        float targetSpeed = MoveSpeed;

        var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input == Vector2.zero)
        {
            targetSpeed = 0;
        }

        var currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0, _controller.velocity.z).magnitude;
        var speedOffset = 0.1f;

        // ����/����
        if (currentHorizontalSpeed < targetSpeed - speedOffset
            || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * SpeedChangeRate);
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        } else
        {
            _speed = targetSpeed;
        }

        // ĳ���� ȸ��
        if (input != Vector2.zero)
        {
            var inputDir = new Vector3(input.x, 0, input.y).normalized;

            _targetRotation = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg
                + _camera.transform.eulerAngles.y;

            var currentVelocity = 0f;
            var rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref currentVelocity, Smoothness);

            transform.rotation = Quaternion.Euler(0f, rotation, 0f);
        }

        // ���� ���� (0,0,1)�� �������� _targetRotation��ŭ ȸ���� ���͸� ���Ѵ�
        // ����� * ���� = ���͸� �������ŭ ȸ���� ����
        // ����� * ����� = ȸ�� ��ȯ ������ ������ �����
        var targetDirection = Quaternion.Euler(0f, _targetRotation, 0f) * Vector3.forward;

        var move = targetDirection.normalized * (_speed * Time.deltaTime)
            + new Vector3(0f, _verticalVelocity, 0f) * Time.deltaTime;

        _controller.Move(move);

        _animator.SetBool("isRunning", input != Vector2.zero);

        //Debug.DrawLine(_camera.transform.position, _camera.transform.position + new Vector3(0, _camera.transform.eulerAngles.y, 0), Color.green);
        //Debug.DrawLine(transform.position, transform.position + Vector3.forward * 10, Color.red);
    }

    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (Grounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(
            new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z),
            GroundedRadius);
    }
}
