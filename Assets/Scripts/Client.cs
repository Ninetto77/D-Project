using Mirror;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DProjectMirror
{
	public class Client : NetworkBehaviour
	{
		[Inject]
		private HelloServer server_;

		// Только для теста: создаем текст для дебага билда
		[SerializeField] private Text debug_;

		/// <summary>
		/// Функция активации клиента.
		/// </summary>
		public override void OnStartClient()
		{
			base.OnStartClient();
			if (NetworkClient.active && isLocalPlayer)
			{
				// Регистрируем обработчик сообщений на получение HelloMessage (ответ от сервера)
				NetworkClient.RegisterHandler<HelloMessage>(OnHelloMessageReceived);
				CmdSubscribe();
			}
		}

		/// <summary>
		/// Функция ответ от сервера на подписку клиента.
		/// </summary>
		/// <param name="msg"> Тип подписки. </param>
		private void OnHelloMessageReceived(HelloMessage msg)
		{
			SetText($"Клиент получил сообщение: {msg.msg}");
		}

		/// <summary>
		/// Клиент отправляет запрос на подключение подписки на сообщение конкретного типа.
		/// </summary>
		[Command]
		private void CmdSubscribe()
		{
			server_.Subscribe(connectionToClient);
		}

		/// <summary>
		/// Изменить текст
		/// </summary>
		/// <param name="s">Текст, которыйнадовывести на экран</param>
		private void SetText(string s)
		{
			debug_.text = s;
		}

		/// <summary>
		/// Функция остановки клиента.
		/// </summary>
		public override void OnStopClient()
		{
			base.OnStopClient();
			NetworkClient.UnregisterHandler<HelloMessage>();
		}
	}
}