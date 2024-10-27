using System;

[Serializable]
public class Save{

    public int Highscore;
    public int[] LongestRun = new int[3];
    public Save() {}
    public Save(int highscore, int[] time) {
        Highscore = highscore;
        LongestRun = time;
    }

    
}