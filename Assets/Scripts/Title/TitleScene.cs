using UnityEngine;

public class TitleScene : MonoBehaviour
{
	private bool isFade = false;

    private void Update()
    {
        if (Input.anyKey && !isFade)
        {
			isFade = true;
            SceneFade.FadeOut(1.0f, () =>
            {
                SystemScene.Load("WorldTime");
                SystemScene.Load("Shrine");
                SystemScene.Load("Player");
                SystemScene.Unload("Title");
                SceneFade.FadeIn(1.0f);
            });
        }
    }
}
