using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startGame;
    [SerializeField] private Button _quitGame;
    private ISceneFlowHandler _sceneFlowHandler;


    [Inject]
    void InitInject(ISceneFlowHandler sceneFlowHandler)
    {
        _sceneFlowHandler = sceneFlowHandler;
    }
    
    private void OnEnable()
    {
        _startGame.onClick.AddListener(_sceneFlowHandler.LoadNextScene);
    }

    private void OnDisable()
    {
        _startGame.onClick.RemoveListener(_sceneFlowHandler.LoadNextScene); 
    }
}

interface ISceneFlowHandler
{
    public void LoadScene(string sceneName);
    public void LoadNextScene();
    public void Quit();
}

class SceneLoader : ISceneFlowHandler
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}