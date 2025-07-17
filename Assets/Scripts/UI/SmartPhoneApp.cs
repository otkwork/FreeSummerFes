using System.Collections.Generic;
using UnityEngine;

public class SmartPhoneApp : MonoBehaviour
{
	[SerializeField] Fade m_fade;
	[SerializeField] GameObject m_activeAppPage;
	[SerializeField] List<GameObject> m_disActiveAppPage;

	private static bool m_isTransition = false;

	public void OnClick()
	{
		if (m_isTransition || m_activeAppPage.activeSelf) return;
		m_isTransition = true;

		m_fade.FadeOut(0.5f, () =>
		{
			m_isTransition = false;
			m_activeAppPage.SetActive(true);

			foreach (GameObject app in m_disActiveAppPage)
			{
				app.SetActive(false);
			}
			m_fade.FadeIn(0.5f);
		});
	}
}
