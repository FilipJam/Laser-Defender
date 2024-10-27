using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTime
{
    int[] _totalTime = new int[3];
    readonly int[] _unitLimits = {10, 60};

    public int[] Values { get => _totalTime; set => _totalTime = value; }

    public MyTime() {}
    public MyTime(int[] time) {
        for (int i = 0; i < _totalTime.Length; i++)
        {
            _totalTime[i] = time[i];
        }
    }

    public void AddDeciseconds(int amount) {
        _totalTime[0] += amount;
        AdjustTimeValues(_totalTime);
    }
    public override string ToString() {
        (string ds, string sec, string min) = FormattedTimeValues();
        return string.Format("{0}:{1}.{2}", min, sec, ds);
    }


    void AdjustTimeValues(int[] time) {
        for (int i = 0; i < time.Length; i++)
        {
            if(i == time.Length-1) { return; }

            int carried = time[i] / _unitLimits[i];
            int remainder = time[i] % _unitLimits[i];
            time[i] = remainder;
            time[i+1] += carried;
        }
    }
    (string, string, string) FormattedTimeValues() {
        string[] result = new string[_totalTime.Length];
        for(int i = 0; i < _totalTime.Length; i++) {
            result[i] = i > 0 ? FormatTimeValue(_totalTime[i]) : _totalTime[i].ToString();
        }
        return (result[0], result[1], result[2]);
    }
    string FormatTimeValue(float timeValue) {
        return (timeValue < 10 ? "0" : "") + timeValue;
    }
}
