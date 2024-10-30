using System;

[Serializable]
public class Save{
    public int Highscore;
    public MyTime LongestRun;

    public Save() {
        LongestRun = new MyTime();
    }
    public Save(int highscore, int[] time) {
        Highscore = highscore;
        LongestRun = new MyTime(time);
    }
}