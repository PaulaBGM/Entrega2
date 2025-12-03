using System;
using TMPro;
using UnityEngine;

public class ClockUI : MonoBehaviour
{
    [Header("Schedule Settings")]
    [SerializeField] private int startHour = 9;
    [SerializeField] private int startMinute = 0;

    [SerializeField] private int endHour = 17;
    [SerializeField] private int endMinute = 0;

    [Header("Day progress")] // Needs to be stated to work properly
    [SerializeField] private int workDayArtworks = 8; 

    private int _artworksExamined = 0;
    private int _currentTotalMinutes;
    private int _minutesPerArtwork;

    private TMP_Text _clockTMP;

    private void Awake()
    {
        _clockTMP = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        int startMinutes = startHour * 60 + startMinute;
        int endMinutes = endHour * 60 + endMinute;

        int totalMinutes = endMinutes - startMinutes;
        
        _currentTotalMinutes = startMinutes;
        
        _minutesPerArtwork = totalMinutes / workDayArtworks;

        UpdateClock();
    }
    
    public void OnArtworkExamined() // Call this method when an artwork is examined
    {
        if (_artworksExamined >= workDayArtworks)
            return;

        _artworksExamined++;
        AdvanceTime();
    }

    private void AdvanceTime()
    {
        _currentTotalMinutes += _minutesPerArtwork;
        UpdateClock();
    }

    private void UpdateClock()
    {
        int hour = _currentTotalMinutes / 60;
        int minute = _currentTotalMinutes % 60;

        _clockTMP.text = $"{hour:00}:{minute:00}";
    }
}