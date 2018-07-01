using System.Collections.Generic;
using Assets.Scripts;
using Planets;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller<ProjectInstaller>
{

    public override void InstallBindings()
    {

        Container.Bind<IPlayer>().To<Player>().AsSingle();
        Container.Bind<ICamera>().To<Planets.Camera>().AsSingle();
        Container.Bind<IConstants>().To<Planets.Constants>().AsSingle();
        Container.Bind<ISectorManager>().To<SectorManager>().AsSingle();
        Container.Bind<ISectorCreator>().To<SectorCreator>().AsSingle();
        Container.Bind<IComparer<int>>().To<PlanetComparer>().AsSingle();

        //Container.BindFactory<IShell, Shell.ShellFactory>().FromComponentInNewPrefab(mShellPrefab);
        //Container.BindFactory<IAirBomb, AirBomb.AirBombFactory>().FromComponentInNewPrefab(mAirbombPrefab);
    }
}