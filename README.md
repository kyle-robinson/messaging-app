# Messaging Application

[![MSBuild-Debug](https://github.com/kyle-robinson/messaging-app/actions/workflows/msbuild-debug.yml/badge.svg)](https://github.com/kyle-robinson/messaging-app/actions/workflows/msbuild-debug.yml)
&nbsp;
[![MSBuild-Release](https://github.com/kyle-robinson/messaging-app/actions/workflows/msbuild-release.yml/badge.svg)](https://github.com/kyle-robinson/messaging-app/actions/workflows/msbuild-release.yml)
&nbsp;
[![CodeQL](https://github.com/kyle-robinson/messaging-app/actions/workflows/codeql.yml/badge.svg)](https://github.com/kyle-robinson/messaging-app/actions/workflows/codeql.yml)
&nbsp;
<img src="https://img.shields.io/static/v1?label=University&message=Year 2&color=49a1e5&style=flat&logo=nintendogamecube&logoColor=CCCCCC" />

A multithreaded online messaging application that utilises a client-server architecture to allow for devices to send messages across the local network.

*- Click <a href="https://kyle-robinson.github.io/html/networking" target="_blank">here</a> to view project on website -*<br/>

<img src="Assets/Client Demo 1.png" alt="Client Message Application Demo 1" border="10" width="47%" /> <img src="Assets/Client Demo 2.png" alt="Client Message Application Demo 2" border="10" width="47%" />

## List of Features

- [x] TCP/IP and UDP
- [x] Global Messaging
- [x] Private Messaging
- [x] Cryptography
- [x] Login System
- [x] Muting System
- [x] Client/Friend Lists

## Getting Started

Refer to the following information on how to install and use the application.

### Dependencies
To use the application, the following prerequisites must be met.
* Windows 10+
* Visual Studio
* Git Version Control

The application does not rely on the any additional libraries or APIs to function.

### Installing

To download a copy of the application, select "Download ZIP" from the main code repository page, or create a fork of the project. More information on forking a GitHub respository can be found [here](https://www.youtube.com/watch?v=XTolZqmZq6s).

### Executing program

A runtime configuration has been setup to run an instance of both the server and client project on project load. To create additional instances of the client application, follow these steps while the application in running.
1. Navigate to the Client project in the Solution Explorer.
2. Right-click the project and navigate to the Debug option.
3. Click "Start New Instance" to generate another Client application.

---

### Credits

    Code Reference
        https://youtu.be/EA5jF_7FteM
        https://youtu.be/sYGS80-Joi8
