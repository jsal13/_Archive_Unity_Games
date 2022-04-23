using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CalendarController : MonoBehaviour
{
    [SerializeField] private TMP_Text monthLabel;
    [SerializeField] private TMP_Text dayLabel;

    private List<string> monthNames = new List<string>() {
        "Spring",
        "Summer",
        "Autumn",
        "Winter"
    };

    private void OnEnable()
    {
        TurnManager.OnCalendarMonthChange += HandleCalendarMonthChange;
        TurnManager.OnCalendarDayChange += HandleCalendarDayChange;
    }

    private void OnDisable()
    {
        TurnManager.OnCalendarMonthChange -= HandleCalendarMonthChange;
        TurnManager.OnCalendarDayChange -= HandleCalendarDayChange;
    }

    private void HandleCalendarDayChange(int val)
    {
        // Translate 0-index to 1-index for day numbers.
        dayLabel.text = (val + 1).ToString().PadLeft(2, '0');
    }

    private void HandleCalendarMonthChange(int val)
    {
        monthLabel.text = monthNames[val];
    }
}
