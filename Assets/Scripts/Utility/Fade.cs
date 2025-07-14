using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Fade : MonoBehaviour
{
	private static Color m_fromColor;
	private static Color m_toColor;
	private static float m_fadeTime;
	private static float m_fadeDeltaTime;
	private static Action m_callback;

	private static Image m_image;

	public bool IsFade
	{
		get { return m_fadeTime > 0.0f; }
	}

	public Color NowColor
	{
		get { return m_image.color; }
	}

	private void Awake()
	{
		m_image = GetComponent<Image>();
		m_fadeTime = 0.0f;
	}

	void FixedUpdate()
	{
		if (m_fadeTime == 0.0f) return;

		m_fadeDeltaTime += Time.deltaTime;

		float t = m_fadeDeltaTime / m_fadeTime;
		if (m_fadeDeltaTime > m_fadeTime)
		{
			t = 1.0f;
			m_fadeTime = 0.0f;
			m_fadeDeltaTime = 0.0f;
		}

		m_image.color = Color.Lerp(m_fromColor, m_toColor, t);

		// コールバックが指定されていれば、フェード終了時に実行
		if (m_fadeTime == 0.0f && m_callback != null)
		{
			m_callback();
		}
	}

	public static void FadeOut(float time, Action callback = null)
	{
		FadeStart(time, new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), callback);
	}

	public static void FadeIn(float time, Action callback = null)
	{
		FadeStart(time, new Color(0, 0, 0, 1), new Color(0, 0, 0, 0), callback);
	}

	public static void FadeStart(float time, Color from, Color to, Action callback = null)
	{
		m_fadeTime = time;
		m_fadeDeltaTime = 0.0f;
		m_fromColor = from;
		m_toColor = to;
		m_callback = callback;
	}
}
