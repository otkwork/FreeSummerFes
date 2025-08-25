using UnityEngine;

public class TitleScene : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKey)
        {
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
