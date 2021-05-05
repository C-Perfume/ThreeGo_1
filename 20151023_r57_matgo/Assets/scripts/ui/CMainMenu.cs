using UnityEngine;
using System.Collections;
using FreeNet;

public class CMainMenu : MonoBehaviour, IMessageReceiver {

    public void enter()
    {
        CNetworkManager.Instance.message_receiver = this;

		CPacket msg = CPacket.create((short)PROTOCOL.READY_TO_START);
		CNetworkManager.Instance.send(msg);
    }


	void IMessageReceiver.on_recv(CPacket msg)
	{
	}
}
