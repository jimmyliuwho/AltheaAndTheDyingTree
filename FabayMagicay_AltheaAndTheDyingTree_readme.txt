i. Start scene file
    Menu

ii. How to play and what parts of the level to observe technology requirements
    
    How to Play
        Use the menu buttons to start the game.
        Use esc in a level to pause and unpause, and press H to view help message.
        Walk using the arrow keys or wasd.
        Switch between elements by going into the different powers: 
            a spiky sun for light power, water droplet for water power, and a whirlwind for air power.
        Click to fire a projectile when possessing light power.
        Light projectiles can grow plants.
        Light barriers can only be crossed while possessing light power.
        Rivers can only be crossed while possessing water power.
        Player can high jump while possessing air power, or can do a small jump otherwise.
        Pinecones are collectibles but are not required to complete the level.
        Don't fall off the tree or hit an enemy!
    
    Technology Requirements
	Level 1 - can observe the sun power, light barrier, vines and flowers growing
	Level 2 - can observe the wind power, pinecones
	Level 3 - can observe enemies, water power, river obstacle


iii. Known problem areas
enemies in level 7 get occasionally stuck at waypoints
did not implement shooting animation of player character
End Door of level 2 requires player to enter door through the side, for some reason the player cannot walk through it straight on

iv. Manifest of which files authored by each teammate:

    Carson Anderson: 
        - character states for element powers and corresponding abilities based on element
        - projectiles shooting and collisions
        - camera movement around tree, player movement around tree (circular)
        - Player death if falls off tree
        - Scripts:
            - PlayerController.cs (vast majority - 450 lines)
            - CameraFollowPlayer.cs
            - pProjectileController.cs
            - GameManager.cs

    Aparna Arul:
        - pinecone collectibles and ability to collect
        - HUD to display current power and number of pinecones collected
        - start menu, credits page, level complete menu, pause menu and functionality, death menu and functionality, win screen
        - debugging with flower growth, player movement and animation, enemyAI, enemy death
        - Scripts:
            - PlayerController.cs
            - BackToMenu.cs
            - Credits.cs
            - DoorScript.cs
            - GameQuitter.cs
            - GameStarter.cs
            - GameStarterNextScene.cs
            - Pause.cs
            - playerDeath.cs
            - Resume.cs
            - Rotator.cs
            - ScrollingCredits.cs
            - ToCredits.cs
            - AudioManager.cs

    Kathryn Carlson:
        - level design
        - character abilities based on element powers
        - sound
        - tutorial text
        - final level projectile and animation
        - debugging with player movement and animation
        - Scripts:
            - PlayerController.cs
            - AudioManager.cs
            - ElementPower.cs
            - enemyStomp.cs
            - footsteps.cs
            - Sound.cs

    Madison Kim:
        - player animation states, blend tree, player movement
        - enemy animation and AI
        - player death & enemy death
        - Scripts:
            - PlayerController.cs
            - AnimationStateController.cs
            - enemyAI.cs
            - enemyDestroy.cs
            - playerDeath.cs
            - TriggerDeath.cs
            - TwoDimensionalAnimationStateController.cs

    Jimmy Liu:
        - special effects, visual effects with light barrier, and river
        - special effects, visual effects with projectiles and power enablers
        - modeling flowers and flower growth based on projectiles
        - modeling vines and vine growth
        - whirlwind effect when jumping
        - UI for transitioning between different scenes
        - Scripts:
            - PlayerController.cs
            - GrowVinesCollision.cs
            - GrowVinesScript.cs
            - TriggerGrowth.cs
            - pProjectileController.cs
- playerDeath.cs
- DoorScript.cs

v. Assets
Models Used

https://sketchfab.com/3d-models/hand-painted-low-poly-pine-trees-b02e45ccdb28453898daea5753942a7d

https://seamless-pixels.blogspot.com/2012/09/free-seamless-ground-textures.html

"Low Poly Rocks" (https://skfb.ly/6CIuv) by Michael Hooper is licensed under Creative Commons Attribution (http://creativecommons.org/licenses/by/4.0/).

https://sketchfab.com/3d-models/star-wars-low-poly-hoth-skybox-caddf0ec811a47d5a2449a4d076a9d7b#download

https://assetstore.unity.com/packages/3d/vegetation/flowers/fantasy-mushroom-flower-226969

https://sketchfab.com/3d-models/pinecone-9ad231a5335b453e90bda43c3d35e1a4

https://www.mixamo.com/#/?page=1&query=warrok&type=Character

https://www.mixamo.com/#/?page=1&query=jolleen&type=Character

https://www.cgtrader.com/items/261332/download-page

https://assetstore.unity.com/packages/3d/environments/simple-forest-pack-209273

Sounds
https://www.youtube.com/watch?v=jyGOSahyy3E
https://www.youtube.com/watch?v=Ca1mS-cC7W8
https://www.youtube.com/watch?v=JRZYh-ZO9HY
https://www.youtube.com/watch?v=O5Y1QGG0q90
https://www.youtube.com/watch?v=6Z3gPfwb-SI

Free  Sound.org

Sound # 166316 by deleted_user_2104797 water_drop_2.wav
Sound # 174638 by altfuture leather-stretched.wav
Sound # 221683 by timbre another-magic-wand-spell-tinkle.flac
Sound # 249819 by spookymodem magic-smite.wav
Sound # 353194 by inspectorj wind-chimes-a.wav
Sound # 398720 by inspectorj water-swirl-small-23.wav
Sound # 460658 by sergequadrado story-logo.wav
Sound # 536109 by eminyildirim water-bubble.wav
Sound # 562196 by gristi snd_fragment_retrieve.wav
Sound # 613163 by  sonically_sound magic.flac
Sound # 625716 by sonicallysound shiny-object.flac
Sound # 645710 by mikkelcayce pull-stretch.wav
Sound # 660892 by xcreenplay shining-light.wav
Sound # 662316 by arodlru2018 latex-glove-stretching.wav
Sound # 181253 by mario1298 a-gentle-breeze-wind-4.wav


