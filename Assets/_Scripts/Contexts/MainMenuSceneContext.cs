using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenuSceneContext : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
    }
}
