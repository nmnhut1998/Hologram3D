import keras
import numpy as np
import matplotlib.pyplot as plt
import keras
from keras.models import Sequential
from keras.layers import Dense, Conv2D, MaxPooling2D, Dropout, Flatten
from keras.utils import to_categorical
from keras.models import load_model

import cv2
import matplotlib.pyplot as plt

import socket; 
import sys; 

pretrained_model = load_model("fer2013_mini_XCEPTION.99-0.65.hdf5")
faces = []
face_cascade = cv2.CascadeClassifier('haarcascade_frontalface_default.xml')


def get_label(argument):
    labels = {0:'Angry', 1:'Disgust', 2:'Fear', 3:'Happy', 4:'Sad' , 5:'Surprise', 6:'Neutral'}
    return(labels.get(argument, "Invalid emotion"))



def expression_recognize(img):
    faces = face_cascade.detectMultiScale(np.asarray(img), 1.3, 5)
    for (x, y, w, h) in faces:
        if len(faces) == 1: #Use simple check if one face is detected, or multiple (measurement error unless multiple persons on image)
            crop_img = img[y:y+h, x:x+w]
        else:
            print("multiple faces detected, passing over image")

        #Resizing image to required size
        test_image = cv2.resize(crop_img,(64,64),0,0,interpolation = cv2.INTER_AREA)

        #Converting image to array
        test_image = np.array(test_image)

        #converting to grayscale
        gray = cv2.cvtColor(test_image, cv2.COLOR_BGR2GRAY)

        #scale pixels values to lie between 0 and 1 because we did same to our train and test set
        gray = gray/255

        #reshaping image (-1 is used to automatically fit an integer at it's place to match dimension of original image)
        gray = gray.reshape(-1, 64, 64, 1)

        res = pretrained_model.predict(gray)

        #argmax returns index of max value
        result_num = np.argmax(res[0])

        # print predictions
        print("\nProbabilities are " + str(res[0])+"\n")
        print("Emotion is "+ get_label(result_num))
        return (result_num,res[0][result_num]); 
    

def start_server(res_arr): 
  


  HOST = '' # Symbolic name meaning all available interfaces
  PORT = 8888 # Arbitrary non-privileged port

  s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
  print ('Socket created')

  #Bind socket to local host and port
  try:
    s.bind((HOST, PORT))
  except socket.error as msg:
    print ('Bind failed. Error Code : ' + str(msg[0]) + ' Message ' + msg[1])
    sys.exit()
  
  print ('Socket bind complete')

  #Start listening on socket
  s.listen(10)
  print ('Socket now listening')



  #now keep talking with the client
  while 1:
    #wait to accept a connection - blocking call
    conn, addr = s.accept()
    print ('Connected with ' + addr[0] + ':' + str(addr[1]))
    # send a message to the client.
    l = len(res_arr)
    l = str(l).zfill(13)
    print('send : '+l); 
    conn.send(l.encode('utf-8')); 
    for item in res_arr:
        if (item is not None):
            conn.send(str(item[0]).zfill(13).encode('utf-8'))
            conn.send(str(item[1]).zfill(13).encode('utf-8'))
            print('send',item[0],item[1]) 

  s.close()

def video_process(): 
  cap = cv2.VideoCapture("C:/Users/nmnhut/Pictures/Camera Roll/test.mp4")
  train_result = []
  while(cap.isOpened()):
    ret, frame = cap.read()
    if (ret):
        #cv2.imshow('frame',frame)
        train_result.append(expression_recognize(frame));
    else:
        break
    if cv2.waitKey(2) & 0xFF == ord('q'):
        break
    if (not ret):
        break

  cap.release()
  #cv2.destroyAllWindows()
  return train_result; 


result = video_process();
start_server(result);

