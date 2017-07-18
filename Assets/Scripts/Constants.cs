using UnityEngine;
using System.Collections;

// Class used to hold various game constants, static functions,
// indexes of arrays held by Gameobjects returned by methods such as GetComponents and GetComponentsInChildren
public sealed class Constants : MonoBehaviour {
	// Game constants
	public static readonly float SLIDER_VALUE_MAX = 100.0f;     // All sliders are valued from 0.0 to 100.0
	public static readonly float SLIDER_VALUE_DEFAULT = 50.0f;    
	public static readonly float VOLUME_VALUE_DEFAULT = 0.5f;     // All volume is measured from 0.0 to 1.0
	public static readonly int AMMO_LOWDIVISOR = 5;      // Once the ammo becomes 1/5 of its magazine size, it is considered "low"
	public static readonly int AMMO_MULTIPLIER = 3;       // Determines the starting ammo based on the magazine size
	public static readonly int MAX_NUMENEMIES = 24;      // The maximum number of enemies that can be alive at one time- to prevent overflow and game balance
	public static readonly int STARTINGLIVES = 3;
	public static readonly float TIME_FLASH_SCREEN = 5.0f;     // For damage being taken
	public static readonly float TIME_FLASH_MUZZLE = 0.2f;     // For shooting
	public static readonly float LENGTH_CAMRAY = 100.0f;

	// AmmoUI has two Images in its children
	// The first is its Background
	// The second is its Fill
	public static readonly int INDEX_AMMOUI_IMG_BACK = 0;
	public static readonly int INDEX_AMMOUI_IMG_FILL = 1;

	// AmmoUI has two Texts in its children
	// The first is its ammo count
	// The second is its gun
	public static readonly int INDEX_AMMOUI_TXT_COUNT = 0;
	public static readonly int INDEX_AMMOUI_TXT_GUN = 1;

	// Next Level contains Texts for its various UI elements 
	// The index of each is below
	public static readonly int INDEX_NEXT_TXT_LIVES = 0;
	public static readonly int INDEX_NEXT_TXT_NOTIF = 1;
	public static readonly int INDEX_NEXT_TXT_SCORE_DISP = 2;
	public static readonly int INDEX_NEXT_TXT_SCORE_PLACE = 3;
	public static readonly int INDEX_NEXT_TXT_SCORE_INPUT = 4;

	// The EnemyManager script holds an array of enemies to be spawned,
	// and Score contains multiple Texts, each corresponding to the amount of an enemy killed
	// The index of each type of enemy is below
	public static readonly int INDEX_ENEMY_ZOMBUNNY = 0;
	public static readonly int INDEX_ENEMY_ZOMBEAR = 1;
	public static readonly int INDEX_ENEMY_HELLEPHANT = 2;

	// The PlayerGun script holds an array of Materials that correspond to the different guns
	// The index of each Material is below
	public static readonly int INDEX_MATERIAL_SMG = 0;
	public static readonly int INDEX_MATERIAL_SHOTGUN = 1;
	public static readonly int INDEX_MATERIAL_LMG = 2;
	public static readonly int INDEX_MATERIAL_SNIPER = 3;
	public static readonly int INDEX_MATERIAL_ASSAULTRIFLE = 4;

	// PlayerGun has two SkinnedMeshRenderers in its children
	// The first is the gun's
	// The second is the player's
	public static readonly int INDEX_REND_GUN = 0;
	public static readonly int INDEX_REND_PLAYER = 1;

	// GunBarrelEnd has seven possible gun sounds that can be played
	// The index of each AudioSource is below
	public static readonly int INDEX_AUDIO_NOAMMO = 0;
	public static readonly int INDEX_AUDIO_RELOADING = 1;
	public static readonly int INDEX_AUDIO_SMG= 2;
	public static readonly int INDEX_AUDIO_SHOTGUN = 3;
	public static readonly int INDEX_AUDIO_LMG = 4;
	public static readonly int INDEX_AUDIO_SNIPER = 5;
	public static readonly int INDEX_AUDIO_ASSAULTRIFLE = 6;

	// Player has four possible sounds that can be played
	// The index of each AudioSource is below
	public static readonly int INDEX_AUDIO_PLAYER_HURT = 0;
	public static readonly int INDEX_AUDIO_PICKUP_GUN = 1;
	public static readonly int INDEX_AUDIO_PICKUP_AMMO = 2;
	public static readonly int INDEX_AUDIO_PLAYER_DEATH = 3;

	// The End Game has four Texts in its Canvas
	// The index of each is below
	public static readonly int INDEX_END_BANNER = 0;
	public static readonly int INDEX_END_SCORE = 1;
	public static readonly int INDEX_END_THANK = 2;

	// PickupManager holds an array of pickups that can be spawned
	// The index of each is below
	public static readonly int INDEX_PICKUP_AMMO = 0;
	public static readonly int INDEX_PICKUP_SMG = 1;    
	public static readonly int INDEX_PICKUP_SHOTGUN = 2;     
	public static readonly int INDEX_PICKUP_LMG = 3;     
	public static readonly int INDEX_PICKUP_SNIPER = 4;     
	public static readonly int INDEX_PICKUP_ASSAULTRIFLE = 5;   

	// PickupManager holds an array of spawn locations that pickups can be spawned at
	// The index of each is below
	public static readonly int INDEX_SPAWN_WEAPON1 = 0;
	public static readonly int INDEX_SPAWN_WEAPON2 = 1;
	public static readonly int INDEX_SPAWN_WEAPON3 = 2;
	public static readonly int INDEX_SPAWN_AMMO1 = 3;
	public static readonly int INDEX_SPAWN_AMMO2 = 4;
	public static readonly int INDEX_SPAWN_AMMO3 = 5;

	// HealthUI contains 11 hearts that correspond to a certain health range and two extra hearts to represent extra lives
	// The number in the constant name is the minimum threshold that the health
	// can drop to in order to active that heart
	// For example,  HP_50 corresponds to a health level that has dipped below 50% HP(hit points)
	// The index of each is below
	public static readonly int INDEX_HP_100 = 0;
	public static readonly int INDEX_HP_90 = 1;
	public static readonly int INDEX_HP_80 = 2;
	public static readonly int INDEX_HP_70 = 3;
	public static readonly int INDEX_HP_60 = 4;
	public static readonly int INDEX_HP_50 = 5;
	public static readonly int INDEX_HP_40 = 6;
	public static readonly int INDEX_HP_30 = 7;
	public static readonly int INDEX_HP_20 = 8;
	public static readonly int INDEX_HP_10 = 9;
	public static readonly int INDEX_HP_0 = 10;
	// The indexes for the extra hearts representing lives remaining
	public static readonly int INDEX_HEART_EXTRA1 = 11;
	public static readonly int INDEX_HEART_EXTRA2 = 12;

	// HUD Canvas contains Texts for its various UI elements
	// The index of each is below
	public static readonly int INDEX_HUD_TXT_AMMOCOUNT = 0;
	public static readonly int INDEX_HUD_TXT_GUN = 1;
	public static readonly int INDEX_HUD_TXT_ZOMBUNNUM = 2;
	public static readonly int INDEX_HUD_TXT_ZOMBEARNUM = 3;
	public static readonly int INDEX_HUD_TXT_HELLENUM = 4;
	public static readonly int INDEX_HUD_TXT_GAMEOVER = 5;
	public static readonly int INDEX_HUD_TXT_OBJECTIVE = 6;

	// The last seven characters of newly instantiated object is "(Clone)"
	// As a result, that section of the string is omitted when parsing the GameObject's name
	public static string parseName(string input) {
		return input.Substring(0, input.Length - 7); 
	}
}
