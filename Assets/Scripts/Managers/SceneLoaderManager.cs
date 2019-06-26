using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour {

    #region Singleton

    public static SceneLoaderManager instance { get; private set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public GameObject loadScreen;
    public float waitTime;

    private void Start()
    {
        loadScreen.SetActive(false);
    }

    public void LoadScene(int sceneIndex)
    {
        loadScreen.SetActive(true);
        StartCoroutine("LoadNewScene", sceneIndex);
    }

    private IEnumerator LoadNewScene(int sceneIndex)
    {
        yield return new WaitForSeconds(waitTime);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);

        while (!async.isDone)
        {
            yield return null;
        }

    }
}
