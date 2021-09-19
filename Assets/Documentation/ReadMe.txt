For the inital capture
----------------------
1) At first, the game is rendered at 5x to speed up the initial approach.
2) Pause the game when the "Min" reaches 100, and change the render speed to 1.
3) Continue the game until the "Min" reaches 60 then pause.
4) Advance the game one step at a time until the "Dist" starts to increase and the "Min" stops changing (the value for Min should be around 56.2).
5) At this point, press "Trigger" in in the Inspector under Gravity script to change the speed of the player.
6) Play the game and watch the player orbiting around the plant.
7) Press "Ressetting" under UI Controller script to reset the Min and Max distances


For the orbit change:
---------------------
1) Change the New Speed value in Gravity scprit to 29.925, then redo steps 3 to 7. The orbit will be smaller: the player will pass by the same trigger point, but will be nearer to the planet on the other side.
2) Change the New Speed value in Gravity scprit to 27.608, then redo steps 3 to 7. The orbit will be circular and the player is moving at a near constant speed.
3) You can change the render speed at the time to confirm that the orbit is stable.

Disclaimer: The demo is NOT equiped to simulate anything further. Future version will implement functions to automatically calculate and trigger the necessary speed to reach any orbit that the user inputs in the Inspector.