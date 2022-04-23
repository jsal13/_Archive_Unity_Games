using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class DateManager
{
    private static readonly Lazy<DateManager> lazy =
        new Lazy<DateManager>(() => new DateManager());
    public static DateManager Instance { get { return lazy.Value; } }
    private DateManager() { }

    public static Action OnDateChange;

    public static List<string> seasons = Constants.seasons;
    public static string season;
    private static int _seasonIDX = 0;
    public static int SeasonIDX
    {
        get => _seasonIDX;
        set
        {
            _seasonIDX = value % Constants.seasons.Count;
            season = seasons[_seasonIDX];
            OnDateChange?.Invoke();
        }
    }

    private static int _day = 0; // day of season.
    public static int Day
    {
        get => _day;
        set
        {
            if (value >= Constants.seasonLength)
            {
                _day = value % Constants.seasonLength;
                SeasonIDX += 1;
            }
            else
            {
                _day = value;
                SeasonIDX += 0;
            }

            OnDateChange?.Invoke();
        }
    }
}