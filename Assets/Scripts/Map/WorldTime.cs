using TMPro;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class WorldTime : MonoBehaviour
{
	enum TimeOfDay
	{
		Morning,// 朝
		Noon,	// 昼
		Evening,// 夕方
		Night	// 夜
	}

	[SerializeField] private Color[] m_sunColor;

	[SerializeField] private Light m_sun; // 太陽のライト
	[SerializeField] private TextMeshProUGUI m_textTime;
	[SerializeField] private int m_timeScale = 1; // ゲーム内の時間の進行速度（1秒で1時間進む）

	private float m_time;		// リアルの経過時間
	private int m_hourTime;		// ゲーム内の時間（時間）
	private int m_minuteTime;   // ゲーム内の時間（分）

	private readonly float[] ChangeTime =
	{
		5f, // 夜から朝への時間帯
		12f, // 朝から昼への時間帯
		18f, // 昼から夕方への時間帯
		24f, // 夕方から夜への時間帯
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

		if (m_time >= 1f) // 1秒経過ごとに時間を更新
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

		m_textTime.text = $"{m_hourTime:D2}:{m_minuteTime:D2}"; // 時間を表示
	}

	void LateUpdate()
	{
		float sunAngle = 360 - (m_hourTime + m_minuteTime / 60f) * 15; // 1時間あたり15度
		m_sun.transform.rotation = Quaternion.Euler(sunAngle + StartSunRot, -45, 0);

		if		(m_hourTime >= 0 && 
				 m_hourTime < ChangeTime[(int)TimeOfDay.Morning])
		{
			// 夜から朝
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
			// Lerpの開始点を0にするための補正
			float setZero = ChangeTime[(int)TimeOfDay.Morning];
			// 朝から昼
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
			// Lerpの開始点を0にするための補正
			float setZero = ChangeTime[(int)TimeOfDay.Noon];
			// 昼から夕方
			m_sun.color = Color.Lerp
			(
				m_sunColor[(int)TimeOfDay.Noon], 
				m_sunColor[(int)TimeOfDay.Evening],
				(m_hourTime - setZero) / (ChangeTime[(int)TimeOfDay.Evening] - setZero)
			); 
		}
		else
		{
			// Lerpの開始点を0にするための補正
			float setZero = ChangeTime[(int)TimeOfDay.Evening];
			// 夕方から夜
			m_sun.color = Color.Lerp
			(
				m_sunColor[(int)TimeOfDay.Evening],
				m_sunColor[(int)TimeOfDay.Night],
				(m_hourTime - setZero) / (ChangeTime[(int)TimeOfDay.Night] - setZero)
			); 
		}
	}
}
