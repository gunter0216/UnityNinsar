using System;
using App.Common.AssetSystem.Runtime;
using App.Common.Configs.Runtime;
using App.Common.Utilities.Utility.Runtime;
using App.Core.Cubes.External.Presenter;
using App.Core.Cubes.External.Presenter.Fabric;
using App.Core.Cubes.Runtime.Config;
using App.Core.Cubes.Runtime.Services;
using UniRx;
using UnityEngine;

namespace App.Core.Cubes.External
{
    public class CubesController : IInitSystem, IDisposable
    {
        private readonly IConfigLoader m_ConfigLoader;
        private readonly IAssetManager m_AssetManager;

        private CubesConfigController m_ConfigController;
        private CubesPresenter m_Presenter;
        private CubesMoveStrategy m_MoveStrategy;
        private CharToColorMatrixConverter m_Converter;
        private IDisposable m_Disposable;

        public CubesController(IConfigLoader configLoader, IAssetManager assetManager)
        {
            m_ConfigLoader = configLoader;
            m_AssetManager = assetManager;
        }

        public void Init()
        {
            m_ConfigController = new CubesConfigController(m_ConfigLoader);
            m_ConfigController.Initialize();

            m_Presenter = new CubesPresenter(new CubeViewCreator(m_AssetManager));
            m_Presenter.Initialize();

            m_MoveStrategy = new CubesMoveStrategy(m_ConfigController);
            m_MoveStrategy.Initialize();

            m_Converter = new CharToColorMatrixConverter();

            m_Disposable = Observable.EveryUpdate()
                .Select(_ =>
                {
                    var direction = new Vector2Int
                    {
                        x = Input.GetKeyDown(KeyCode.A) ? -1 : Input.GetKeyDown(KeyCode.D) ? 1 : 0,
                        y = Input.GetKeyDown(KeyCode.W) ? 1 : Input.GetKeyDown(KeyCode.S) ? -1 : 0
                    };

                    return direction;
                })
                .Where(direction => direction != Vector2Int.zero)
                .Subscribe(Move);
            
            UpdateView();
        }

        private void Move(Vector2Int direction)
        {
            m_MoveStrategy.Move(direction);
            UpdateView();
        }

        private void UpdateView()
        {
            var grid = m_MoveStrategy.GetGrid();
            m_Presenter.UpdateCubes(m_Converter.Convert(grid));
        }

        public void Dispose()
        {
            m_Disposable?.Dispose();
        }
    }
}