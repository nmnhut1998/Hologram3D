import keras
import numpy as np
import matplotlib.pyplot as plt
import keras
from keras.models import Sequential
from keras.layers import Dense, Conv2D, MaxPooling2D, Dropout, Flatten
from keras.utils import to_categorical
from keras.models import load_model
pretrained_model = load_model("fer2013_mini_XCEPTION.99-0.65.hdf5")

import cv2
import matplotlib.pyplot as plt

def get_label(argument):
    labels = {0:'Angry', 1:'Disgust', 2:'Fear', 3:'Happy', 4:'Sad' , 5:'Surprise', 6:'Neutral'}
    return(labels.get(argument, "Invalid emotion"))

faces = []


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
    
cap = cv2.VideoCapture(0); 

while(cap.isOpened()):
    ret, frame = cap.read()
    cv2.imshow('frame',frame)
    expression_recognize(frame); 
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

cap.release()
cv2.destroyAllWindows()


img = cv2.imread("surprise.jpg")


