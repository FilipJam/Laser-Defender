using System;

[Serializable]
public class MyTime
{
    int[] _totalTime = new int[3];
    readonly int[] _timeThresholds = {10, 60};

    public int[] Values { get => _totalTime; set => _totalTime = value; }

    public MyTime() {}
    public MyTime(int[] time) {
        _totalTime = time;
    }

    public void AddDeciseconds(int amount) {
        _totalTime[0] += amount;
        // if value exceeds time threshold, convert to next and set value to remainder
        AdjustTimeValues(_totalTime);
    }

    // overriden to display the time instead
    public override string ToString() {
        // assigns formatted values to variables so it's easier to know what each value is representing
        (string ds, string sec, string min) = FormattedTimeValues();
        return string.Format("{0}:{1}.{2}", min, sec, ds);
    }

    // generalised to process values correctly when adding many deciseconds that exceed time threshold
    void AdjustTimeValues(int[] time) {
        for (int i = 0; i < time.Length; i++)
        {
            // minutes can go over 60 as there's no hours, so no point continuing
            if(i == time.Length-1) { return; }

            // convert to next time unit (e.g. sec to min = sec / 60)
            int carried = time[i] / _timeThresholds[i];
            // calculate remainder of conversion (e.g. seconds that couldn't fit into a minute)
            int remainder = time[i] % _timeThresholds[i];
            
            // assign remainder to current value(e.g. seconds)
            time[i] = remainder;
            // assign carried to next value(e.g. minutes)
            time[i+1] += carried;
        }
    }
    (string, string, string) FormattedTimeValues() {
        string[] result = new string[_totalTime.Length];
        // minutes don't need to be formatted
        result[0] = _totalTime[0].ToString();
        for(int i = 1; i < _totalTime.Length; i++) {
            // formats the values to look like digital time
            result[i] = FormatTimeValue(_totalTime[i]);
        }
        // returns tuple so it's easier to work with
        return (result[0], result[1], result[2]);
    }
     string FormatTimeValue(float timeValue) {
        // adds a leading zero if less than 10
        return (timeValue < 10 ? "0" : "") + timeValue;
    }
}
