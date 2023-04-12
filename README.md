# Whac-A-Mole
This is a Unity3D project I made in a couple days with some set limitations that I had not attempted before:
- The application must be able to run on Android.
- The highscores need to be saved between app sessions.
And also:
- Making variable and scalable implementations of whac-a-mole gameplay mechanics.

# Summary
Following this, I'll shortly explain the scripts.

For context, the game contains 4 scenes:
- Menu, where the player loads in at the start, from which the Game- and Highscores-scenes can be reached.
- Highscores, where the player can look at their past highscores.
- Game, where most of the game logic is set up and handled.
- Loading, a temporary scene to default to if for some reason a scene wont load.

It also features 3 difficulty settings that can be chosen at the start of the Game-scene. These Difficulty-settings each have:
- A different number of molehills to show it can generate different fields.
- A different highscores-list to maintain and update.
- And of course more difficult gameplay.
(And a missed opportunity to show the other flexible game constants such as gameduration and score-rewards. These are still handily editable in the Unity Editor however.)

The game scene features a GameManager to keep track of and switch between gamestates. A UIManager keeps track of these gamestate-changes to independently activate the necessary UI panels. From the GameManager the GameController is also set-up and enabled.

The GameController sets up the gameplay mechanics and keeps track of the score, mole spawning, and time remaining. It also sets up the GameBoard that generates the optimal layout given a number of molehills.

Other scripts are also well-documented and pretty self-explanatory in terms of functionality.
