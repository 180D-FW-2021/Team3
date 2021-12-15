Hand Gesture Recognition

For our game, hand gestures are used to control the throttle of the plane and shoot lasers. MediaPipe and Tensorflow are used with OpenCV in python 3 to implement the hand gesture recognition classifier.

Source Code for hand gesture recognition: https://techvidvan.com/tutorials/hand-gesture-recognition-tensorflow-opencv/

This source code uses:
  - OpenCV to read frames from the camera
  - MediaPipe to track the hand 
  - Tensorflow to recognize the hand gesture

Added code to make sure only left hand was being detected.

Added source code from https://github.com/Siliconifier/Python-Unity-Socket-Communication to get communication between unity and python.
  - Uses UDP socket to send hand gesture data from python to unity.

Gameplay:
  - Before starting the game in unity, the player must run hgr.py first. 
  - During the game, if a player displays a thumbs-up/thumbs-down, the throttle will increase/decrease. If they display an open palm, the plane will shoot projectile.


