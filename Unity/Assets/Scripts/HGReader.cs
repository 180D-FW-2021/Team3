using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;

public class HGReader : MonoBehaviour
{
    [HideInInspector] public bool isTxStarted = false;

	[SerializeField] string IP = "0.0.0.0";
	[SerializeField] int rxPort = 8000;
	[SerializeField] int txPort = 8001;

	private UdpClient client;
	private IPEndPoint remoteEndPoint;
	private Thread receiveThread;
	private string gesture = "";

    void Awake()
	{
		remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), txPort);
		client = new UdpClient(rxPort);
		receiveThread = new Thread(new ThreadStart(GetData));
		receiveThread.IsBackground = true;
		receiveThread.Start();
		Debug.Log("UDP Comms Initialised");
	}

    private void GetData()
	{
		while (true)
		{
			try
			{
				IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
				byte[] data = client.Receive(ref anyIP);
				string text = Encoding.UTF8.GetString(data);
				SetGesture(text);
			}
			catch (Exception err)
			{
				print(err.ToString());
			}
		}
	}

    public void SetGesture(string text)
	{
		gesture = text;
	}

    public string GetGesture()
    {
        return gesture;
    }

    void OnDisable()
    {
        if (receiveThread != null)
			receiveThread.Abort();
		client.Close();
    }
}
