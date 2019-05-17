﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingGame : MonoBehaviour
{

    public void Start()
    {
        StartCoroutine(LoadAsynchronously(1));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .2f);
            yield return null;

        }
    }

}
