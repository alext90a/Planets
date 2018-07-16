using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.QuadTree;
using JetBrains.Annotations;
using Planets;
using QuadTree;

namespace Assets.Scripts
{
    public sealed class StartUpNodeInitializer : IStartUpNodeInitializer
    {
        [NotNull]
        private readonly IPlanetFactoryCreator mPlanetFactoryCreator;

        [NotNull] private readonly IArrayBackgroundWorker mBackgroundWorker;

        [NotNull] private readonly IConstants mConstants;

        

        public StartUpNodeInitializer([NotNull] IPlanetFactoryCreator planetFactoryCreator
            , [NotNull] IConstants constants
            , [NotNull] IArrayBackgroundWorker backgroundWorker)
        {
            mPlanetFactoryCreator = planetFactoryCreator;
            mConstants = constants;
            mBackgroundWorker = backgroundWorker;
        }

        public void Run(IAABBox startViewBox, IQuadTreeNode rootNode, IArrayBackgroundWorkerListener listener)
        {
            var visibleNodesCollector = new VisibleNodesCollector();
            rootNode.VisitVisibleNodes(startViewBox, visibleNodesCollector);
            var planetFactory = mPlanetFactoryCreator.CreatePlanetFactory();
            foreach (var curLeaf in visibleNodesCollector.GetVisibleLeaves())
            {
                curLeaf.SetPlanets(planetFactory.CreatePlanetsForSector());    
            }

            var nodesInCameraCollector = new VisibleNodesCollector();
            rootNode.VisitVisibleNodes(new AABBox(0f,0f, mConstants.GetMaxCameraSize(), mConstants.GetMaxCameraSize()), nodesInCameraCollector);
            
            mBackgroundWorker.AddListener(listener);
            mBackgroundWorker.Run(nodesInCameraCollector.GetVisibleLeaves(), mPlanetFactoryCreator);
        }


        
    }
}
