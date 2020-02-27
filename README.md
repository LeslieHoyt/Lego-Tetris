# Lego-Tetris
A 3D Lego Tetris game written in C# and made in Unity

## Program Overview
This game simulates the falling of Lego blocks of different shapes and sizes randomly from the sky and landing on a given platform of NxM dimensions. While the blocks are falling, the user has the ability to move the falling block within the boundaries of the platform as well as rotate the falling block using their keyboard.

## Basic Features
The program has the following features:
1. Dynamically generates the platform where the Lego blocks are going to land. The dimensions of the platform will be provided by the user when they run the application.
2. Dynamically instantiate one of the many Lego blocks at random.
  - The instantiated Lego block will have a random position on the XZ plane.
  - The instantiated Lego block will have a random orientation.
  - The instantiated Lego block will have a random color assigned to it.
3. User can move the falling Lego Block on the XZ plane.
  - The following keys are used for movement: A, W, S, D.
4. User can re-orient the falling Lego Block using the same constraints listed above.
  - R key is used for rotation of the block.
5. The falling block will stop when it has landed either on the platform or on another existing block.
6. The cycle will begin all over again until the user quits the application.
7. The application can be terminated by pressing the Q button.

### Camera Movement
The user will also have the ability to control some aspects of the camera during the simulation. They will be able to perform the following:
1. Rotate the camera around the platform, always looking at the center of the platform
  - Left and right arrow keys are used for rotating the camera around the platform.
2. Move the camera Up and Down on the Y-Axis
  - Up and Down arrow keys are used for elevating the camera position or lowering it, respectively.
3. Zoom In or Out the camera for closer look at the platform
  - Z and X keys are used to zoom the camera in and out, respectively.

### Additional Features
Settings menu contains the following features:
1. Simulation Mode
  - This is the default mode, where the user does not have control of the movement/rotation of the blocks, but can move the camera.
2. Game Mode
  - This is the playable mode, where the user has full control of the falling blocks.
3. Breakable Mode
  - In this mode, Lego blocks break apart on impact with another block.
4. Uniform Mode
  - This is the default mode, where Lego blocks do not break apart on impact.
5. Difficulty Level
  - By default this is set to the hardest level. This corresponds to the speed with which the blocks fall.
