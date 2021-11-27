# Aeroplay :airplane:
A fun household game where you use a 3D printed plane + Gesture + Voice controls to fly a virtual plane!

Checkout the online leaderboard [here](https://www.aeroplay.online/)! 

## Tech Stack Used
- [Unity](https://unity.com/products/unity-student) for game development
- [BerryIMU](https://ozzmaker.com/product/berryimu-accelerometer-gyroscope-magnetometer-barometricaltitude-sensor/) for Gyroscope and Accelerometer
- [Tensorflow](https://github.com/tensorflow/tensorflow) & [Mediapipe](https://github.com/google/mediapipe) for Gesture Recognition
- [GCP](https://cloud.google.com/speech-to-text) for Voice/Speech Recognition
- [React](https://reactjs.org/), [Express](https://expressjs.com/), [MySQL](https://www.mysql.com/) for Web App

## How can I run this on my local machine? 
Simply clone this repo and follow the setup below!
``` 
git clone https://github.com/180D-FW-2021/Team3.git 
```


# Setup
## Unity

1. Download and Installl Unity Hub, get the [student version]((https://unity.com/products/unity-student)) if possible
2. Inside Unity Hub -> Install Unity 2019.4.32f1 (LTS)
3. In the Projects Tab -> Add -> Team3/Unity Directory
In Unity -> 
4. Drag the `Main Scene` under the Assets/Scenes folder in the Project tab into the Hierarchy
5. Drag the `Menu Scene` under the Assets/Scenes folder in the Project tab into the Hierarchy
   


## IMU Plane Control
1. Go to the Controls Directory
```
cd Controls
```
2. Run the game on Unity and get the IP Address from the Unity console

3. Run 'ourIMU.py' file with the IP Address from part 2 as an argument
```
python2 ourIMU.py 'IP Address'
```

## Gesture Recognition
1. Install the prerequisites
```
pip3 install mediapipe 
pip3 install --upgrade tensorflow    
cd Hand_Gesture_Recognization/HandGesture 
```
2. Add your IP Address in `hgr.py` and `IP Script (Hierarchy)`
```
vim hgr.py -> Add your IP Address in line 31 (udpIP="")
In Unity Hierarchy -> Select Plane -> Add your IP Address to the IP Script (Inspector)

python3 hgr.py 
```


## Speech Recognition
1. In the Project Settings menu, change Player -> Configuration -> API Compatibility Level to **.NET 4.x**. 
2. Follow step 1 of Google's [Cloud Speech-to-Text Quickstart Guide](https://cloud.google.com/speech-to-text/docs/quickstart-client-libraries#before-you-begin) to:
    1. Set up a GCP Console project.
    2. Enable the Speech-to-Text API for your project.
    3. Create a service account.
    4. Download your service account's private key as a JSON file.
3. Rename your private key JSON file to `gcp_credentials.json`.
4. Place your `gcp_credentials.json` file in the `Assets/StreamingAssets` folder in your Unity project.


# Controls
IMU
- Roll Physical plane up to 90° to turn left and right
- Pitch Physical plane to go up or down
- Boost the Physical plane quickly forward for a short in-game speed boost

Gesture Recognition
- Thumbs up to increase throttle
- Thumbs down to decrease throttle
- Open palm to shoot projectile

Voice Commands
> "Start"

> "Pause"

> "Play"

> "Pause"

> "Resume"

> "Restart"

> "Quit"


Keyboard Commands
- W: Pitch up
- A: Roll Left
- S: Pitch Down
- D: Roll Right 
- E: Throttle Increase
- Q: Throttle Decrease

## Contributors
- [Leeksun Cho]()
- [Moaddat Naqvi](https://github.com/mznaqvi)
- [Kevin Wiranata](https://github.com/kevinwiranata)
- [Eric Zhang](https://github.com/Ericzklm)
