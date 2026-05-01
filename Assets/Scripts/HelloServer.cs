using Mirror;
using UnityEngine;
using Zenject;

namespace DProjectMirror
{
    public class HelloServer : NetworkManager
    {
		[Inject]
		private DiContainer container;

		HelloMessage helloMsg = new HelloMessage { msg = "Hello Client" };

		/// <summary>
		/// При активации сервера.
		/// </summary>
		public override void OnStartServer()
		{
			base.OnStartServer();
			Debug.Log($"Сервер подключился.");
		}

		/// <summary>
		/// При подключении клиента к серверу. 
		/// </summary>
		/// <param name="conn">Конкретный клиент</param>
		public override void OnServerConnect(NetworkConnectionToClient conn)
		{
			base.OnServerConnect(conn);
			GameObject client = container.InstantiatePrefab(playerPrefab);
			// метод автоматически даёт authority клиенту.
			NetworkServer.AddPlayerForConnection(conn, client);
		}

		/// <summary>
		/// При отключении от сервреа удаляем клиент.
		/// </summary>
		/// <param name="conn">Конкретный клиент</param>
		public override void OnServerDisconnect(NetworkConnectionToClient conn)
		{
			// Удаляем объект игрока
			if (conn.identity != null)
			{
				Destroy(conn.identity.gameObject);
			}
			base.OnServerDisconnect(conn);
		}

		/// <summary>
		/// Функция подключения клиента к определенной подписке.
		/// Выполняется только на сервере.
		/// </summary>
		/// <param name="conn">Тип соединения с клиентом</param>
		[Server]
		public void Subscribe(NetworkConnectionToClient conn)
		{
			if (conn != null && conn.isReady)
			{
				conn.Send(helloMsg);
			}
		}
	}
}