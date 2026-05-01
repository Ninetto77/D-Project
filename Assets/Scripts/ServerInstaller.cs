using UnityEngine;
using Zenject;

namespace DProjectMirror
{
    public class ServerInstaller : MonoInstaller
    {
        [SerializeField] private HelloServer server_;
		public override void InstallBindings()
        {
            Container
                .Bind<HelloServer>()
                .FromInstance(server_)
                .AsSingle()
                .NonLazy();
        }
    }
}