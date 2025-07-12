using TMPro;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class WorldTime : MonoBehaviour
{
	enum TimeOfDay
	{
		Morning,// ��
		Noon,	// ��
		Evening,// �[��
		Night	// ��
	}

	[SerializeField] private Color[] m_sunColor;

	[SerializeField] private Light m_sun; // ���z�̃��C�g
	[SerializeField] private TextMeshProUGUI m_textTime;
	[SerializeField] private int m_timeScale = 1; // �Q�[�����̎��Ԃ̐i�s���x�i1�b��1���Ԑi�ށj

	private float m_time;		// ���A���̌o�ߎ���
	private int m_hourTime;		// �Q�[�����̎��ԁi���ԁj
	private int m_minuteTime;   // �Q�[�����̎��ԁi���j

	private readonly float[] ChangeTime =
	{
		5f, // �邩�璩�ւ̎��ԑ�
		12f, // �����璋�ւ̎��ԑ�
		18f, // ������[���ւ̎��ԑ�
		24f, // �[�������ւ̎��ԑ�
	};
	private const int StartSunRot = -90;

	void Start()
    {
		m_time = 0f;
		m_hourTime = 0;
		m_minuteTime = 0;
	}

    // Update is called once per frame
    void Update()
    {
		m_time += Time.deltaTime * m_timeScale;

		if (m_time >= 1f) // 1�b�o�߂��ƂɎ��Ԃ��X�V
		{
			m_minuteTime++;
			if (m_minuteTime >= 60)
			{
				m_minuteTime = 0;
				m_hourTime++;
				if (m_hourTime >= 24)
				{
					m_hourTime = 0;
				}
			}
			m_time -= 1f;
		}

		m_textTime.text = $"{m_hourTime:D2}:{m_minuteTime:D2}"; // ���Ԃ�\��
	}

	void LateUpdate()
	{
		float sunAngle = 360 - (m_hourTime + m_minuteTime / 60f) * 15; // 1���Ԃ�����15�x
		m_sun.transform.rotation = Quaternion.Euler(sunAngle + StartSunRot, -45, 0);

		if		(m_hourTime >= 0 && 
				 m_hourTime < ChangeTime[(int)TimeOfDay.Morning])
		{
			// �邩�璩
			m_sun.color = Color.Lerp
			(
				m_sunColor[(int)TimeOfDay.Night], 
				m_sunColor[(int)TimeOfDay.Morning],
				m_hourTime / ChangeTime[(int)TimeOfDay.Morning]
			); 
		}
		else if (m_hourTime >= ChangeTime[(int)TimeOfDay.Morning] && 
				 m_hourTime < ChangeTime[(int)TimeOfDay.Noon])
		{
			// Lerp�̊J�n�_��0�ɂ��邽�߂̕␳
			float setZero = ChangeTime[(int)TimeOfDay.Morning];
			// �����璋
			m_sun.color = Color.Lerp
			(
				m_sunColor[(int)TimeOfDay.Morning],
				m_sunColor[(int)TimeOfDay.Noon],
				(m_hourTime - setZero) / (ChangeTime[(int)TimeOfDay.Noon] - setZero)
			); 
		}
		else if (m_hourTime >= ChangeTime[(int)TimeOfDay.Noon] && 
				 m_hourTime < ChangeTime[(int)TimeOfDay.Evening])
		{
			// Lerp�̊J�n�_��0�ɂ��邽�߂̕␳
			float setZero = ChangeTime[(int)TimeOfDay.Noon];
			// ������[��
			m_sun.color = Color.Lerp
			(
				m_sunColor[(int)TimeOfDay.Noon], 
				m_sunColor[(int)TimeOfDay.Evening],
				(m_hourTime - setZero) / (ChangeTime[(int)TimeOfDay.Evening] - setZero)
			); 
		}
		else
		{
			// Lerp�̊J�n�_��0�ɂ��邽�߂̕␳
			float setZero = ChangeTime[(int)TimeOfDay.Evening];
			// �[�������
			m_sun.color = Color.Lerp
			(
				m_sunColor[(int)TimeOfDay.Evening],
				m_sunColor[(int)TimeOfDay.Night],
				(m_hourTime - setZero) / (ChangeTime[(int)TimeOfDay.Night] - setZero)
			); 
		}
	}
}
