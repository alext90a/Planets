using Planets;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller<ProjectInstaller>
{

    public override void InstallBindings()
    {

        Container.Bind<IPlayer>().To<Player>().AsSingle();
        Container.Bind<ICamera>().To<Planets.Camera>().AsSingle();
        //Container.BindFactory<IShell, Shell.ShellFactory>().FromComponentInNewPrefab(mShellPrefab);
        //Container.BindFactory<IAirBomb, AirBomb.AirBombFactory>().FromComponentInNewPrefab(mAirbombPrefab);
    }
}