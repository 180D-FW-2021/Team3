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

	public GameObject TimerObject;
	public CountdownTimer TimerInstance;

	public IMUReader IMUReaderInstance;

	public AudioSource audio;
	public GameObject pauseScreen;

	//The game object's Transform  
	private Transform goTransform;

	//the throttle increment to the current velocity  
	private float increment = 0.0f;
	//this variable stores the vertical axis values  
	private float vertAxis = 0.0f;
	private float acceleration = 0f;
	private float lastShot = 0f;
	public float shotCooldown = 350f;

	public float roll;
	public float pitch;

	//the throttle  
	public float throttle = 1f;
	public float boost = 0;
	public int boostFrames = 0;

	// affected by pitch
	public float airSpeed = 1f;

	public int boostCount = 0;
	public int imuControl = 0;
	public int imuDataReceived = 0;

	// Start is called before the first frame update
	void Start()
	{
		gameObject.tag = "Plane";
		//get this game object's Transform  
		goTransform = this.GetComponent<Transform>();
		TimerInstance = TimerObject.GetComponent<CountdownTimer>();
		IMUReaderInstance = this.GetComponent<IMUReader>();
		audio = GetComponent<AudioSource>();
		Gameplay.keyboardMode = false;
		Gameplay.gameStarted = false;
		Gameplay.pauseGame();
		//await Task.Run(() => ReadIMU());
	}


	// Update is called once per frame
	void Update()
	{
		roll = IMUReaderInstance.roll;
		pitch = IMUReaderInstance.pitch;
		boostCount = IMUReaderInstance.boostCount;
		imuControl = IMUReaderInstance.imuControl;
		imuDataReceived = IMUReaderInstance.imuDataReceived;

		if (Input.GetKeyDown("k"))
		{
			Gameplay.keyboardMode = true;
		}

		if (imuDataReceived == 1 || Gameplay.keyboardMode)
		{
			GameObject controllerConnectScreen = GameObject.Find("ControllerConnectScreen");
			if (controllerConnectScreen)
			{
				Gameplay.gameStarted = true;
				Gameplay.resumeGame();
				controllerConnectScreen.SetActive(false);
			}
		 	//GameObject.Find("ControllerConnectScreen").SetActive(false);
		}
		else
		{
			Gameplay.pauseGame();
		}

		if (boost != 0) 
		{
			boostFrames++;
		}
		if (boostFrames > 50)
		{
			boostFrames = 0;
			boost = 0;
		}


		if (Gameplay.isPaused && Gameplay.gameStarted)
		{
			pauseScreen.SetActive(true);
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Gameplay.resumeGame();
				pauseScreen.SetActive(false);
			}
			return;
		}
		else {
		 	pauseScreen.SetActive(false);
		}

		if (Input.GetButtonDown("Fire1") && !Gameplay.isPaused)
	 	{
	 		TakeShot();
	 	}

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
		else if (getText() == "shoot")
		{
			if (!Gameplay.isPaused)
			{
				TakeShot();
				//Shooter.instance.Shoot();
			}
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

		audio.volume = throttle / 1f * .03f;

		if (imuDataReceived == 1)
		{
			goTransform.Translate(airSpeed * Vector3.forward);
			if (boostFrames > 0)
			{
				goTransform.Translate(boost * Vector3.forward);
			}
			goTransform.Translate(airSpeed * Vector3.right * pitch / 90);
			transform.rotation = Quaternion.Euler(-1 * roll, goTransform.eulerAngles.y + pitch / 45, -1 * pitch);
		}
		else if (Gameplay.keyboardMode)
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				if (!Gameplay.isPaused)
				{
					Gameplay.pauseGame();
				}
			}
			if (Input.GetKeyDown("space"))
			{
				boost = 10f;
			}
			if (boostFrames > 0)
			{
				goTransform.Translate(boost * Vector3.forward);
			}
			goTransform.Translate(airSpeed * Vector3.forward);
			goTransform.Rotate(Vector3.up * Input.GetAxis("Horizontal"));
			goTransform.Rotate(Vector3.right * Input.GetAxis("Vertical"));
		}
		else
		{
			TimerInstance.timeLeft = 180;
		}
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

	private void TakeShot()
	{
		float currentTime = Time.time * 1000;
		if (currentTime - lastShot > shotCooldown)
		{
			Shooter.instance.Shoot();
			lastShot = currentTime;
		}
	}

	//Prevent crashes - close clients and threads properly!
	void OnDisable()
	{
		PlayerPrefs.SetInt("boosts_used", boostCount);
		if (receiveThread != null)
			receiveThread.Abort();

		client.Close();
	}

}