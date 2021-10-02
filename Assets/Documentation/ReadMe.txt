#####################
2021-09-19 Version 1:
#####################
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


#####################
2021-09-24 Version 2:
#####################
1) Drag functionality is added: the player will slowly drift towards the planet without any fuel consumption.
2) Added a second camera (third person).
3) "C" key can switch between the top view and the third person cameras.
4) "Space" key is used to slow down: use this functionality when the min distance reaches 65 until the speed reaches 31. This will get the player captured by the planet, then increase the render speed and watch.

PS: The final descent (yet not coded) will get us into the second stage where we change the scene to a Plane gameobject with the landing site from the NASA resources.