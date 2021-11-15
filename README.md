# Aeroplay :airplane:
Fun household game where you use a 3D printed plane + Gesture + Voice controls to fly a virtual plane!

Checkout the online leaderboard at [here](https://www.aeroplay.online/)! 

## Tech Stack Used
- [Unity](https://unity.com/products/unity-student) for game development
- [BerryIMU](https://ozzmaker.com/product/berryimu-accelerometer-gyroscope-magnetometer-barometricaltitude-sensor/) for Gyroscope and Accelerometer
- [Tensorflow](https://github.com/tensorflow/tensorflow) & [Mediapipe](https://github.com/google/mediapipe) for Gesture Recognition
- [GCP](https://cloud.google.com/speech-to-text) for Voice/Speech Recognition
- [React](https://reactjs.org/), [Expres](https://expressjs.com/), [MySQL](https://www.mysql.com/) for Web App

## How can I run this on my local machine? 
Simply clone this repo and run it!

1. Unity
```
Download and Installl Unity Hub, get student ver if possible
Inside Unity Hub -> Install Unity 2019.4.32f1 (LTS)
In the Projects Tab -> Add -> Team3/Unity Directory
```

2. Server
``` 
git clone https://github.com/180D-FW-2021/Team3.git
cd Team 3
npm start
```

3. Client (in new terminal)
``` 
cd Team3/client/src
npm start
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
