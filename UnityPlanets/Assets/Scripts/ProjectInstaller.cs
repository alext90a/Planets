using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Assets.Scripts;
using Planets;
using QuadTree;
using UnityEngine;
using Zenject;

[SuppressMessage("ReSharper", "PossibleNullReferenceException")]
public class ProjectInstaller : MonoInstaller<ProjectInstaller>
{
    [SerializeField] private GameObject mPlanetDataObject;

    public override void InstallBindings()
    {
        Container.Bind<IPlayer>().To<Player>().AsSingle();
        Container.Bind<ICamera>().To<Camera>().AsSingle();
        Container.Bind<IComparer<int>>().To<PlanetComparer>().AsSingle();
        Container.BindFactory<UnityPlanetData, UnityPlanetDataFactory>().FromComponentInNewPrefab(mPlanetDataObject);
        Container.Bind<IStartNodeCreator>().To<StartNodeCreator>().AsSingle();
        Container.Bind<IPlanetFactoryCreator>().To<PlanetFactoryCreator>().AsSingle();
        Container.Bind<IStartUpNodeInitializer>().To<StartUpNodeInitializer>().AsSingle();
        Container.Bind<IArrayBackgroundWorker>().To<ArrayBackgroundWorker>().AsTransient();
        Container.Bind<IQuadTreeNodeMerger>().To<QuadTreeNodeMerger>().AsTransient();
        Container.Bind<IThreadQuadTreeNodeCreatorFactory>().To<ThreadQuadTreeNodeCreatorFactory>().AsSingle();
    }
}