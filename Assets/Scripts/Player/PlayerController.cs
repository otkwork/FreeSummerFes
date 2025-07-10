using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	float m_speed = 5f;
	[SerializeField] GunSelect m_gunSelect; // �e�̑I��UI

	Vector3 m_direction;
	Vector3 m_velocity;
	GameObject m_model;
	Animator m_animator;
	PlayerInput m_playerInput;
	CharacterController m_charaCon;

	bool m_canMove;
	bool m_canDirection;
    static bool m_isShooting;

	private void Awake()
	{
		m_model = transform.GetChild(0).gameObject;
		m_playerInput = GetComponent<PlayerInput>();
		m_animator = GetComponent<Animator>();
		m_charaCon = GetComponent<CharacterController>();
	}

	private void Start()
	{
		m_direction = new Vector3(0, 0, 0);
		m_velocity = new Vector3(0, 0, 0);
		m_canMove = true;
		m_canDirection = true;
		m_isShooting = false;

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void OnEnable()
	{
		m_playerInput.actions["Move"].performed += OnMove;
		m_playerInput.actions["Move"].canceled += OnMoveCancel;
		m_playerInput.actions["Decision"].performed += OnDecision;
	}

	private void OnDisable()
	{
		m_playerInput.actions["Move"].performed -= OnMove;
		m_playerInput.actions["Move"].canceled -= OnMoveCancel;
		m_playerInput.actions["Decision"].performed -= OnDecision;
	}

	private void OnMove(InputAction.CallbackContext callback)
	{
		var value = callback.ReadValue<Vector2>();
		m_direction = new Vector3(value.x, 0, value.y);
		m_animator.SetBool("Move", true);
	}

	void OnMoveCancel(InputAction.CallbackContext callback)
	{
		m_direction = Vector3.zero;
		m_animator.SetBool("Move", false);
	}

	void OnDecision(InputAction.CallbackContext callback)
	{
		// ��������Ă���Ƃ�
		if (PlayerRay.lookStall && !m_isShooting)
		{
			m_isShooting = true;
			ChangeCamera.ShootingCamera();
			m_gunSelect.StartSetUi(); // �e�̑I��UI��\��
		}
	}

	// �A�j���[�V��������Ă΂��
	public void ResetTrigger()
	{
		m_canMove = true;
		m_canDirection = true;
		//m_animator.ResetTrigger("Jump");
		//m_animator.ResetTrigger("Attack");
	}

	private void FixedUpdate()
	{
		// �˓I���͈ړ��𐧌�
		if (m_isShooting)
		{
			// ���f���̔�\��
			m_model.SetActive(false);
			return;
		}
		else
		{
			m_model.SetActive(true);
		}

		// �J�����̐��ʃx�N�g�����쐬
		Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

		// �i�s�����ɂ���������
		if (m_direction != Vector3.zero)
		{
			// Y���ȊO�͌Œ肷��
			transform.rotation = Quaternion.Slerp(transform.rotation,
				Quaternion.LookRotation(m_velocity.normalized), 0.3f);
			transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
		}

		if (m_canDirection)
		{
			// �J�����̌������l�������ړ���
			m_velocity = cameraForward * m_direction.z + Camera.main.transform.right * m_direction.x;
			m_velocity *= m_speed * Time.deltaTime;
		}

		// �d�͂�������
		m_velocity.y += Physics.gravity.y * Time.deltaTime;

		// �ړ�
		if (m_canMove)
		{
			m_charaCon.Move(m_velocity);
		}
	}

	public static bool isShooting
	{
		get { return m_isShooting; }
		set { m_isShooting = value; }
	}
}
