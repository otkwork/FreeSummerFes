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
				SystemScene.ChangeScene("Title", "Story");
                SceneFade.FadeIn(1.0f);
            });
        }
    }
}
