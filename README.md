# Aeroplay :airplane:
A fun household game where you use a 3D printed plane + Gesture + Voice controls to fly a virtual plane!

Checkout the [online leaderboard](https://www.aeroplay.online/) where you can also download our game! (MacOS + Windows) <br>

Please visit the [User Guide](https://docs.google.com/document/d/1YpPP1kQf3QHHyk7c0A6Lcdp2TIKk9Tfp9LNCFGn9VaA/edit?usp=sharing) for the game! (Readme below is slightly outdated)

Watch a v3.0 demo video of the game [here](https://youtu.be/Z3bomdumWlk)! [Winter, 2022 March] <br> Note: *This is the final version of our game and this video explains all the game features/mechanics in detail!*<br>

Watch a v2.0 demo video of the game [here](https://www.youtube.com/watch?v=2cv6WaeUb7c)! [Winter, 2022 Feb] <br>

Watch a v1.0 demo video of the game [here](https://www.youtube.com/watch?v=9XCapfStW8s)! [Fall, 2021 Dec]


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


# Development Setup
## Unity (Game Development Environment)

1. Download and Installl Unity Hub, get the [student version]((https://unity.com/products/unity-student)) if possible
2. Inside Unity Hub -> Install Unity 2019.4.32f1 (LTS)
3. In the Projects Tab -> Add -> Team3/Unity Directory
In Unity -> 
4. Drag the `Menu Scene` under the Assets/Scenes folder in the Project tab into the Hierarchy

## IMU Plane Control
1. ssh into Raspberry Pi
```
ssh pi@raspberrypi.local
```

1. Go to the Controls Directory
```
cd Controls
```
2. Run publisher executable

3. Run 'ourIMU.py' file on the Raspberry Pi
```
python3 ourIMU.py
```

## Gesture Recognition
1. Install the prerequisites (Use 3.6 <= Python <= 3.8 )
```
pip3 install mediapipe 
pip3 install --upgrade tensorflow    
cd Hand_Gesture_Recognition/HandGesture 
```
**NOTE: [If you're using an ARM M1 Mac**](https://gist.github.com/kevinwiranata/864682f6c1f195dbbc956b5497f178ff)
```
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
> "Start" / "Play"
- Starts the game when in Start Screen

> "Pause"
- Pauses the game while playing

> "Resume"
- Resume the game if the current game is paused

> "Restart"
- "Takes you back to the Start Screen if currently in-game"

> "Quit"
- "Quits the game"

> "Keyboard Mode"
- Activates Keyboard Mode for the game, will default to IMU if an IMU is detected
- Note: Game will not start until an IMU is detected, or until keyboard mode is activated


Keyboard Commands
- W: Pitch up
- A: Roll Left
- S: Pitch Down
- D: Roll Right 
- E: Throttle Increase
- Q: Throttle Decrease

## Contributors
- [Leeksun Cho](https://github.com/lcho0320)
- [Moaddat Naqvi](https://github.com/mznaqvi)
- [Kevin Wiranata](https://github.com/kevinwiranata)
- [Eric Zhang](https://github.com/Ericzklm)
