using System;
using System.Collections.Generic;

public static class GameManager {
    public static int numberJumps = 0;
    public static int numberDeaths = 0;
    public static bool isPaused = false;
    public static float remainingTime = 0;
    public static bool needLine = false; 

    public static int currentLevel = 1;
    public static Dictionary<int, Dictionary<string, int>> levelInfo = new Dictionary<int, Dictionary<string, int>>{
        {1, new Dictionary<string, int>{{"unlocked", 1}, {"time", 30}, {"jumpsR", -1}, {"deathsR", -1}, {"timeR", -1}}},
        {2, new Dictionary<string, int>{{"unlocked", 0}, {"time", 40}, {"jumpsR", -1}, {"deathsR", -1}, {"timeR", -1}}},
        {3, new Dictionary<string, int>{{"unlocked", 0}, {"time", 50}, {"jumpsR", -1}, {"deathsR", -1}, {"timeR", -1}}},
        {4, new Dictionary<string, int>{{"unlocked", 0}, {"time", 60}, {"jumpsR", -1}, {"deathsR", -1}, {"timeR", -1}}},
        {5, new Dictionary<string, int>{{"unlocked", 0}, {"time", 70}, {"jumpsR", -1}, {"deathsR", -1}, {"timeR", -1}}},
    };

    public static void ResetCurrentStats(){
        numberJumps = 0;
        numberDeaths = 0;
        isPaused = false;
        needLine = false;
    }
}


