using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GunSelect : MonoBehaviour
{
	[SerializeField] private PlayerInput m_playerInput;
	[SerializeField] private GameObject[] m_gunUi;
	[SerializeField] private Transform[] m_setTransform;
	[SerializeField] private float m_moveSpeed = 1f; // UI移動速度

	private const int CenterIndex = 2; // 中央の銃のインデックス（0から始まる）

	private Image[] m_gunImage;
	private Image[] m_setImage;
	private bool m_isMoveCorsor = false;
	private bool m_moveRight = false;
	private bool m_moveLeft = false;
	private float m_elapsedTime = 0;
	//private int m_selectIndex = 0; // 選択中の銃をのインデックス

	private void Awake()
	{
		m_gunImage = new Image[m_gunUi.Length];
		for (int i = 0; i < m_gunUi.Length; ++i)
		{
			m_gunImage[i] = m_gunUi[i].GetComponent<Image>();
			m_gunUi[i].SetActive(false); // 初期状態では非表示
		}

		m_setImage = new Image[m_setTransform.Length];
		for (int i = 0; i < m_setTransform.Length; ++i)
		{
			m_setImage[i] = m_setTransform[i].GetComponent<Image>();
		}
	}

	private void OnEnable()
	{
		m_playerInput.actions["CursorMove"].performed += OnMove;
		m_playerInput.actions["Decision"].performed += OnDecision;
		m_playerInput.actions["Escape"].performed += OnEscape;
	}

	private void OnDisable()
	{
		m_playerInput.actions["CursorMove"].performed -= OnMove;
		m_playerInput.actions["Decision"].performed -= OnDecision;
		m_playerInput.actions["Escape"].performed -= OnEscape;
	}

	void OnMove(InputAction.CallbackContext callback)
	{
		if (m_isMoveCorsor) return; // 既に移動中なら何もしない

		var value = callback.ReadValue<Vector2>();
		m_isMoveCorsor = true;

		if (value.x < -0.5f) // 右に移動
		{
			m_moveRight = true;
		}
		else if (value.x > 0.5f) // 左に移動
		{
			m_moveLeft = true;
		}
	}

	void OnDecision(InputAction.CallbackContext callback)
	{
		for (int i = 0; i < m_gunUi.Length; ++i)
		{
			m_gunUi[i].SetActive(false); // 全てのUIを非表示
		}

		//m_gunUi[CenterIndex].
	}

	void OnEscape(InputAction.CallbackContext callback)
	{
		if (PlayerController.isShooting)
		{
			PlayerController.isShooting = false;
			ChangeCamera.MoveingCamera();
		}
	}

	public void StartSetUi()
	{
		for (int i = 0; i < m_gunUi.Length; ++i)
		{
			m_gunUi[i].SetActive(true); // UIを表示
			m_gunUi[i].transform.position = m_setTransform[i].position; // 初期位置に設定
			m_gunUi[i].transform.localScale = m_setTransform[i].localScale; // 初期サイズに設定
			m_gunImage[i].color = m_setImage[i].color; // 初期透明度に設定
		}
	}

	private void FixedUpdate()
	{
		if (!PlayerController.isShooting)
		{
			// 射的中以外はフラグをリセットしてUIを非表示
			for (int i = 0; i < m_gunUi.Length; ++i)
			{
				m_gunUi[i].SetActive(false); // UIを非表示
			}

			m_isMoveCorsor = false; 
			m_moveLeft = false; 
			m_moveRight = false; 
			return;
		}

		if (!m_isMoveCorsor) return;

		m_elapsedTime += Time.deltaTime * m_moveSpeed;
		for (int i = 0; i < m_gunUi.Length; ++i)
		{
			if (m_moveRight)
			{
				m_gunUi[i].transform.position = 
					Vector3.Lerp(m_setTransform[i].position, m_setTransform[i + 1].position, m_elapsedTime); // 右に移動
				m_gunUi[i].transform.localScale = 
					Vector3.Lerp(m_setTransform[i].localScale, m_setTransform[i + 1].localScale, m_elapsedTime); // サイズを変更
				m_gunImage[i].color = 
					Color.Lerp(m_setImage[i].color, m_setImage[i + 1].color, m_elapsedTime); // 透明度を変更
			}
			else if (m_moveLeft)
			{
				m_gunUi[i].transform.position =
					Vector3.Lerp(m_setTransform[i + 1].position, m_setTransform[i].position, m_elapsedTime); // 左に移動
				m_gunUi[i].transform.localScale =
					Vector3.Lerp(m_setTransform[i + 1].localScale, m_setTransform[i].localScale, m_elapsedTime); // サイズを変更
				m_gunImage[i].color =
					Color.Lerp(m_setImage[i + 1].color, m_setImage[i].color, m_elapsedTime); // 透明度を変更
			}
		}

		if (m_elapsedTime >= 1f)
		{
			m_isMoveCorsor = false;
			m_moveRight = false;
			m_moveLeft = false;
			m_elapsedTime = 0; // タイマーをリセット
		}
	}
}
