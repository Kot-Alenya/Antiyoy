using CodeBase.Infrastructure.Project.Services.StateMachine.Factory;
using Zenject;

namespace CodeBase.Infrastructure.Project.Services.StateMachine
{
    public class StateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
            Container.Bind<IStateMachine>().To<StateMachine>().AsSingle();
        }
    }
}