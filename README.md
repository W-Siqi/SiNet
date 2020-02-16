[Devlog-link]:()
[Img-entity-separation]:/Resources/Media/entity-separation.png
[Img-server-client-illus]:Resources/Media/server-client-illus.png
[Img-ECS]:Resources/Media/ECS.png
# SiNet
A lightweight networking framework of 3D synchronizing.    
Which separate data synchronizing from the logic built on these data.  

It is used in and driven by  a acedemic project in Carnegie Mellon University.   
## demo1: Car driving, hello world
![image](/Resources/Media/demo.gif)
## demo2: VR character present
present the whole body in muliplayer VR，by synchronizing positon, animation and behaviour.
![image](/Resources/Media/vrdemo1.gif)
![image](/Resources/Media/vrdemo2.gif)
# Features 
This is an under developing  project (see [Devlog][Devlog-link]), evolving with the project using this framework.  
1. provide a [Entity-Component-System](https://en.wikipedia.org/wiki/Entity_component_system) for wrting data synchronizing logic.
2. Build an application layer protocal and its transmitting fucntions  
3. Remote Procedure Calls (RPC) for event communication between servers and clients.
# How it works
### separate data synchronzing from logic codes
In OOP, we inheritance hierachies.  
In an ECS design, we use combination to describe the commonality and difference between entitits(classes), and support polymorphism by removing and adding components in the runtime.
![image][Img-ECS]
### separate the codes of source entity and its replication on other clients.
By instancing different entity, we can write two versions on authority side and replication side:  

![image][Img-entity-separation]  

Our framework will do the mapping between two side across servers and clients:  
![iamge][Img-server-client-illus]