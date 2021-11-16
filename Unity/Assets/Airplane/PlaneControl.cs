using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class PlaneControl : MonoBehaviour
{
	[HideInInspector] public bool isTxStarted = false;

	[SerializeField] string IP = "192.168.1.84"; // local host
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

	//the throttle  
	public float throttle = 1f;

    // affected by pitch
    public float airSpeed = 1f;

	// Start is called before the first frame update
	void Start()
	{
		gameObject.tag = "Plane";
		//get this game object's Transform  
		goTransform = this.GetComponent<Transform>();
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
        if (throttle >= airSpeed) {
            acceleration = 0.004f*(throttle - airSpeed);
        } else {
            acceleration = 0.0001f*(1/(throttle - airSpeed));
        }
  
        airSpeed += acceleration;

        // clamp from throttle only
        airSpeed = Mathf.Clamp(airSpeed, 0.08f, 2f);  

        // clamp with pitch gravity
        airSpeed -= goTransform.forward.y * Time.deltaTime * 1f;
        airSpeed = Mathf.Clamp(airSpeed, 0.08f, 2.5f);

		//translates the game object based on the throttle  
		goTransform.Translate(airSpeed * Vector3.forward);

		//rotates the game object, based on horizontal input  
        goTransform.Rotate(Vector3.up * Input.GetAxis("Horizontal"));
		goTransform.Rotate(Vector3.right * Input.GetAxis("Vertical"));
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
				print(text);
				setText(text);
			}
			catch (Exception err)
			{
				print(err.ToString());
			}
		}
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