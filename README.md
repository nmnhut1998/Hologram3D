CS427 - 3D Visualization and Game Development
FINAL PROJECT REPORT 

Minh-Nhut Nguyen, Gia-Han Diep		 
           

Brief
Hologram has long appeared on screen of sci-fi films, especially in form of hologram-chatting that can show the speaker's expression, simulating the conversation between people. For that reason, our group choose 'Hologram 3D to express facial expression in video*' as our topic. To-be-processed video is uploaded with TCP socket and user will get a hologram avatar with their facial expression changing over time as in the video (the video must be recorded in close-up so that all parts of the face is visible)

Implementation 
We use MVC model and TCP Client-Server model as follows: 

Server creates a TCP socket and bind it to a server address (we use localhost) on port 8888. After that, the server runs in passive mode, listening for connection request from client. Once connection is established, the server will stream data to the client. 

As for client, it first creates a TCP socket, and as soon as it is ready, send a 'hand-shake' request to the server. The server then sends packets that contains facial expression data in the video, which the client parses and displays it in the hologram. 

In our project, Unity acts as client and Python is responsile for server. Video is first passed in to a facial recognition model in server. The server uses the model to determine expressions in the video and waits for client to request. 

We use 3D model designed with Daz Studio (reference below) to show facial expression. 

How to run (test) 
In the github source, there is 3 folders. Hologram3D and Hologram3D - 4 side is the unity client and Facial-expression-recognition-master contains python server. 

First, the server need to be start, open file  facial-expression-recognition-master/ /facial-expression-recognition-master/trained models/run.py ,install necessary packages (kerras, numpy, opencv, etc) and change path to test video. After that, run the file run.py, waits for a moment until the 'listening' message appear on the console. At this time, the server is successully started and result of facial expression in the video is ready to be streamed to client. . 

After that, run Hologram 3D or Hologram 3D -4 side in Unity, the server will try to connect to server and display expression on 3D avatar model

# HOLOGRAM DEMO:
Because the file is too big to be uploaded on github, this is the link:
https://drive.google.com/file/d/1vks0Vk4MsuNIOAZxZqVFIAgiPm-U1L6D/view?usp=sharing

References:
Pretrained model cho phần detection:
https://github.com/akash720/Facial-expression-recognition

3D model by Daz Studio:
https://www.youtube.com/watch?v=jIdNZsEJK-U

TCP socket guide - python: 
https://docs.python.org/2/library/socket.html

C# socket guide: 
https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.tcpclient?view=netframework-4.8

TCP - socket client basics: 
https://www.geeksforgeeks.org/tcp-server-client-implementation-in-c/

MVC model - image
https://upload.wikimedia.org/wikipedia/commons/thumb/a/a0/MVC-Process.svg/1200px-MVC-Process.svg.png

Supporting tools: 
Unity:		https://unity.com
Daz Studio:	https://www.daz3d.com
