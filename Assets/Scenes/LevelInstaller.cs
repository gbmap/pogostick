using System;
using DRAP;
using DRAP.Objectives;
using UnityEngine;
using Zenject;

public class Signal {}

public class LevelInstaller : MonoInstaller
{
    [SerializeField] float levelTime;

    public override void InstallBindings()
    {
        InstallLevelRoot();
        InstallGameplaySignals();
        InstallLevelTimer();
    }

    private void InstallLevelRoot()
    {
        var levelRoot = GameObject.FindGameObjectWithTag("LevelRoot");
        if (levelRoot == null)
            throw new System.Exception("LevelRoot not found");

        LevelBounds levelBounds = new LevelBounds(levelRoot);
        Container.Bind<ILevelBounds>().FromInstance(levelBounds).AsSingle();
    }

    private void InstallGameplaySignals()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<OnObjectiveComplete>();
        Container.DeclareSignal<OnAllObjectivesComplete>();
        Container.DeclareSignal<OnLevelTimerEnded>();
    }

    private void InstallLevelTimer()
    {
        Container.BindInterfacesAndSelfTo<LevelTimer>().AsSingle().WithArguments(levelTime);
    }

}