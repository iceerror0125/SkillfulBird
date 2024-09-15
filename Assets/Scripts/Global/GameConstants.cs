using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants
{
    public static int DEFAULT_PIPE_SPEED = -5;
    public static float DEFAUT_GRAVITY = 5;

    // Skill spawn
    public static int SKILL_SPAWN_RATE = 100;
    public static bool SKILL_SPAWNED;
    public static int SKILL_SPAWN_AFTER_PIPES = 5; // test: 5, origin: 10

    // Scene
    public static readonly string GAME_PLAY_SCENE = "GamePlay";
    public static readonly string MAIN_MENU_SCENE = "MainMenu";
}