Instructions: 
WASD to move, Space to dash, Esc to pause, left click to shoot (you will need to click fast)
Waves of enemies will attack you, and you need to use your laser to fend them off. As you accumulate kills, you will be prompted to select Upgrades which improve you stats so you can keep going.

Alpha Report:
We got the core features of the game working. Enemies attack in waves, and as the player destroys them, they accumulate more and more powerups to make future waves more manageable. We have a variety of enemy types with there own AIs including mini bosses.

We still need a lot of polish on visuals and especially with the weapon.
These are the main issues we found testing:
-Hit Detection for the laser was inconsistent.
-The collider that medics use to heal allies also takes collision input from the player laser, making them significantly easier to kill. Fiddling with layers/tags/code should fix this but I'm not sure just yet how
-Wave spawner spawns all at once: not game breaking but not intended behavior
-Laser is very short-ranged and seems to go into the ground sometimes
-Players often accidentally buy upgrades when spamming lasers (need to add full auto)
-The laser raycast points to the edge of enemies' hitbox, particularly for larger enemies


While we have the features we want, the game feels clunky to actually play, and there is a lot to work on. One of the biggest areas we are lacking is feedback to player actions. There are no sound effects, and no visual effects to indicate successful kills, which makes gameplay feel unrewarding. There is no knockback applied to enemies on contact with the player either.

These changes, and improved visuals, will be our next steps to bring this game closer to what we want it to be.