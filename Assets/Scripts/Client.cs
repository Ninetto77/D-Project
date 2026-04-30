using Mirror;
using System;
using UnityEngine;
using Zenject;

namespace DProjectMirror
{
	public struct HelloMessage: NetworkMessage
	{
		public string msg;
	}

	public class Client : NetworkBehaviour
	{

		[Inject]
		private Server server_;

		public override void OnStartLocalPlayer()
		{
			Debug.Log("OnStartLocalPlayer");

			if (isLocalPlayer)
			{
				// Регистрируем обработчик сообщений на получение HelloMessage (ответ от сервера)
				NetworkClient.RegisterHandler<HelloMessage>(OnHelloMessageReceived);
				Subscribe();
			}
		}
		
		/// <summary>
		/// Функция ответ от сервера на подписку клиента.
		/// </summary>
		/// <param name="msg"> Тип подписки. </param>
		private void OnHelloMessageReceived(HelloMessage msg)
		{
			Debug.Log($"Клиент получил сообщение: {msg.msg}");
		}

		/// <summary>
		/// Клиент отправляет запрос на подключение подписки на сообщение конкретного типа.
		/// </summary>
		[Command]
		void Subscribe()
		{
			if (server_)
			{
				Debug.Log("server is ok");				
				server_.CmdSubscribe(connectionToClient);
			}
		}
	}
}