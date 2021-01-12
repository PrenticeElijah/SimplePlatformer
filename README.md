# SimplePlatformer

I created a short (one level) platformer game similar to Mario or Kirby within one week. Working on this project was a great introduction to the some basic jump and movement physics in Unity. The main concepts I wanted to learn from this project include:
  - Raycasting
  - Force and Velocity
  - Tilemaps and Sprite Atlas'
  - Delta Time and Fixed Delta
  - Animation

One of the biggest issues I ran into was the moving platforms. At first, the moving platforms seemed to be moving at an appropriate speed in the Unity Editor, but testing the build showed that the platforms were actually moving slower than I thought. This was because my script that moved the platforms was dependent on the Update() function that is called once per frame. Therefore, the speed of the platforms was dependent on the frame rate of my laptop when testing the build. Changing the 'moveSpeed' variable only served as a temporary solutuon because the platforms would still be dependent on the frame rate. The solution was to replace Update() with FixedUpdate(). Unlike the other function, FixedUpdate() is called the fixed number of times every second. The platforms now move at the same speed in the editor and in the build after changing the script to call the movement functions in FixedUpdate() and multiplying their movement speed by Time.fixedDeltaTime.

Builds for PC, MAC, and Linux are available in the "Builds" folder of the project.

Art/Sound Credits
- Player Character and Tileset: https://opengameart.org/users/grafxkid
- Coin SFX: https://opengameart.org/content/10-8bit-coin-sounds
- Jump SFX: https://opengameart.org/content/8-bit-platformer-sfx
- Music: https://opengameart.org/content/4-chiptunes-adventure
