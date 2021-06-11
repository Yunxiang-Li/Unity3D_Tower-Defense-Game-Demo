# Unity3D_Tower-Defense-Game-Demo

[![standard-readme compliant](https://img.shields.io/badge/readme%20style-standard-brightgreen.svg?style=flat-square)](https://github.com/RichardLitt/standard-readme)

A simple 3d tower defense game demo based on Unity3D game engine.

## Table of Contents

- [Background](#Background)
- [Exhibition](#Exhibition)
- [Install](#install)
- [Usage](#usage)
- [Structure](#Structure)
- [Maintainers](#Maintainers)
- [License](#license)

## Background

In this tower defense game demo, there will be enemies(rams) spawned from the enemy(left) side's castle gate and head towards the player(right) side's castle gate. Each enemy's movement will follow a certain path and the path will be calculated again if it is not the best one. We players can place towers(ballista) to shoot and kill each enemy with left mouse click.

For each tower, we need certain golds to place and we can only place towers on green tiles except castle area. If an enemy is destroyed by towers, we will gain certain reward golds, if an enemy reaches the player's castle gate, we will lose certain punish golds. When gold amount is less than or equal to 0 then players lose. Additionally, if our placement of a tower will block the path, then that tower cannot be placed down.

I use object pool to manage each enemy's instantiation. For path finding, I use BFS(Breadth-First Search) approach.

## Exhibition

<div align="center"> <img src=""/> </div>

## Install

I use Unity2020.1.1f1 and JetBrain's Rider IDE for this project under Windows 10 environment.<br>
[Unity and Unity hub download](https://unity3d.com/get-unity/download)<br>
[archived Unity download ](https://unity3d.com/get-unity/download/archive)<br>
[Jetbrains Rider download](https://www.jetbrains.com/rider/download/#section=windows)

## Usage

1. Download this repo, open(or unzip and open) the **Unity2D_Flock-Demo** folder.

2. Open the **Unity Hub**, from the Home Screen, click **Projects** to view the **Projects** tab.

3. To open an existing Unity Project stored on your computer, click the Project name in the **Projects** tab, or click **Open** to browse your computer for the Project folder.

4. Note that a Unity Project is a collection of files and directories, rather than just one specific Unity Project file. To open a Project, you must select the main Project folder, rather than a specific file.

5. For this game, just select the **Unity2D_Flock-Demo** folder and open this project.

## Structure

The whole project in Unity contains two main folders, **Assets** folder and **Package** folder.

Under **Assets** folder, there are altogether **6** subfolders:

1. Behavior Objects folder: contains **15** behavior objects (Alignment behavior object, stay in radius behavior object and so on) created by scriptable objects in Unity as assets.

2. Filter Objects folder: contains **2** filter objects (same flock filter and obstacle layer filter) also created by scriptable objects in Unity as assets.

3. Prefabs Objects folder: contains **4** prefabs (orange, green, blue and default white prefabs).

4. Scenes folder: contains **1** main scene of this game demo.

5. Scripts folder: contains **18** C# scripts I write for this game demo. Under **Behavior Scripts** subfolder, there are altogether **12** C# scripts about flock behaviors. Under **Editor** subfolder, there is **1** C# script called CompositeBehaviorEditor which uses Unity GUI module to generate a custom behaviors editor. Under **Filter Scripts** subfolder, there are **3** C# scripts about filters. In addition to these C# scripts, there are also **Flock.cs** and **FlockAgent.cs** files to represent a flock and each single bird.

6. Sprites folder: contains **1** sprite for a single flock agent.

## Maintainers

[@Yunxiang-Li](https://github.com/Yunxiang-Li).

## License

[MIT license](https://github.com/Yunxiang-Li/Unity3D_Tower-Defense-Game-Demo/blob/master/LICENSE)
