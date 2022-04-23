using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    // ** DATETIME 
    private static int _turnNumber;
    public static int TurnNumber
    {
        get => _turnNumber;
        set
        {
            _turnNumber = value;

            CalendarDay += 1;
            OnTurnNumberChange?.Invoke(_turnNumber);
        }
    }

    private static int daysInMonth = 30;
    private static int monthsInYear = 4;

    // TODO: Enums?
    private static int _calendarMonth;
    public static int CalendarMonth
    {
        get => _calendarMonth;
        set
        {
            // TODO: We've set this to four months, real months????
            _calendarMonth = value % monthsInYear;
            OnCalendarMonthChange?.Invoke(_calendarMonth);
        }
    }

    private static int _calendarDay;
    public static int CalendarDay
    {
        get => _calendarDay;
        set
        {
            // TODO: Make this follow real months?????
            if (value >= daysInMonth)
            {
                CalendarMonth += 1;
            }

            _calendarDay = value % daysInMonth;
            OnCalendarDayChange?.Invoke(_calendarDay);
        }
    }

    // The following take the new value as int.
    public static event Action<int> OnTurnNumberChange;
    public static event Action<int> OnCalendarMonthChange;
    public static event Action<int> OnCalendarDayChange;

    // ** PLAYER ACTIONS
    private ActionType _actionTypeChoice;
    public ActionType ActionTypeChoice
    {
        get => _actionTypeChoice;
        set
        {
            _actionTypeChoice = value;
            OnActionTypeChoice?.Invoke(_actionTypeChoice);
        }
    }

    public static event Action<ActionType> OnActionTypeChoice;

    private void Start()
    {
        SetOnClickForOptionButtons();
    }

    public void SetOnClickForOptionButtons()
    {
        // TODO: NO HARDCODED PATHS.
        GameObject options_panel = GameObject.Find("HUD/Right_Panel/Options_Panel");
        Button[] optionButtons = options_panel.GetComponentsInChildren<Button>();
        foreach (Button btn in optionButtons)
        {
            btn.onClick.AddListener(() => SetActionTypeOnClick($"{btn.gameObject.name}"));
        }
    }

    public void SetActionTypeOnClick(string actionTypeStr)
    {
        switch (actionTypeStr)
        {
            case "Rest":
                ActionTypeChoice = ActionType.Rest;
                break;

            case "Work":
                ActionTypeChoice = ActionType.Work;
                break;

            case "Study":
                ActionTypeChoice = ActionType.Study;
                break;

            case "Travel":
                ActionTypeChoice = ActionType.Travel;
                break;

            default:
                break;
        }
    }
}
