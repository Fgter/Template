using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoader : MonoSingleton<SceneLoader>
{
    [SerializeField]
    Image fade;
    WaitForSeconds time = new WaitForSeconds(0.01f);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="fade">ÊÇ·ñ½¥Èë½¥³ö</param>
    public void LoadSceneAsync(string sceneName, bool fade = true)
    {
        StartCoroutine(LoadSceneAsyncCor(sceneName, fade));
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator LoadSceneAsyncCor(string sceneName, bool fade)
    {
        yield return LoadSceneCor(sceneName, fade);
        yield return UnloadSceneCor(fade);
    }
    IEnumerator LoadSceneCor(string sceneName, bool fade)
    {
        if (fade)
            yield return FadeOut();
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

    }

    IEnumerator UnloadSceneCor(bool fade)
    {
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        if (fade)
            yield return FadeIn();
    }

    IEnumerator FadeIn()
    {
        float r = 1;
        while (r > 0)
        {
            r -= 0.02f;
            fade.color = new Color(0, 0, 0, r);
            yield return time;
        }
    }

    IEnumerator FadeOut()
    {
        float r = 0;
        while (r < 1)
        {
            r += 0.02f;
            fade.color = new Color(0, 0, 0, r);
            yield return time;
        }
    }
}
