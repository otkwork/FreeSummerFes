using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GunSelect : MonoBehaviour
{
	[SerializeField] private ExcelData m_excelData;
	[SerializeField] private PlayerInput m_playerInput;
	[SerializeField] private List<GameObject> m_gunUi;
	[SerializeField] private Transform[] m_setTransform;
	[SerializeField] private float m_moveSpeed = 1f; // UI�ړ����x

	private const int CenterIndex = 2; // ������UI�̃C���f�b�N�X

	private static bool m_isSelectTime = true;
	private GunUi[] m_uiScript;
	private Image[] m_setImage;
	private bool m_isMoveCorsor = false;
	private bool m_moveRight = false;
	private float m_elapsedTime = 0;

	private void Awake()
	{
		m_uiScript = new GunUi[m_gunUi.Count];
		m_setImage = new Image[m_setTransform.Length];
		for (int i = 0; i < m_gunUi.Count; ++i)
		{
			m_uiScript[i] = m_gunUi[i].GetComponent<GunUi>();
			m_setImage[i] = m_setTransform[i].GetComponent<Image>();
			m_gunUi[i].SetActive(false); // ������Ԃł͔�\��
			m_uiScript[i].SetData(m_excelData.Gun[SetNum(i, m_excelData.Gun.Count)]); // UI�ɏe�̃f�[�^��ݒ�
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
		if (!PlayerController.isShooting) return; // �˓I���łȂ��ꍇ�͉������Ȃ�

		var value = callback.ReadValue<Vector2>();
		m_isMoveCorsor = true;

		if (value.x < -0.5f) // �E�Ɉړ�
		{
			m_moveRight = true;
		}
	}

	void OnDecision(InputAction.CallbackContext callback)
	{
		// �˓I���ȊO�͉������Ȃ�
		if (!PlayerController.isShooting) return;
		if (!m_isSelectTime) return;

		for (int i = 0; i < m_gunUi.Count; ++i)
		{
			m_gunUi[i].SetActive(false); // �S�Ă�UI���\��
		}

		// �e��I��
		m_uiScript[CenterIndex].Decision();
		m_isSelectTime = false;
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
		for (int i = 0; i < m_gunUi.Count; ++i)
		{
			m_gunUi[i].SetActive(true); // UI��\��
			m_uiScript[i].SetStyle(m_setTransform[i].position, m_setTransform[i].localScale, m_setImage[i].color);
		}
	}

    private void FixedUpdate()
	{
		if (!PlayerController.isShooting)
		{
			// �˓I���ȊO�̓t���O�����Z�b�g����UI���\��
			for (int i = 0; i < m_gunUi.Count; ++i)
			{
				m_gunUi[i].SetActive(false); // UI���\��
			}

			m_isSelectTime = true;
			m_isMoveCorsor = false; 
			m_moveRight = false; 
			return;
		}

		if (!m_isMoveCorsor) return;

		m_elapsedTime += Time.deltaTime * m_moveSpeed;
		for (int i = 0; i < m_gunUi.Count; ++i)
		{
			Vector3 pos;
			Vector3 scale;
			Color color;

			int index = SetNum(i, m_gunUi.Count);

			if (m_moveRight)
			{
				int nextIndex = SetNum(index + 1, m_gunUi.Count);
				pos = Vector3.Lerp(m_setTransform[index].position, m_setTransform[nextIndex].position, m_elapsedTime); // �E�Ɉړ�
				scale = Vector3.Lerp(m_setTransform[index].localScale, m_setTransform[nextIndex].localScale, m_elapsedTime); // �T�C�Y��ύX
				color = Color.Lerp(m_setImage[index].color, m_setImage[nextIndex].color, m_elapsedTime); // �����x��ύX
            }
			else
			{
				int nextIndex = SetNum(index - 1, m_gunUi.Count);
				pos = Vector3.Lerp(m_setTransform[index].position, m_setTransform[nextIndex].position, m_elapsedTime); // ���Ɉړ�
				scale = Vector3.Lerp(m_setTransform[index].localScale, m_setTransform[nextIndex].localScale, m_elapsedTime); // �T�C�Y��ύX
				color = Color.Lerp(m_setImage[index].color, m_setImage[nextIndex].color, m_elapsedTime); // �����x��ύX				
			}

			m_uiScript[i].SetStyle(pos, scale, color);
		}

		if (m_elapsedTime >= 1f)
		{
			ReSetSelectList(m_moveRight); // UI�����������

			m_isMoveCorsor = false;
			m_moveRight = false;
			m_elapsedTime = 0; // �^�C�}�[�����Z�b�g
		}
	}

	// �l��0�ȉ��܂��͍ő吔�ȏ�̏ꍇ�������
	private int SetNum(int value, int max)
	{
        int num = value < 0 ? 
			max + value :
			value > max - 1 ?
			value - max : value;

		return num;
    }

	private void ReSetSelectList(bool isRight)
	{
		// �ێ�����l
		int keepIndex = isRight ? m_gunUi.Count - 1 : 0; // ���Ɉړ�����ꍇ�͍ŏ���UI��ێ�
		GameObject gunUi = m_gunUi[keepIndex];
		m_gunUi.RemoveAt(keepIndex); // �ŏ���UI���폜
		if (!isRight) m_gunUi.Add(gunUi);
		else m_gunUi.Insert(0, gunUi);

		for (int i = 0; i < m_gunUi.Count; ++i)
		{
			m_uiScript[i] = m_gunUi[i].GetComponent<GunUi>();
		}
	}

	public static bool isSelectTime
	{
		get { return m_isSelectTime; }
		set { m_isSelectTime = value; }
	}
}
