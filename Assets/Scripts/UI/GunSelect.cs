using System.Net.Sockets;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GunSelect : MonoBehaviour
{
	[SerializeField] private ExcelData m_excelData;
	[SerializeField] private PlayerInput m_playerInput;
	[SerializeField] private GameObject[] m_gunUi;
	[SerializeField] private Transform[] m_setTransform;
	[SerializeField] private float m_moveSpeed = 1f; // UI移動速度

	private const int CenterIndex = 2; // 中央の銃のインデックス（0から始まる）
	private const int MaxIndex = 4;

	private GunUi[] m_uiScript;
	private Image[] m_backImage;
	private Image[] m_gunImage;
	private Image[] m_setImage;
	private bool m_isMoveCorsor = false;
	private bool m_moveRight = false;
	private float m_elapsedTime = 0;
	private static int m_selectIndex; // 選択中の銃をのインデックス

	private void Awake()
	{
		m_setImage = new Image[m_setTransform.Length];
		m_uiScript = new GunUi[m_gunUi.Length];
		for (int i = 0; i < m_gunUi.Length; ++i)
		{
			m_uiScript[i] = m_gunUi[i].GetComponent<GunUi>();
			m_setImage[i] = m_setTransform[i].GetComponent<Image>();
			m_gunUi[i].SetActive(false); // 初期状態では非表示
		}
	}

    private void Start()
    {
        m_selectIndex = CenterIndex;
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
			m_selectIndex--;
		}
		else if (value.x > 0.5f) // 左に移動
		{
			m_selectIndex++;
		}

		m_selectIndex = SetNum(m_selectIndex, MaxIndex);
		Debug.Log(m_selectIndex);
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
			m_uiScript[i].SetStyle(m_setTransform[i].position, m_setTransform[i].localScale, m_setImage[i].color);

            m_uiScript[i].SetData(m_excelData.Gun[SetNum(i, m_excelData.Gun.Count)]);
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
			m_moveRight = false; 
			return;
		}

		if (!m_isMoveCorsor) return;

		m_elapsedTime += Time.deltaTime * m_moveSpeed;
		for (int i = 0; i < m_gunUi.Length; ++i)
		{
			Vector3 pos;
			Vector3 scale;
			Color color;

			int index = i >= MaxIndex ? 0 : i + 1;
			if (m_moveRight)
			{
				pos = Vector3.Lerp(m_setTransform[i].position, m_setTransform[index].position, m_elapsedTime); // 右に移動
				scale = Vector3.Lerp(m_setTransform[i].localScale, m_setTransform[index].localScale, m_elapsedTime); // サイズを変更
				color = Color.Lerp(m_setImage[i].color, m_setImage[index].color, m_elapsedTime); // 透明度を変更
            }
			else
			{
				pos = Vector3.Lerp(m_setTransform[index].position, m_setTransform[i].position, m_elapsedTime); // 左に移動
				scale = Vector3.Lerp(m_setTransform[index].localScale, m_setTransform[i].localScale, m_elapsedTime); // サイズを変更
				color = Color.Lerp(m_setImage[index].color, m_setImage[i].color, m_elapsedTime); // 透明度を変更				
			}

			m_uiScript[i].SetStyle(pos, scale, color);
		}

		if (m_elapsedTime >= 1f)
		{
			for (int i = 0; i < m_uiScript.Length; ++i)
			{
				int index = m_selectIndex - (CenterIndex - i);
                m_uiScript[i].SetData(m_excelData.Gun[SetNum(index, m_excelData.Gun.Count)]);
            }

			m_isMoveCorsor = false;
			m_moveRight = false;
			m_elapsedTime = 0; // タイマーをリセット
		}
	}

	// 値が0以下または最大数以上の場合一周する
	private int SetNum(int value, int max)
	{
        int num = value < 0 ? 
			max - 1 :
			value > max - 1 ?
			0 : value;

		return num;
    }
}
