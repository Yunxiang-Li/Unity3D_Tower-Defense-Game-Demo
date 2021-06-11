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

<div align="center"> <img src="https://github.com/Yunxiang-Li/Unity3D_Tower-Defense-Game-Demo/blob/main/Screenshots%20and%20GIFs/Second%20exhibition.gif"/> </div>

<div align="center"> <img src="https://github.com/Yunxiang-Li/Unity3D_Tower-Defense-Game-Demo/blob/main/Screenshots%20and%20GIFs/first%20exhibition.jpg"/> </div>

## Install

I use Unity2020.1.1f1 and JetBrain's Rider IDE for this project under Windows 10 environment.<br>
[Unity and Unity hub download](https://unity3d.com/get-unity/download)<br>
[archived Unity download ](https://unity3d.com/get-unity/download/archive)<br>
[Jetbrains Rider download](https://www.jetbrains.com/rider/download/#section=windows)

## Usage

1. Download this repo, open(or unzip and open) the **Unity3D_Tower-Defense-Game-Demo** folder.

2. Open the **Unity Hub**, from the Home Screen, click **Projects** to view the **Projects** tab.

3. To open an existing Unity Project stored on your computer, click the Project name in the **Projects** tab, or click **Open** to browse your computer for the Project folder.

4. Note that a Unity Project is a collection of files and directories, rather than just one specific Unity Project file. To open a Project, you must select the main Project folder, rather than a specific file.

5. For this game, just select the **Unity3D_Tower-Defense-Game-Demo** folder and open this project.

## Structure

The whole project in Unity contains two main folders, **Assets** folder and **Package** folder.

Under **Assets** folder, there are altogether **7** subfolders:

1. AssetPackages folder: contains **2** subfolders: **VoxelCastle** and **TextMesh Pro**, which help us add visual effects to the game demo.

2. Fonts folder: contains **1** font object we use for this game demo.

3. Materials Objects folder: contains **2** materials we use for this game demo. For instance, enemy material and ground material.

4. PostProcessing folder: contains **1** profile object of this game demo which we use to add some post processing effects.

5. Prefabs folder: contains **27** prefabs we use for this game demo. **20** of them are tile prefabs and are store in a **TilePrefabs** subfolder. Some of them are: tree prefabs, ram prefab, ballista prefab, object pool prefab, bank prefab and so on.

6. Scenes folder: contains **1** main game scene.

7. Scripts folder: contains altogether **12** scripts. **3** scripts are stored in a **PathFinding** subfolder and are used for enemy objects' BFS path finding approach: PathFinder, Node and GridManager scripts. Some of other scripts are: EnemyMover, TargetLocator, EnemyHealth and so on.

## Maintainers

[@Yunxiang-Li](https://github.com/Yunxiang-Li).

## License

[MIT license](https://github.com/Yunxiang-Li/Unity3D_Tower-Defense-Game-Demo/blob/master/LICENSE)
