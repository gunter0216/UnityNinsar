using App.Common.FSM.External;
using Zenject;

namespace App.Core.Startups.External
{
    /// <summary>
    /// Базовый класс для всех конфигураторов
    /// </summary>
    public abstract class Configurator
    {
        private DiContainer m_Container;
        private FSMRegistrar m_FsmRegistrar;

        protected DiContainer Container => m_Container;
        protected FSMRegistrar FsmRegistrar => m_FsmRegistrar;

        public void SetDiContainer(DiContainer container)
        {
            m_Container = container;
        }
        
        public void SetFSMRegistrator(FSMRegistrar fsmRegistrar)
        {
            m_FsmRegistrar = fsmRegistrar;
        }

        public abstract void Configuration();
    }
}