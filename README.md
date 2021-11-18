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
Simply clone this repo and run it!

1. Unity
```
Download and Installl Unity Hub, get student ver if possible
Inside Unity Hub -> Install Unity 2019.4.32f1 (LTS)
In the Projects Tab -> Add -> Team3/Unity Directory
In Unity -> Drag the "Main Scene" under the Assets/Scenes folder in the Project tab into the Hierarchy
```

2. Gesture Recognition
```
pip3 install mediapipe 
pip3 install --upgrade tensorflow    
cd Hand_Gesture_Recognization/HandGesture 
python3 hgr.py 
```

## Controls
IMU
- Roll Physical plane up to 90° to turn left and right
- Pitch Physical plane to go up or down
- Boost the Physical plane quickly forward for a short in-game speed boost

Gesture Recognition
- Thumbs up to increase throttle
- Thumbs down to decrease throttle
- Open palm to shoot projectile

Voice Commands
> "Start Game"

> "Pause Game"

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
