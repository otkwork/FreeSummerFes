using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class EndScene : MonoBehaviour
{
	[SerializeField] private string[] m_storyText;
	private TextMeshProUGUI m_text;
	private int m_textIndex = 0;
	private bool m_isFade = false;

	void Start()
	{
		m_text = GetComponent<TextMeshProUGUI>();
		m_text.text = Regex.Unescape(m_storyText[m_textIndex]);
	}

	void Update()
	{
		if (Input.anyKey && !m_isFade)
		{
			m_isFade = true;
			SceneFade.FadeOut(0.5f, () =>
			{
				if (m_textIndex < m_storyText.Length - 1)
				{
					m_textIndex++;
					m_text.text = Regex.Unescape(m_storyText[m_textIndex]);
					m_isFade = false;
					SceneFade.FadeIn(0.5f);
				}
				else
				{
					SystemScene.Load("Title");
					SystemScene.AllClearScene();
					SceneFade.FadeIn(0.5f);
				}
			});
		}
	}
}
