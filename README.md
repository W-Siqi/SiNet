[Devlog-link]:()
[Img-entity-separation]:/Resources/Media/entity-separation.png
[Img-server-client-illus]:Resources/Media/server-client-illus.png
[Img-ECS]:Resources/Media/ECS.png
# SiNet
A lightweight networking framework of 3D synchronizing.    
Which separate data synchronizing from the logic built on these data.  

It is used in and driven by  a acedemic project in Carnegie Mellon University.   
![image](/Resources/Media/demo.gif)
### Core idea of this project
Compared to [UNet](https://docs.unity3d.com/Manual/UNet.html) in Unity, we try to build a more flexible, lightweight and transparent framework, to let programmers write logic codes without caring too much on networking issues.  
# Features 
This is an under developing  project (see [Devlog][Devlog-link]), evolving with the project using this framework.  
1. provide a [Entity-Component-System](https://en.wikipedia.org/wiki/Entity_component_system) for wrting data synchronizing logic.
2. Build an application layer protocal and its transmitting fucntions  
3. Remote Procedure Calls (RPC) for event communication between servers and clients.
# Goals
### Goal 1: separate data synchronzing from logic codes
In OOP, we inheritance hierachies.  
In an ECS design, we use combination to describe the commonality and difference between entitits(classes), and support polymorphism by removing and adding components in the runtime.
![image][Img-ECS]
### Goal 2: separate the codes of source entity and its replication on other clients.
By instancing different entity, we can write two versions on authority side and replication side:  

![image][Img-entity-separation]  

Our framework will do the mapping between two side across servers and clients:  
![iamge][Img-server-client-illus]