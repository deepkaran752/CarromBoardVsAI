# CarromBoardVsAI
A 2D Carrom Board Game against AI

Characters :- Player, AI
Materials :- Board, Pawns(Black, Red, White), Striker(Player/Enemy)

Process flow:- 
PAWN CLASS: Assigned the pawn as white, red, black and gave them respective scores for assigning the score part 
                      Used the Enum class for easy assigning.
                      
GameController: Resets the pawn, striker, Resets the force on pawn, striker. Checks for the bool isPlayer() -> true
                then Player, false -> AI;
                
StrikerDirect: Contains the functionality for how the striker should strike
               The down functionality works only for isPLayer == true i.e for player.
               When the mouse button is pressed the player can choose the direction while released it releases in that direction.
               Used the raycastHit2D feature to certain if the player is selecting the striker and performed certain rotations and 
               selection to strike in that direction.
               When mouse button pressed: -
               In built functions used here are Mathf.Atan2();, Mathf.Rad2Deg; Quaternion.Euler(); this basically helps with 
               estimating the direction the player is trying to strike.
               When mouse button released:-
               It finds the point of contact with the respect to the click and adds the impulse2d Force to the striker 
               using the method AddForce(pointOfContact* ForceMagnitude, ForceMode2D.Impulse);
               Then a coroutine starts -> which waits for a certain seconds and lets the striker roam then resets the striker location,
               resets the force on the striker and pawns.
               if isPlayer == false // For AI
               AIController() -> script is called;
               
AIController: Works if isPLayer == false //for AI 
              Randomly chooses the position between the striking position, Randomly chooses the angle for striking
              starts a coroutine -> to choose the position, and to rotate the arrow towards which the striker is to be striked by the AI.
              it also uses the bool ready_to_Strike it is set in the above coroutine and if true then the force is added in the direction
              same ADDFORCE method is used with the forceMode2d.Impulse;
              
TriggerDetection: Uses the OnTriggerEnter2D method to find if the striker managed to strike the pawns in the pocket and then the colliders
                  added to the pockets detects if the pawns got collected in it or not and then destroys it accordingly.
                  
Timer: To start a counter of 2 minutes according to the mentioned.
               
               
