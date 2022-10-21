# GoWalkies
In this project, we have developed a mobile AR Virtual Pets Game. 

The projects is based on the AR Fuondatioin examlple. 

It contians three sections, ticket, evolution and social ranking part. 
### Ticket
    * This section grants users more rest time when they gain a level.
### Evolution
    * This section provides players with several types of pets, with level one granting little cats, level two granting huge cats, level three granting foxes, and level four granting tigers.
### Social Ranking
    * This part grants players new pet titles and a rating list.

### Reset Position Button -- In the lower right corner of each scene
    * Due to time constraints, we have not yet completed the setting to allow pets to constantly remain in the centre of the scene; hence, players are occasionally unable to locate their pet. As a consequence, we included a reset position button; when players click this button, they may tap the screen to reveal the pets.

## Purpose of this project
* This project's objective is to compare the value of various objects in a loss aversion exergame.
## Unity Version
  * 2022.1.3f1c1


## Preparation

* Download Unity Hub from Unity Website.
* Installs -> Install Editor
* Find the editor you want.
* If you cannot find the unity version in Unity Hub, you can go to Unity Long Term Support Released Website. Find LTS Release 2022.3.1f1.


## How to play the game in Unity
  * Go to  **2. Implementation**, the **MainPage** Folder. 
  * Then open **MainMenu** Scene. 
  * Hit Play button. 


## Notes
* Most useful scripts is in the **2. Implementation** folder, tickets session in the tickets folder, evolution in the evolution folder and social ranking in the social standing folder. All these in the  **2. Implementation** folder. 
* Since this project is developed under Mac OS, if you use windows, you might need to convert some settings, however, I believe you just need to follow the instruction by Unity, Unity will do this part automatically for you.


# Future use
## Unity Part
    * If you want to alter the settings for each session, you need to modify the progressbar script in each scene. The majority of notice panels and progressbars are updated by this script. 
    *  To modify the pet's behavior, just write a script that extends PlaceOnPlane Script and replace PlaceOnPlane Script in the XR origin.