using Mirror;
using System;
using UnityEngine;
using UnityEngine.UI;
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

		// Только для теста: создаем текст для дебага билда
		[SerializeField] private Text debug_;

		/// <summary>
		/// Функция активации клиента.
		/// </summary>
		public override void OnStartClient()
		{
			base.OnStartClient();
			Debug.Log("OnStartClient");
			if (NetworkClient.active && isLocalPlayer)
			{
				Debug.Log("active");
				// Регистрируем обработчик сообщений на получение HelloMessage (ответ от сервера)
				CmdSubscribe();
				NetworkClient.RegisterHandler<HelloMessage>(OnHelloMessageReceived);
			}
		}

		/// <summary>
		/// Функция остановки клиента.
		/// </summary>
		public override void OnStopClient()
		{
			base.OnStopClient();
			NetworkClient.UnregisterHandler<HelloMessage>();
		}

		/// <summary>
		/// Функция ответ от сервера на подписку клиента.
		/// </summary>
		/// <param name="msg"> Тип подписки. </param>
		private void OnHelloMessageReceived(HelloMessage msg)
		{
			SetText($"Клиент получил сообщение: {msg.msg}");
			Debug.Log($"Клиент получил сообщение: {msg.msg}");
		}

		/// <summary>
		/// Клиент отправляет запрос на подключение подписки на сообщение конкретного типа.
		/// </summary>
		//[Command]
		private void CmdSubscribe()
		{
			Debug.Log("server ");
			//if (server_)
			//{
			//	Debug.Log("server is ok");				
			//	server_.CmdSubscribe(connectionToClient);
			//}
		}

		private void SetText(string s)
		{
			debug_.text = s;
		}
	}
}