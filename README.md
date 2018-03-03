# <img src="https://s3.amazonaws.com/unity-stories/unity-stories-withname.png" style="max-height: 128px !important; min-height: 96px !important;">
Unity Stories is a state container for games built in Unity utilizing Scriptable Objects. 

## Influences
Unity Stories is mainly taking inspiration and is influenced by <a href="https://github.com/reactjs/redux">Redux</a> and <a href="https://github.com/facebook/flux">Flux</a>. 

Forerunner implementations in Unity / C# that also have influenced Unity Stories are: 
- <a href="https://github.com/gblue1223/redux-unity3d">redux-unity-3d</a>
- <a href="https://github.com/mattak/Unidux">Unidux</a>

## Motivation
The general approach to building scripts in Unity often generates a code base that is monolithic. This results in that your code is cumbersome to test, non-modular and hard to debug and understand. 

The aim of Unity Stories is to seperate concerns between your game state and the implementation of your game logic making your scripts modular. The will make protyping your game faster and makes it easier to make changes to your code base even though your project has grown large.  

## Installation
Import unitypackage from latest releases or download and import into your project from the Unity Asset Store (coming soon).

## Usage
See examples in the project.

## Performance
In order to avoid unnecessary garbage collection reference types (for example strings) in stories and in actions should be avoided when possible. Furthermore, it also might be a good idea to use action factories in order to not generate unnecessary garbage. 