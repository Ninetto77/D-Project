using UnityEngine;
using Zenject;

namespace DProjectMirror
{
    public class ServerInstaller : MonoInstaller
    {
        [SerializeField] private Server server_;
		public override void InstallBindings()
        {
            Container
                .Bind<Server>()
                .FromInstance(server_)
                .AsSingle()
                .NonLazy();
        }
    }
}