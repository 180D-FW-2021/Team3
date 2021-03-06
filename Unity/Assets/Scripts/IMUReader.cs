using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class IMUReader : MonoBehaviour
{
    public float roll;
    public float pitch;

    public int boostCount = 0;
	public int imuControl = 0;
	public int imuDataReceived = 0;

    private TcpListener tcpListener;
    private TcpClient tcpClient;
    private Thread IMUThread;

    public GameObject TimerObject;
	public CountdownTimer TimerInstance;

    public PlaneControl PlaneControlInstance;
    public GameObject ControllerConnectScreen;

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
        TimerInstance = TimerObject.GetComponent<CountdownTimer>();
        PlaneControlInstance = this.GetComponent<PlaneControl>();
        IMUThread = new Thread(new ThreadStart(ReadIMU));
		IMUThread.IsBackground = true;
		IMUThread.Start();
    }

    public void ReadIMU()
	{
		IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
		IPAddress ipAddr = ipHost.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
		String[] imuData;
        try 
        {
            tcpListener = new TcpListener(ipAddr, 8081);
            tcpListener.Start();
            Debug.Log("Server Listening at " + ipAddr.MapToIPv4().ToString());
            imuControl = 1;
            Byte[] bytes = new Byte[1024];
            while (true) {
                using (tcpClient = tcpListener.AcceptTcpClient())
                {
                    using (NetworkStream stream = tcpClient.GetStream())
                    {
                        int length;
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            var incomingData = new byte[length];
                            Array.Copy(bytes, 0, incomingData, 0, length);
                            string serverMessage = Encoding.ASCII.GetString(incomingData);
                            imuDataReceived = 1;
                            imuData = serverMessage.Split(';');
                            foreach (var Reading in imuData)
                            {
                                if (!String.IsNullOrEmpty(Reading))
                                {
                                    String[] IMUValues = Reading.Split(',');
                                    try
                                    {
                                        roll = -1 * float.Parse(IMUValues[0]) / 4; //* 60;
                                        pitch = -1 * float.Parse(IMUValues[1]) / 4; //* 60;
                                        if (IMUValues[2][0] == '1')
                                        {
                                            this.boostCount++;
                                            PlaneControlInstance.boost = 10f;
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Debug.Log(IMUValues);
                                    }
                                }
                            }
                            if (TimerInstance.timeLeft <= 0)
                            {
                                break;
                            }
                        }
                        if (TimerInstance.timeLeft <= 0)
                        {
                            break;
                        }
                    }
                }
            }
        }
        catch (SocketException SocketException)
        {
            Debug.Log("Socket Exception: " + SocketException);
        }
	}
}
