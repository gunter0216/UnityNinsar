using App.Common.FSM.External;
using App.Common.FSM.Runtime;
using App.Common.Utilities.Utility.Runtime;
using App.Core.Startups.External.Constants;
using Zenject;

namespace App.Core.Startups.External
{
    /// <summary>
    /// Startup для стартовой сцены
    /// </summary>
    public class StartSceneStartup : MonoInstaller<StartSceneStartup>
    {
        public override void InstallBindings()
        {
            var configuratorsManager = Container.Resolve<ConfiguratorsManager>();
            var fsmRegistrator = Container.Resolve<FSMRegistrar>();
            configuratorsManager.RunConfigurator(ContextConstants.StartContext, Container);
            
            var stateMachine = new StateMachine(
                Container.ResolveAll<IInitSystem>(),
                Container.ResolveAll<IPostInitSystem>(),
                fsmRegistrator.GetInfo());
            
            stateMachine.AddState(new DefaultState((int)FSMStage.StartInitStage));
            stateMachine.SyncRun();
        }
    }
}