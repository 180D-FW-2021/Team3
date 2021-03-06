# https://techvidvan.com/tutorials/hand-gesture-recognition-tensorflow-opencv/
# TechVidvan hand Gesture Recognizer

# import necessary packages
import socket
import sys
import UdpComms as U
import time
import cv2
import numpy as np
import mediapipe as mp
import tensorflow as tf
from tensorflow.keras.models import load_model
import os

def resource_path(relative_path):
    try:
        base_path = sys._MEIPASS
    except Exception:
        base_path = os.path.abspath(".")

    return os.path.join(base_path, relative_path)

# initialize mediapipe
mpHands = mp.solutions.hands
hands = mpHands.Hands(max_num_hands=1, min_detection_confidence=0.7)
mpDraw = mp.solutions.drawing_utils

# Load the gesture recognizer model
model = load_model(resource_path('mp_hand_gesture'))

# Load class names
classNames = ['', '', 'thumbs up', 'thumbs down', '', 'shoot', '', 'shoot', '', '']
#print(classNames)


# Initialize the webcam
cap = cv2.VideoCapture(0)

def extract_ip():
	st = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
	try:
		st.connect(('10.255.255.255', 1))
		IP = st.getsockname()[0]
	except Exception:
		IP = '127.0.0.1'
	finally:
		st.close()
	return IP


# sock = U.UdpComms(udpIP="192.168.50.126", portTX=8000, portRX=8001, enableRX=True, suppressWarnings=True)

ipAddress = extract_ip()

sock = U.UdpComms(udpIP=ipAddress, portTX=8000, portRX=8001, enableRX=True, suppressWarnings=True)
i = 0

while True:
    # Read each frame from the webcam
    _, frame = cap.read()
    x, y, c = frame.shape
    # Flip the frame vertically
    frame = cv2.flip(frame, 1)
    framergb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)

    # Get hand landmark prediction
    result = hands.process(framergb)

    # print(result)
    
    className = ''

    # post process the result
    if result.multi_hand_landmarks:
        landmarks = []
        for handslms in result.multi_hand_landmarks:
            for lm in handslms.landmark:
                # print(id, lm)
                lmx = int(lm.x * x)
                lmy = int(lm.y * y)

                landmarks.append([lmx, lmy])

            # Drawing landmarks on frames
            mpDraw.draw_landmarks(frame, handslms, mpHands.HAND_CONNECTIONS)

            # Predict gesture
            prediction = model.predict([landmarks])
            # print(prediction)
            classID = np.argmax(prediction)
            className = classNames[classID]


            for idx, hand_handedness in enumerate(result.multi_handedness):
                if (hand_handedness.classification[0].label == "Left"):
                    if (classID == 2 or classID == 3 or classID == 5 or classID == 7):
                        cv2.putText(frame, className, (10, 50), cv2.FONT_HERSHEY_SIMPLEX, 1, (0,0,255), 2, cv2.LINE_AA)
                        print(className)
                        sock.SendData(className) # Send this string to other application
                        i += 1

                        data = sock.ReadReceivedData() # read data

                        if data != None: # if NEW data has been received since last ReadReceivedData function call
                            print(data) # print new received data

                        time.sleep(0.1)

    

    
    # Show the final output
    cv2.imshow("Output", frame) 

    if cv2.waitKey(1) == ord('q'):
        break
# release the webcam and destroy all active windows
cap.release()

cv2.destroyAllWindows()
