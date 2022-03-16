# Aeroplay Unity
## **High-Level Overview**

This Unity App is the foundation of the entire game, where all the IMU, gesture and voice controls will pool into to control a virtual plane. We chose this game to run on `Unity 2019.4.32f1 (LTS)` as we had compatability issues with Unity 2020 on macOS.

## **Gameplay Rules**
- A player enters the game through the `Authentication` screen. They will need to either login or create a new account to be taken to the menu scene.
- The `Menu Scene` gives players some options: start the game, visit the leaderboard, go into settings, log out of their account, quit the game, or change the map they want to play on.
  - Starting the game will take the player to a loading screen, then onto the `Main Scene` or `LowPolyScene`.
  - Entering the settings takes the player to the `Settings Menu` scene.
  - Logging out will take the player back to the `Authentication` scene.
- Once the player is in the `Main Scene` or `LowPolyScene`, the game will officially start when either an IMU has been detected and sucessfully connected, or when the player enables `keyboard mode`.
- Each player has 3 minutes once the plane starts moving.
- Colliding with any terrain (islands, water) will result in an instant respawn at the origin. The player will incur a 20-second time penalty.
- There are 6 different colored balloons, each with different sizes from largest to smallest. These balloons are worth: 
  - 1 Point (Orange), 2 Points (Red), 3 Points (Purple), 5 Points (Lilac), 10 Points (Blue), 15 Points (Gold)
    - Gold balloons give an additional 15 seconds on the clock.
    - Gold balloons are stationary, but spawn in hard-to-get-to locations
- You can score a balloon either by shooting it or colliding with it.
  - Gold balloons cannot be shot down and can only be popped by colliding with it.
- When the 3-minute timer reaches 0, the game will automatically move to the `End Scene` where your score will automatically be uploaded to the web.
  - The player can then choose to play again, visit the leaderboard, return to the `Menu Scene`, or quit the game.

## **Scenes**
There are 6 scenes: `Authentication`, `Menu Sceen`, `Settings Menu`, `Main Scene`, `LowPolyScene`, and `End Scene`. The flow of these scenes is described in the Gameplay Rules section.

All options in all scenes are voice-enabled and can be activated when the player says the words that appear on screen. There are additional keywords added since different keywords may be more intuitive depending on the player. All valid keywords and their effects are in the user guide.

The `Authentication` scene allows users to sign in with a pre-existing account or create a new account. Voice commands include *Login*, *Signup*, and *Quit*.

The `Menu Scene` scene is the main menu and allows the users to access most of the other scenes. Voice commands match the text on screen. Users can also choose a map by saying *Realistic* or *Low Poly* or can toggle the current map with *Change Map*

The `Settings Menu` scene allows players to change the sounds and looks of the game. Voice commands for this scene are formed with a combination of the name of a setting + *increase*, *decrease*, or *toggle*

The `Main Scene` and `LowPolyScene` is also voice-enabled and some voice commands enabled include: *Pause*, *Resume*, *Keyboard Mode*, *Quit*... This is where the fun begins and where the player actually flies the plane based on the IMU, gesture and voice controls. There is a 3-minute timer everytime the game starts and points will be awarded until the timer runs out, where the game will automatically jump to the End Scene.

The `End Scene` is the ending of the game where you are presented with your game stats and you should see your username and statistics on our [Website](aeroplay.online)!


## **Terrain**
The realistic map for the game is an area of 6 individual islands, surrounded by water. The game map itself is 2000 x 2000 units, giving an area of 4000000 units<sup>2</sup>. For comparison, the model airplane is 10 x 3 units and can fly up to max speed of 2 units/frame.

The individual islands are firstly taken from a free [Island Asset](https://assetstore.unity.com/packages/3d/environments/landscapes/free-island-collection-104753) on the Unity Asset Store. The islands are then individually sized, placed and modified with the [Unity Terrain Tools Package](https://docs.unity3d.com/Manual/terrain-Tools.html). Then, [Water/Oceans](https://assetstore.unity.com/packages/vfx/shaders/nvjob-water-shaders-v2-x-149916) are added including the water shaders, specular reflections, shadows and the ocean movement/rotation speed. 

The low-poly map in contrast is much smaller and much denser, with more obstacles to avoid. The terrain consists of a single island in a large ocean with a large mountain at the center. This map features bridges, rocks, trees, and other fun obstacles. The addition of obstacles in this map means the addition of the gold balloons mentioned earlier as a fun challenge.

## **Audio**
We have added sound effects as well as background music. There are sound effects for clicking buttons, changing settings, shooting a laser, popping a balloon, flying, and etc. We also have created distinct soundtracks for each scene by creating a jukebox script that allows us to have multiple songs for each scene, where each can be played with a defined probability. The music is from various games and systems.

## **Images**
In the images section, we have the various components that make up the UI of the game, as well as the photoshop files used to create them.

## **Visuals**
Using the built-in Universal Rendering Pipeline from Unity, we have added post-processing effects to the low-poly scene to make the visuals brighter and more interesting. This includes colormapping, motion smoothing, depth of field, bloom, lens dirt, and a vignette.

## **Performance**
The game has to load a large map to fly over which is taxing on game performance. To fix this, we used and experimented with different techniques like texture reductions, LOD-culling, static objects, and lightmap baking. This makes it so that further away objects take up less resources and the game does not have to dynamically update the lighting.

# All Scripts
## **Plane Controls**
### PlaneControl.cs
`PlaneControls.cs` is the main file in the program which controls all aspects of the plane movement and game state. Here, when the `Main Scene` is loaded, we automatically bind to the `IMUReader` and `HGReader` scripts that starts two threads: one to recieve data from the IMU, and the other to recieve data from Tensorflow/Mediapipe for gesture recognition. Voice Recognition uses a [plugin](https://github.com/oshoham/UnityGoogleStreamingSpeechToText) so no additional thread is needed. This script also detects shooting and boosting and calls the appropriate handler.

Although there is some physics in the form of simple kinematics that govern the movement of the plane, we decided against using a full simulation model airplane such as [this](https://github.com/gasgiant/Aircraft-Physics) as the controls are way too complex, and each individual surface needs to be modeled by a fluid/kinematic equation with individual drag, lift slopes, frictions, etc... We implemented our own physics which is easier to understand and allows the game to be played by a wider audience.

We did however implement acceleration and deceleration based on the current airspeed, throttle input and Angle of Attack (AoA, basically if you're diving or climbing). We also attached a `RigidBody` and a `BoxCollider` to the plane to detect terrain, water and balloon collisions. 

This script also handles the game state transitions. The game starts off in the controller connect state and transitions to the in game phase. Depending on whether the user is connecting the controller or in the pause menu, the game will either be in the pause state or out of the pause state.

### PlaneTelemetry.cs
A script that monitors and updates the values of telemetry on screen: throttle, speed, and altitude.

### PropellerControl.cs
A script that animates the propeller at the front of the plane which scales the rotation speed by the current throttle.

### CameraControl.cs
This script defines the movement of the camera that follows the plane. The camera is located behind the player where the distance scales based on speed in order to create and FOV effect. The camera's motion is also smoothed to make the game look smoother at lower frame rates.

## **Global Scripts**

### Gameplay.cs
A global static script that holds many important values and constants in one place and has low-scope functions that may be used in multiple scenes.

### Player.cs
A global static script that keeps track of the current player and some player attributes.

## **Communication Interfaces**
### IMUReader.cs
This script is the interface between the game and our controller with the IMU. On script startup, we spawn a new thread and create a TCP socket and listen for data on a given IP and port. A controller would send data from the IP and port to the `IMUReader.cs` script which parses the data and sends the roll, pitch, and boost values over to `PlaneControl.cs`. The arrival of data changes the game state from the controller connect state to the in game state.

We decided to use TCP instead of MQTT to route the data streaming from the IMU to our `PlaneControls.cs` as TCP allowed for 100+ Packets/second while MQTT only allowed for 7 Packets/second which was nowhere near precise enough for a smooth ride as controls would often be extremly janky. We still use MQTT as a way to conduct a handshake between the laptop and the controller but all data is sent over TCP.

### HGReader.cs
This script is the interface between the game and Tensorflow/Mediapipe for gesture recognition. On script startup, we spawn a new thread and try to receive data over UDP from a UDP socket open in the hand gesture script thats running on the laptop. The data is parsed and if a hand gesture is detected, the information is passed to `PlaneControl.cs` which changes the speed or fires a laser.

### StreamingRecognizer.cs
This script handles the communciation between the microphone and the google speech to text cloud API. The script activates the microphone and occasionally sends snippets of recordings to the cloud and receives a transcript of what was said. We can then parse the transcript for keywords and define actions for each keyword recognizer. The keywords that are available are different depending on which scene we are in.

### WebAPIAccess.cs
Starts coroutines to asynchronously make a web API request to our backend server. Each request will take in parameters and return the web's response.

## **Gameplay Handlers**
### Shooter.cs
This script handles the main scorekeeping and statistics tracking associated with shooting and earning points. It also implements the shooting method which represents the firing of a shot.

### Bullet.cs
This script is attached to the bullet object and defines the motion of a laser after being shot. It also detects collisions between a bullet and an object and spawns a particle explosion at the point of collision.

### Target.cs
Attached to any balloon or object that can be destroyed for points. This script handles the removal of the object from the world as well as defining the motion of whatever object the script is attached to.

### SpawnBalloons.cs
Randomly spawn balloons within a given bounding box with a given frequency for each balloon type. This script initializes all the balloons as targets and creates the random wind that dictates the targets' movement.

### ObjectCollision.cs
This script handles the collisions between the plane and various objects. If the plane collides with a poppable object, the score is incremented and the target is destroyed. If the plane collides with terrain, we spawn the plane back at the start and decrement the time.

### CountdownTimer.cs
This script handles the time keeping in game and ends the game when time runs out.

### GameEnd.cs
This script handles the game flow after time is up. It fetches the statistics of the current game, displays it to the user, and uploads it into the web server. We also check to see if the player earned any new achievements and update the web server accordingly.

## **Menu Handlers**
### AuthenticationHandler.cs
This script handles all the logic behind authentication. We use two different states, a login state and a signup state so a user can login with a pre-existing account or make a new one. To login a user, we take the username and find the salt and password hash associated with that username using a web API request. We then use that salt in conjunction with the password the user provided to create a hash which we can use to compare with the expected hash to know if the correct password was provided or not. A sign up a user, we generate a random salt and add it to the user's password before hashing it and saving everything to the database. This script also handles the player-bound information by loading in the player's settings layout and loading the player's achievement progress.

### ButtonHandler.cs
This script handles all the logic for controlling the gameflow from the UI on the main menu, end scene, and both game scenes. It is the script that implements all the menu buttons including start, leaderboard, settings, logout, quit, etc. The script is also handles the creating of a loading screen and also starts up the hand gesture recognition in the background.

### SettingsHandler.cs
This script handles all the logic for changing the game settings. It implements inputs to decrease, increase, or toggle certain settings on the settings screen and changes the values globally. These settings are also saved for the player using a web API request.

## **UI Related Scripts**
### TutorialSequence.cs
This script defines a scripted in-game tutorial sequence for the game. It activates different instructions with a pause between each instruction to give players a time to read and process the tutorial.

### UIBoostAnimcation.cs
This script animates the boosting animation of the plane in the tutorial.

### UITiltOscillation.cs
This script animates the tilting animation of the plane in the tutorial.

### MiniCamFollow.cs
This script defines the position of the minimap camera. The camera is updated to always be right above the plane with a given offset.

### MinimapHandler.cs
This script activates or deactivates the minimap depending on the settings

### NoFog.cs
This script disables fog from the minimap camera since it is located very high up and everything is far away from the minimap.

### BalloonIcon.cs
This script handles the bobbing motion of the balloons in the background of the main menu and loading screen.

## **Low Poly Specific Scripts**
### CloudMotion.cs
This script moves the clouds in the direction of the random wind.

### DaytimeHandler.cs
This script changes the lighting settings and skybox depending on the daytime setting set by the user.

### HotAirBalloons.cs
This script changes the colors of the hot air balloons randomly from a list of presets.

### MoveForwards.cs
This script moves whatever object it is attached to forward at a constant defined speed. Used for moving obstacles.

### RetroCamHandler.cs
This script handles the retro camera effect that is available in the settings. The way the effect works is we take the input to the camera and instead of feeding it directly to the screen, we first pass it to a pixel renderer which pixelates the input. We then take the output of the pixel renderer and load it on the canvas so that the screen displays the pixelated image rather than the camera feed. Post processing is disabled to highlight the camera effect when the setting is on.

### WindTurbine.cs
This script rotates the wind turbines with the rotation speed scaled to the speed of the wind.

## **Misc Scripts**
### Achievements.cs
This script contains data structures and helper functions that are used to deserialize a JSON retried from the web server, store it in a data structure, and load the achievement data for a given player.

### LoadAchievements.cs
This script is a helper script that calls the web server to get the achievement data for a given player and passes it to the Achievement.cs handler.

### Jukebox.cs
This script handles the soundtracks for each map. We create a data structure to hold each song and create an array of songs. Each song contains an audio file, a value to indicate how frequently it plays in relation to other songs in the array, and a volume normalization value since some songs are louder than others. At the beginning of each scene, we use the frequency values to pick a random song, scale the volume by the song's normalization value, and play it. Each scene has its own soundtrack meaning each scene has a different array of songs to pick from.

# Future Improvements:
We can improve the game by adding a multiplayer mode that allows direct competition between two players in the same map.
