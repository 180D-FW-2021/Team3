# Aeroplay Unity
### High-Level Overview

This Unity App is the foundation of the entire game, where all the IMU, gesture and voice controls will pool into to control a virtual plane. 

### Gameplay Rules
- Once the player is in the `Main Scene`, the game will officially start when either an IMU has been detected and sucesfully connected, or when the player enables `keyboard mode`.
- Each player has 3 minutes once the plane starts moving.
- Colliding with any terrain (islands, water) will result in death and an instant respawn at the origin. The player will incur a 20-second time penalty.
- There are 5 different colored balloons, each with different sizes from largest to smallest. These balloons are worth: 
  - 1 Point (Orange), 2 Points (Red), 3 Points (Purple), 5 Points (Lavender), 10 Points (Blue)
- You can score a balloon either by shooting it or colliding with it.
- When the 3-minute timer reaches 0, the game will automatically move to the `End Scene` where you can put your username for upload. Simply press **Enter** to confirm. 

### Scenes
Currently, we have 3 scenes (more will be added throughout Winter Quarter). Those scenes are `Menu Sceen`,  `Main Scene`,  `End Scene`. As outlined in the general README.md of this project, the player should only drag the `Menu Sceen` into the hierarchy when playing the game. The flow of these scenes are simple as it is simply a cyclic pattern: <br>
 - `Menu Sceen` -> `Main Scene` -> `End Scene` -> `Menu Sceen`  -> .....

The `Menu Scene` is voice-enabled and the player can say *Start* to start the game, or hover and click the Start Button. This will bring you to the Main Scene.<br>
The `Main Scene` is also voice-enabled and some voice commands enabled include: *Pause*, *Resume*, *Keyboard Mode*, *Quit*... This is where the fun begins and where the player actually flies the plane based on the IMU, gesture and voice controls. There is a 3-minute timer everytime the game starts and points will be awarded until the timer runs out, where the game will automatically jump to the End Scene. <br>
The `End Scene` is the ending of the game where you are presented with your game stats, as well as an input field to put your username. After inputting your username, pressing **Enter** on the keyboard will send a POST request to our backend server and you should see your username and statistics on our [Website](aeroplay.online)!


### Terrain 
The current map for the game is an area of 6 individual islands, surrounded by water. The game map itself is 2000 x 2000 units, giving an area of 4000000 units<sup>2</sup>. For comparison, the model airplane is 10 x 3 units and can fly up to max speed of 2 units/frame.

The individual islands are firstly taken from a free [Island Asset](https://assetstore.unity.com/packages/3d/environments/landscapes/free-island-collection-104753) on the Unity Asset Store. The islands are then individually sized, placed and modified with the [Unity Terrain Tools Package](https://docs.unity3d.com/Manual/terrain-Tools.html). Then, [Water/Oceans](https://assetstore.unity.com/packages/vfx/shaders/nvjob-water-shaders-v2-x-149916) are added including the water shaders, specular reflections, shadows and the ocean movement/rotation speed. 

### Plane Controls
`PlaneControls.cs` is the main file in the program which controls all aspects of the plane movement. Here, when the `Main Scene` is loaded, we automatically start two threads: one to recieve data from the IMU, and the other to recieve data from Tensorflow/Mediapipe for gesture recognition. Voice Recognition uses a [plugin](https://github.com/oshoham/UnityGoogleStreamingSpeechToText) so no additional thread is needed. 

We decided to use TCP instead of MQTT to route the data streaming from the IMU to our `PlaneControls.cs` as TCP allowed for 100+ Packets/second while MQTT only allowed for 7 Packets/second which was nowhere near precise enough for a smooth ride as controls would often be extremly janky.

Although there is some physics in the form of simple kinematics that govern the movement of the plane, we decided against using a full simulation model airplane such as [this](https://github.com/gasgiant/Aircraft-Physics) as the controls are way too complex, and each individual surface needs to be modeled by a fluid/kinematic equation with individual drag, lift slopes, frictions, etc...  <br>

We did however implement acceleration and deceleration based on the current airspeed, throttle input and Angle of Attack (AoA, basically if you're diving or climbing). We also attached a `RigidBody` and a `BoxCollider` to the plane to detect terrain, water and balloon collisions. 


### Future Improvements:
We are in the process of introducing multiple maps. The current additional map features a cartoonish-theme where almost all of the objects are low-poly to provide an arcade-like feel. We will have to modify or create another scene so that the player can select which map they want. 

We will also introduce other features such as a sound design, including background music and SFXs for shooting, explosions and engine noise. Another future improvement currently in the working is moving objects, such as moving balloons which are harder to hit, as well as moving obstacles like planes which the player will have to avoid. 
