using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GunSelect : MonoBehaviour
{
	[SerializeField] private PlayerInput m_playerInput;
	[SerializeField] private GameObject[] m_gunUi;
	[SerializeField] private Transform[] m_setTransform;
	[SerializeField] private float m_moveSpeed = 1f; // UI�ړ����x

	private const int CenterIndex = 2; // �����̏e�̃C���f�b�N�X�i0����n�܂�j

	private Image[] m_gunImage;
	private Image[] m_setImage;
	private bool m_isMoveCorsor = false;
	private bool m_moveRight = false;
	private bool m_moveLeft = false;
	private float m_elapsedTime = 0;
	//private int m_selectIndex = 0; // �I�𒆂̏e���̃C���f�b�N�X

	private void Awake()
	{
		m_gunImage = new Image[m_gunUi.Length];
		for (int i = 0; i < m_gunUi.Length; ++i)
		{
			m_gunImage[i] = m_gunUi[i].GetComponent<Image>();
			m_gunUi[i].SetActive(false); // ������Ԃł͔�\��
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
		if (m_isMoveCorsor) return; // ���Ɉړ����Ȃ牽�����Ȃ�

		var value = callback.ReadValue<Vector2>();
		m_isMoveCorsor = true;

		if (value.x < -0.5f) // �E�Ɉړ�
		{
			m_moveRight = true;
		}
		else if (value.x > 0.5f) // ���Ɉړ�
		{
			m_moveLeft = true;
		}
	}

	void OnDecision(InputAction.CallbackContext callback)
	{
		for (int i = 0; i < m_gunUi.Length; ++i)
		{
			m_gunUi[i].SetActive(false); // �S�Ă�UI���\��
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
			m_gunUi[i].SetActive(true); // UI��\��
			m_gunUi[i].transform.position = m_setTransform[i].position; // �����ʒu�ɐݒ�
			m_gunUi[i].transform.localScale = m_setTransform[i].localScale; // �����T�C�Y�ɐݒ�
			m_gunImage[i].color = m_setImage[i].color; // ���������x�ɐݒ�
		}
	}

	private void FixedUpdate()
	{
		if (!PlayerController.isShooting)
		{
			// �˓I���ȊO�̓t���O�����Z�b�g����UI���\��
			for (int i = 0; i < m_gunUi.Length; ++i)
			{
				m_gunUi[i].SetActive(false); // UI���\��
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
					Vector3.Lerp(m_setTransform[i].position, m_setTransform[i + 1].position, m_elapsedTime); // �E�Ɉړ�
				m_gunUi[i].transform.localScale = 
					Vector3.Lerp(m_setTransform[i].localScale, m_setTransform[i + 1].localScale, m_elapsedTime); // �T�C�Y��ύX
				m_gunImage[i].color = 
					Color.Lerp(m_setImage[i].color, m_setImage[i + 1].color, m_elapsedTime); // �����x��ύX
			}
			else if (m_moveLeft)
			{
				m_gunUi[i].transform.position =
					Vector3.Lerp(m_setTransform[i + 1].position, m_setTransform[i].position, m_elapsedTime); // ���Ɉړ�
				m_gunUi[i].transform.localScale =
					Vector3.Lerp(m_setTransform[i + 1].localScale, m_setTransform[i].localScale, m_elapsedTime); // �T�C�Y��ύX
				m_gunImage[i].color =
					Color.Lerp(m_setImage[i + 1].color, m_setImage[i].color, m_elapsedTime); // �����x��ύX
			}
		}

		if (m_elapsedTime >= 1f)
		{
			m_isMoveCorsor = false;
			m_moveRight = false;
			m_moveLeft = false;
			m_elapsedTime = 0; // �^�C�}�[�����Z�b�g
		}
	}
}
