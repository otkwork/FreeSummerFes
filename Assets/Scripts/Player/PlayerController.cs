using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	float m_speed = 5f;
	[SerializeField] GunSelect m_gunSelect; // 銃の選択UI

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
		// 屋台を見ているとき
		if (PlayerRay.lookStall && !m_isShooting)
		{
			m_isShooting = true;
			ChangeCamera.ShootingCamera();
			m_gunSelect.StartSetUi(); // 銃の選択UIを表示
		}
	}

	// アニメーションから呼ばれる
	public void ResetTrigger()
	{
		m_canMove = true;
		m_canDirection = true;
		//m_animator.ResetTrigger("Jump");
		//m_animator.ResetTrigger("Attack");
	}

	private void FixedUpdate()
	{
		// 射的中は移動を制限
		if (m_isShooting)
		{
			// モデルの非表示
			m_model.SetActive(false);
			return;
		}
		else
		{
			m_model.SetActive(true);
		}

		// カメラの正面ベクトルを作成
		Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

		// 進行方向にゆっくり向く
		if (m_direction != Vector3.zero)
		{
			// Y軸以外は固定する
			transform.rotation = Quaternion.Slerp(transform.rotation,
				Quaternion.LookRotation(m_velocity.normalized), 0.3f);
			transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
		}

		if (m_canDirection)
		{
			// カメラの向きを考慮した移動量
			m_velocity = cameraForward * m_direction.z + Camera.main.transform.right * m_direction.x;
			m_velocity *= m_speed * Time.deltaTime;
		}

		// 重力をかける
		m_velocity.y += Physics.gravity.y * Time.deltaTime;

		// 移動
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
