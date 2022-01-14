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
	// connect IMU and HGR modules
	private IMUReader imuReaderInstance;
	private HGReader hgReaderInstance;

	// modules that are interacted with
	public GameObject timerObject;
	private CountdownTimer timerInstance;
	public AudioSource engineAudio;
	public GameObject pauseScreen;
	public GameObject controllerConnectScreen;

	// the plane transform object that we will be controlling
	private Transform planeTransform;

	// values reported by IMU Reader
	public float roll;
	public float pitch;
	public int boostCount = 0;
	public int imuControl = 0;
	public int imuDataReceived = 0;

	public float throttle = 1f;
	public float airSpeed = 1f;

	private float lastShot = 0f;

	public float boost = 0f;
	public float airSpeedFromBoost = 0f;
	private float boostStartTime = 0f;
	private bool inBoost = false;

	public ThrottleBounds throttleBounds;
	public AccelerationBounds accelerationBounds;
	public SpeedBounds speedBounds;

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// get and set values to get into starting state
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void Start()
	{
		gameObject.tag = "Plane";
		SetMovementBounds();
		InitializeComponents();
		SetStartingState();
	}
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// functions that get and set values
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	private void SetMovementBounds()
	{
		throttleBounds.minThrottle = Gameplay.minThrottle;
		throttleBounds.maxThrottle = Gameplay.maxThrottle;
		accelerationBounds.acceleration = Gameplay.acceleration;
		accelerationBounds.deceleration = Gameplay.deceleration;
		speedBounds.minSpeed = Gameplay.minSpeed;
		speedBounds.maxSpeed = Gameplay.maxSpeed;
	}
	private void InitializeComponents()
	{
		planeTransform = this.GetComponent<Transform>();
		timerInstance = timerObject.GetComponent<CountdownTimer>();
		imuReaderInstance = this.GetComponent<IMUReader>();
		hgReaderInstance = this.GetComponent<HGReader>();
		engineAudio = gameObject.GetComponent<AudioSource>();
	}
	private void SetStartingState()
	{
		Gameplay.keyboardMode = false;
		Gameplay.gameStarted = false;
		Gameplay.pauseGame();
	}
	private void FetchIMUValues()
	{
		roll = imuReaderInstance.roll;
		pitch = imuReaderInstance.pitch;
		boostCount = imuReaderInstance.boostCount;
		imuControl = imuReaderInstance.imuControl;
		imuDataReceived = imuReaderInstance.imuDataReceived;
	}
	private float GetEngineVolumeMultiplierFromSettings()
	{
		return Gameplay.engineVolume / 100f;
	}
	private void SetEngineVolume()
	{
		engineAudio.volume = throttle * Gameplay.engineVolumeNormalizerValue * GetEngineVolumeMultiplierFromSettings();
	}
	private void SetKeyboardMode()
	{
		Gameplay.keyboardMode = true;
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// functions that check state
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	private bool UserGainedControl()
	{
		return (ControllerConnected() || Gameplay.keyboardMode);
	}
	private bool PausedBeforeGameStarted()
	{
		return (Gameplay.isPaused && !Gameplay.gameStarted && !UserGainedControl());
	}
	private bool UserPausedGame()
	{
		return (Gameplay.isPaused && Gameplay.gameStarted);
	}
	private bool ControllerConnected()
	{
		return (imuDataReceived == 1);
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// functions that transition state
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	private bool StateTransition()
	{
		if (UserGainedControl())
		{
			StartGame();
		}
		else
		{
			SetGamePausedState();
		}

		if (UserPausedGame())
		{
			PauseGame();
			return false;
		}
		else if (PausedBeforeGameStarted())
		{
			HidePauseScreen();
			return false;
		}
		else
		{
			HidePauseScreen();
			return true;
		}
	}
	private void StartGame()
	{
		if (controllerConnectScreen.active)
		{
			Gameplay.gameStarted = true;
			SetGameResumedState();
			controllerConnectScreen.SetActive(false);
		}
	}
	private void PauseGame()
	{
		ShowPauseScreen();
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			ResumeGame();
		}
	}
	private void SetGamePausedState()
	{
		Gameplay.pauseGame();
	}
	private void ShowPauseScreen()
	{
		pauseScreen.SetActive(true);
	}
	private void HidePauseScreen()
	{
		pauseScreen.SetActive(false);
	}
	private void ResumeGame()
	{
		SetGameResumedState();
		HidePauseScreen();
	}
	private void SetGameResumedState()
	{
		Gameplay.resumeGame();
	}
	private void CheckForKeyboardMode()
	{
		if (Input.GetKeyDown("k"))
		{
			SetKeyboardMode();
		}
	}
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// hand gesture recognition
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	private void ReadGestures()
	{
		switch (hgReaderInstance.GetGesture())
		{
			case "thumbs up":
				throttle += Gameplay.hgrThrottleIncrement;
				break;
			case "thumbs down":
				throttle -= Gameplay.hgrThrottleIncrement;
				break;
			case "shoot":
				if (!Gameplay.isPaused)
				{
					TakeShot();
				}
				break;
			default:
				break;
		}
		hgReaderInstance.SetGesture("");
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// functions that handle plane movement physics
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	private void AdjustThrottleByKeyboard()
	{
		float verticalAxis = Input.GetAxis("Yaw") * Time.deltaTime;
		if (verticalAxis > 0)
		{
			throttle += Gameplay.keyboardThrottleIncrement;
		}
		else if (verticalAxis < 0)
		{
			throttle -= Gameplay.keyboardThrottleIncrement;
		}
	}
	private void ClampThrottleValue()
	{
		throttle = Mathf.Clamp(throttle, throttleBounds.minThrottle, throttleBounds.maxThrottle);
	}
	private void SetPlaneAirSpeed()
	{
		float accelerationValue = GetAccelerationValue();
		SetAirspeedFromThrottle(accelerationValue);
		SetAirspeedAfterGravity();
	}
	private float GetAccelerationValue()
	{
		if (throttle >= airSpeed)
		{
			return accelerationBounds.acceleration * (throttle - airSpeed);
		}
		else
		{
			return accelerationBounds.deceleration * (throttle - airSpeed);
		}
	}
	private void SetAirspeedFromThrottle(float accelerationValue)
	{
		airSpeed += accelerationValue;
		airSpeed = Mathf.Clamp(airSpeed, speedBounds.minSpeed, speedBounds.maxSpeed);
	}
	private void SetAirspeedAfterGravity()
	{
		airSpeed -= planeTransform.forward.y * Gameplay.gravityInfluenceMultiplier;
		airSpeed = Mathf.Clamp(airSpeed, speedBounds.minSpeed, speedBounds.maxSpeed);
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// boost detection
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	private void RegisterBoost()
	{
		if (boost != 0)
		{
			airSpeedFromBoost = Gameplay.boostSpeed;
			boostStartTime = Time.time * 1000;
			inBoost = true;
			boost = 0;
		}
		if (inBoost)
		{
			if (Time.time * 1000 - boostStartTime > Gameplay.boostDuration)
			{
				airSpeedFromBoost = 0;
				inBoost = false;
			}
		}
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// translate the plane in game
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	private void MovePlane()
	{
		if (ControllerConnected())
		{
			planeTransform.Translate((airSpeed + airSpeedFromBoost) * Vector3.forward);
			planeTransform.Translate((airSpeed + airSpeedFromBoost) * Vector3.right * pitch * Gameplay.imuCentripetalMultiplier);
			transform.rotation = Quaternion.Euler(-1 * roll, planeTransform.eulerAngles.y + pitch * Gameplay.imuTurnMultiplier, -1 * pitch);
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
			planeTransform.Translate((airSpeed + airSpeedFromBoost) * Vector3.forward * Time.deltaTime * 100f);
			planeTransform.Rotate(Vector3.up * Input.GetAxis("Horizontal"));
			planeTransform.Rotate(Vector3.right * Input.GetAxis("Vertical"));
		}
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// shoot a laser
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	private void CheckForShot()
	{
		if (UserFiredShot())
		{
			TakeShot();
		}
	}
	private bool UserFiredShot()
	{
		return (Input.GetButtonDown("Fire1") && !Gameplay.isPaused);
	}
	private void TakeShot()
	{
		float currentTime = Time.time * 1000;
		if (currentTime - lastShot > Gameplay.shotCooldown)
		{
			Shooter.instance.Shoot();
			lastShot = currentTime;
		}
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// manage states and plane movement on each frame
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void Update()
	{
		FetchIMUValues();
		SetEngineVolume();
		CheckForKeyboardMode();

		if (!StateTransition())
		{
			return;
		}

		CheckForShot();
		ReadGestures();
		AdjustThrottleByKeyboard();
		ClampThrottleValue();

		SetPlaneAirSpeed();
		RegisterBoost();
		MovePlane();
	}

	void OnDisable()
	{
		PlayerPrefs.SetInt("boosts_used", boostCount);
	}
}

[System.Serializable]
public class ThrottleBounds
{
    public float minThrottle;
    public float maxThrottle;
}

[System.Serializable]
public class AccelerationBounds
{
	public float acceleration;
	public float deceleration;
}

[System.Serializable]
public class SpeedBounds
{
	public float minSpeed;
	public float maxSpeed;
}