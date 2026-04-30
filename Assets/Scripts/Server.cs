using Mirror;
using UnityEngine;

namespace DProjectMirror
{
    public class Server : NetworkManager
    {
		HelloMessage helloMsg = new HelloMessage { msg = "Hello Client" };

		public override void OnStartServer()
		{
			base.OnStartServer();
			Debug.Log($"Сервер подключился.");
			//NetworkServer.RegisterHandler<HelloMessage>(); //указываем, какой struct должен прийти на сервер, чтобы выполнился свапн
		}

		/// <summary>
		/// При подключении клиента к серверу. 
		/// </summary>
		public override void OnServerConnect(NetworkConnectionToClient conn)
		{
			base.OnServerConnect(conn);
			Debug.Log($"Клиент {conn.connectionId} подключился.");
		}

		/// <summary>
		/// Функция подключения клиента к определенной подписке.
		/// Выполняется только на сервере.
		/// </summary>
		/// <param name="conn">Тип соединения с клиентом</param>
		/// <param name="type">Тип подписки</param>
		[Server]
		public void CmdSubscribe(NetworkConnectionToClient conn)
		{
			if (conn != null && conn.isReady)
			{
				Debug.Log($"Клиент {conn.connectionId} подключился к подписке HelloMessage.");
				Debug.Log($"Отправляет клиенту ответ.");
				conn.Send(helloMsg);
			}

		}

		///// <summary>
		///// Широковещательное оповещение клиентам, подключенным
		///// к конкретной подписке.
		///// </summary>
		///// <param name="type"> Тип подписки. </param>
		//[Server]
		//public void BroadcastToClients(TypeOfSubscribe type)
		//{
		//	if (subscribes_.IsEmpty())
		//	{
		//		return;
		//	}

		//	if (subscribes_.ContainsKey(type))
		//	{
		//		switch (type)
		//		{
		//			case TypeOfSubscribe.hello:
		//				foreach (var clients in subscribes_[type])
		//					if (clients != null && clients.isReady)
		//						clients.Send(helloMsg);
		//				break;
		//			case TypeOfSubscribe.bye:
		//				foreach (var clients in subscribes_[type])
		//					if (clients != null && clients.isReady)
		//						clients.Send(buyMsg);
		//				break;
		//			default:
		//				break;
		//		}
		//	}
		//}

	}
}