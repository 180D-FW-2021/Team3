using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;

public class PlaneControl : MonoBehaviour
{
	[HideInInspector] public bool isTxStarted = false;

	[SerializeField] string IP = "0.0.0.0"; // local host
	[SerializeField] int rxPort = 8000; // port to receive data from Python on
	[SerializeField] int txPort = 8001; // port to send data to Python on

	UdpClient client;
	IPEndPoint remoteEndPoint;
	Thread receiveThread; // Receiving Thread
	string ges = "";

	//The game object's Transform  
	private Transform goTransform;

	//the throttle increment to the current velocity  
	private float increment = 0.0f;
	//this variable stores the vertical axis values  
	private float vertAxis = 0.0f;
	private float acceleration = 0f;

	public float roll;
	public float pitch;

	//the throttle  
	public float throttle = 1f;
	public float boost = 1f;

	// affected by pitch
	public float airSpeed = 1f;

	public int boostCount = 0;

	// Start is called before the first frame update
	async void Start()
	{
		gameObject.tag = "Plane";
		//get this game object's Transform  
		goTransform = this.GetComponent<Transform>();
		await Task.Run(() => ReadIMU());
	}


	// Update is called once per frame
	void Update()
	{
		// Gesture Controls
		increment = 0.0f;
		if (getText() == "thumbs up")
		{
			increment = 0.1f;
			throttle = throttle + increment;
			setText("");
		}
		else if (getText() == "thumbs down")
		{
			increment = -0.1f;
			throttle = throttle + increment;
			setText("");
		}
		else if(getText() == "shoot")
		{
			Shooter.instance.Shoot();
			setText("");
		}
		else
		{
			setText("");
		}


		// Keyboard Controls
		vertAxis = Input.GetAxis("Yaw") * Time.deltaTime;
		if (vertAxis > 0)
		{
			increment = 0.01f;
		}
		else if (vertAxis < 0)
		{
			increment = -0.01f;
		}

		throttle += increment;

		//set the throttle limit between 0 and 2f (max speed)  
		throttle = Mathf.Clamp(throttle, 0f, 2f);

		// acceleration calculation 
		if (throttle >= airSpeed)
		{
			acceleration = 0.004f * (throttle - airSpeed);
		}
		else
		{
			acceleration = 0.0001f * (1 / (throttle - airSpeed));
		}

		airSpeed += acceleration;

		// clamp from throttle only
		airSpeed = Mathf.Clamp(airSpeed, 0.08f, 2f);

		// clamp with pitch gravity
		airSpeed -= goTransform.forward.y * Time.deltaTime * 1f;
		airSpeed = Mathf.Clamp(airSpeed, 0.08f, 2.5f);

		//translates the game object based on the throttle  
		goTransform.Translate(airSpeed * Vector3.forward);
		goTransform.Translate(airSpeed * Vector3.right * pitch / 90);

		//rotates the game object, based on horizontal input  
		//goTransform.Rotate(-Vector3.forward * Input.GetAxis("Horizontal"));
		//Debug.Log(Vector3.up.ToString() + " " + Input.GetAxis("Horizontal").ToString());
        
		//transform.rotation = Quaternion.Euler(45,90,90);
		//Debug.Log(goTransform.eulerAngles.y);
		transform.rotation = Quaternion.Euler(-1 * roll, goTransform.eulerAngles.y + pitch / 30, -1 * pitch);
		//goTransform.Rotate(Vector3.right * pitch / 90);
		//goTransform.Rotate(Vector3.up * Input.GetAxis("Horizontal")); //sideways
		//goTransform.Rotate(Vector3.right * Input.GetAxis("Vertical"));
		setText("none");
	}

	void Awake()
	{
		// Create remote endpoint (to Matlab) 
		remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), txPort);

		// Create local client
		client = new UdpClient(rxPort);

		// local endpoint define (where messages are received)
		// Create a new thread for reception of incoming messages
		receiveThread = new Thread(new ThreadStart(RecievedData));
		receiveThread.IsBackground = true;
		receiveThread.Start();

		// Initialize (seen in comments window)
		print("UDP Comms Initialised");


	}

	// Receive data, update packets received
	private void RecievedData()
	{
		while (true)
		{
			try
			{
				IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
				byte[] data = client.Receive(ref anyIP);
				string text = Encoding.UTF8.GetString(data);
				// print(">> " + text);
				ProcessInput(text);
				setText(text);
			}
			catch (Exception err)
			{
				print(err.ToString());
			}
		}
	}

	public void ReadIMU() 
	{
		IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddr = ipHost.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 8081);

		Socket listener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

		String[] IMUData;
		try 
		{
			listener.Bind(localEndPoint);
			listener.Listen(10);
			while (true) 
			{
				Debug.Log("Waiting for a connection... host:" + ipAddr.MapToIPv4().ToString());
				Socket clientSocket = listener.Accept();

				byte[] bytes = new Byte[1024];
				string data = null;

				while (true)
				{
					int numByte = clientSocket.Receive(bytes);
					data = Encoding.ASCII.GetString(bytes, 0, numByte);
					IMUData = data.Split(';');
					Debug.Log(data);
					foreach (var Reading in IMUData)
					{
						if (!String.IsNullOrEmpty(Reading))
						{
							//Debug.Log(Reading);
							String[] IMUValues = Reading.Split(',');
							roll = -1 * float.Parse(IMUValues[0]) / 4; //* 60;
							pitch = -1 * float.Parse(IMUValues[1]) / 4; //* 60;
							if (IMUValues[2] == "1")
							{
								this.boostCount++;
								this.airSpeed = 2f;
							}
						}
					}
					//Debug.Log(data);
				}
				clientSocket.Shutdown(SocketShutdown.Both);
				clientSocket.Close();
			}
		} 
		catch (Exception e)
		{
			Debug.Log(e.ToString());
		}
	}

	public void Service(CancellationToken token) {
		
	}

	void setText(string text)
	{
		ges = text;
	}

	string getText()
	{
		return ges;
	}


	private void ProcessInput(string input)
	{
		// PROCESS INPUT RECEIVED STRING HERE

		if (!isTxStarted) // First data arrived so tx started
		{
			isTxStarted = true;
		}
	}

	//Prevent crashes - close clients and threads properly!
	void OnDisable()
	{
		if (receiveThread != null)
			receiveThread.Abort();

		client.Close();
	}

}