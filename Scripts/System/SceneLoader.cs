using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    private readonly float COMPLETE_VALUE = 0.9f;

    private IEnumerator CoroutineLoadScene(Enums.Scene before, Enums.Scene after)
    {
        AsyncOperation loadingAO = SceneManager.LoadSceneAsync(Enums.Scene.Loading.ToString(), LoadSceneMode.Additive);
        AsyncOperation afterAO = SceneManager.LoadSceneAsync(after.ToString(), LoadSceneMode.Additive);
        afterAO.allowSceneActivation = false;

        yield return new WaitUntil(() => loadingAO.isDone);
        yield return new WaitUntil(() => afterAO.progress >= COMPLETE_VALUE);

        yield return CoroutineUnloadScene(before);

        afterAO.allowSceneActivation = true;
        yield return new WaitUntil(() => afterAO.isDone);

        yield return CoroutineUnloadScene(Enums.Scene.Loading);
    }

    private IEnumerator CoroutineLoadScene(Enums.Scene name)
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(name.ToString(), LoadSceneMode.Additive);
        loadScene.allowSceneActivation = false;

        yield return new WaitUntil(() => loadScene.progress >= COMPLETE_VALUE);
        loadScene.allowSceneActivation = true;
        yield return new WaitUntil(() => loadScene.isDone);
    }

    private IEnumerator CoroutineUnloadScene(Enums.Scene scene)
    {
        Scene activeScene = SceneManager.GetActiveScene();

        if (activeScene.name == scene.ToString())
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene otherScene = SceneManager.GetSceneAt(i);
                if (otherScene.name != scene.ToString() && otherScene.isLoaded)
                {
                    SceneManager.SetActiveScene(otherScene);
                    break;
                }
            }
        }

        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(scene.ToString());
        yield return unloadOperation.isDone;
    }

    public Coroutine LoadScene(Enums.Scene before, Enums.Scene after)
    {
        return StartCoroutine(CoroutineLoadScene(before, after));
    }

    public Coroutine LoadScene(Enums.Scene name)
    {
        return StartCoroutine(CoroutineLoadScene(name));
    }

    public Coroutine UnloadScene(Enums.Scene name)
    {
        return StartCoroutine(CoroutineUnloadScene(name));
    }

    public bool IsActiveScene(Enums.Scene scene)
    {
        Scene activeScene = SceneManager.GetActiveScene();

        return activeScene.name == scene.ToString();
    }
}
