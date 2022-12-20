# State Machine

<p align="center">
	<img alt="GitHub package.json version" src ="https://img.shields.io/github/package-json/v/Thundernerd/Unity3D-StateMachine" />
	<a href="https://github.com/Thundernerd/Unity3D-StateMachine/issues">
		<img alt="GitHub issues" src ="https://img.shields.io/github/issues/Thundernerd/Unity3D-StateMachine" />
	</a>
	<a href="https://github.com/Thundernerd/Unity3D-StateMachine/pulls">
		<img alt="GitHub pull requests" src ="https://img.shields.io/github/issues-pr/Thundernerd/Unity3D-StateMachine" />
	</a>
	<a href="https://github.com/Thundernerd/Unity3D-StateMachine/blob/main/LICENSE.md">
		<img alt="GitHub license" src ="https://img.shields.io/github/license/Thundernerd/Unity3D-StateMachine" />
	</a>
	<img alt="GitHub last commit" src ="https://img.shields.io/github/last-commit/Thundernerd/Unity3D-StateMachine" />
</p>

A code-generated state machine including transitions, with support for Zenject.

## Installation
1. The package is available on the [openupm registry](https://openupm.com). You can install it via [openupm-cli](https://github.com/openupm/openupm-cli).
```
openupm add net.tnrd.statemachine
```

2. Installing through a [Unity Package](http://package-installer.glitch.me/v1/installer/package.openupm.com/net.tnrd.statemachine?registry=https://package.openupm.com) created by the [Package Installer Creator](https://package-installer.glitch.me) from [Needle](https://needle.tools)

[<img src="https://img.shields.io/badge/-Download-success?style=for-the-badge"/>](http://package-installer.glitch.me/v1/installer/package.openupm.com/net.tnrd.statemachine?registry=https://package.openupm.com)

3. Installing through the Package Manager by Git URL.
   1. Open the package manager
   2. Click the plus symbol in the top left corner
   3. Select "Add package from git URL..."
   4. Paste this url: https://github.com/Thundernerd/Unity3D-StateMachine
   5. Click "Add"

## Dependencies
Currently this package has a dependency on [Odin Inspector](https://odininspector.com). This is used for drawing the inspector for the `StateMachineGraph` asset.

Future versions (TBD.) will drop this dependency in favor of a visual graph editor.

## Usage

### Creating a graph
To make a state machine you first have to create a state machine graph. You can do this by clicking your right mouse-button and selecting `Create/State Machine Graph`.

This will create a new file that hosts the information to generate a state machine for you.

![creating graph](/.documentation/create_graph.png)

Once you have created a graph you are required to fill in some information:

**State Machine Name**

This is the name of the state machine and will be used throughout the generated files of the state machine. My suggestion is to use something that is relevant to the state machine as a whole.

**Namespace**

The namespace that will be applied to the generated code. Currently this option is mandatory, but future versions might see this changed.

**Destination**

This is the destination folder where the generated code for the state machine will be saved.

**Use Zenject**

With this toggle you can select if you want this state machine to work with Zenject or not. Please keep in mind that the two options are not compatible with each other, which means that switching this after you've already generated the state machine before could lead to issues.

![basic info](/.documentation/basic_info.png)

### Adding states

To add a state you can click the button with the plus icon to the right of `States`. This will create a new entry in the `States` list.

You can choose a name for the state, and if the state is the initial state. There can only be one initial state, and that is the state that is first entered when the state machine starts.

![adding states](/.documentation/states.png)

### Adding transitions

To add a transition you can click the button with the plus icon to the right of `Transitions`. This will create a new entry in the `Transitions` list.

Please note that you need to make states first for transitions to work.

Once you have created a transition you have to first select the source for this transition. You can use the dropdown field to select a previously made state for this.

Once you have selected a source, you can add destinations by clicking the button with the plus icon to the right of `Destinations`. Clicking this will show you a selection field where you have to select the destination state for this transition. You can only add each state once.

![adding transitions](/.documentation/transitions.png)

### Generation

After you have followed the steps above, you should now be able to press the button that says `Generate`. Once you click this, all the code necessary for using the state machine will be generated in the destination folder that you've provided before.

### Using the state machine

<details>
<summary>With Zenject</summary>

Once you have generated the code with the `Use Zenject` toggle enabled, you will be able to use this state machine with Zenject. 

To get started you will have to create an installer for your newly generated state machine. This installer is also generated for you, and you can easily create the installer by right clicking somewhere in your project view and selecting `Create/State Machine Installers/_name of your statemachine_`.

This will create a Scriptable Object Installer for the state machine, and can be used with the contexts provided by Zenject.
</details>

<details>
<summary>Without Zenject</summary>

Once you have generated the code without the `Use Zenject` toggle enabled, you will be able to use this state machine without Zenject, but through the use of regular Unity practices.

To get started you will need to add a component to an object in a scene. This component is automatically generated for you and will be named `_name of your statemachine_Controller`. Once you have attached this script to a GameObject it will automatically create, initialize, and run itself.
</details>

### Adding functionality

#### States

To add functionality to the states you can simply open one of the generated state class files and add the code that you deem needed.

There are three pre-generated methods in every state class.

**Constructor**

You can use the constructor as you would any other constructor for any other C# class. This constructor will be called before the state is entered, and is therefore useful for initialization.

**OnEnter**

The OnEnter method is called when the state is entered. This means that this state is now the currently active state. You can use this method to execute state specific logic that should only happen when this state is active.

**OnExit**

The OnExit method is called when the state is being exited. This will be called before a new state is entered. You can use this to clean up everything related to your state.

#### Transitions

Transitions are technically also states. They are states that live in between two normal states. These transitions can be used for all kinds of purposes.

Some good examples are:
- Playing animations
- Loading/unloading scenes
- Loading/unloading data from disk/web
- Preparing GameObjects for the next state

Or anything else that takes a longer (but not necessarily long) period of time.

There are two pre-generated methods in every transition class.

**Constructor**

You can use the constructor as you would any other constructor for any other C# class. This constructor will be called before the transition is entered, and is therefore useful for initialization.

**StartTransition**

This is the entry point for the transition. Here you can kick off routines, load/unload scenes, or prepare some data.

Once you have finished everything that this transition needed to do you can continue towards the next state by calling `FinishTransition`.

### Utilities

When using Zenject it can happen that you want to access data from a scene in a state or transition. You are free to write your own solution for this, but, there is also a small helper component that can be used for this usecase.

Find a component that you want to be able to access from a state or transition and add the State Component Installer or Transition Component installer (respectively) to the game object. These components will be prefixed with the name of your state machine.

Drag the component you want to be available into the `Component To Install` field.

If you want to bind any interfaces that are attached to this component as well, check the `Bind Interfaces` toggle.

If you want to bind the component with a specific id, check the `With Id` toggle, and enter the id in the newly appeared `Id` field`.

Note that you cannot bind interfaces **and** bind with a specific id at the same time.

Next add states to the `State Ids` list or transitions to the `Transition Ids` list depending on which you have chosen. You can add as many states or transitions as you like.

Finally, to make the injection actually happen, add the newly added component installer to the scene context in your scene under the `Mono Installers` section.

![component installers](/.documentation/component_installers.png)

## Support
It is by no means necessary but if you feel generous you can support me by donating.

[![ko-fi](https://www.ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/J3J11GEYY)

## Contributions
Pull requests are welcomed. Please feel free to fix any issues you find, or add new features.
