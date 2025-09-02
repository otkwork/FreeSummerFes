using UnityEngine;

public class StoryScene : MonoBehaviour
{
	private bool isFade = false;

    private void Update()
    {
        if (Input.anyKey && !isFade)
        {
			isFade = true;
            SceneFade.FadeOut(1.0f, () =>
            {
				/*
                SystemScene.Load("WorldTime");
                SystemScene.Load("Shrine");
                SystemScene.Load("Player");
				*/
				SystemScene.Load("Clear");
                SystemScene.Unload("Story");
                SceneFade.FadeIn(1.0f);
            });
        }
    }
}
