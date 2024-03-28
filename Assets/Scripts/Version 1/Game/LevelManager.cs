using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _loaderCanvas;

    public static LevelManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
            Destroy(gameObject);

        _loaderCanvas.SetActive(false);
    }

    public async void LoadScene(string sceneName)
    {

        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        _loaderCanvas.SetActive(true);


        do
        {
            await Task.Delay(100);
        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive(false);

        

    }

    
    public async void LoadScene(int sceneNum)
    { 
        var scene = SceneManager.LoadSceneAsync(sceneNum);
        scene.allowSceneActivation = false;

        _loaderCanvas.SetActive(true);


        do
        {
            await Task.Delay(100);
        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive(false);



    }
}
