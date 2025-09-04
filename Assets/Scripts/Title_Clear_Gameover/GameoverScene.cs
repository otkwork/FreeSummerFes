using TMPro;
using UnityEngine;

public class GameoverScene : MonoBehaviour
{
	public enum GameoverType
	{
		None,
		TimeOver,
		Murder,
	}

	[SerializeField] TextMeshProUGUI m_timeOverText;
	[SerializeField] TextMeshProUGUI m_murderText;

	private static GameoverType m_gameOverType = GameoverType.None;

	public static void SetGameOverType(GameoverType type)
	{
		m_gameOverType = type;
	}

	private void Update()
	{
		if (m_gameOverType != GameoverType.None)
		{
			switch (m_gameOverType)
			{
				case GameoverType.TimeOver:
					m_timeOverText.gameObject.SetActive(true);
					break;

				case GameoverType.Murder:
					m_murderText.gameObject.SetActive(true);
					break;
			}
		}
	}
}
