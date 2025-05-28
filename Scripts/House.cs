using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] Alarm _alarm;
    [SerializeField] AlarmArea _alarmArea;

    private void OnEnable()
    {
        _alarmArea.Detected += HendelAlarm;
    }

    private void OnDisable()
    {
        _alarmArea.Detected -= HendelAlarm;
    }

    private void HendelAlarm(int crroksCount)
    {
        if (crroksCount > 0)         
            _alarm.RiseVolum();
        else
            _alarm.DownVolume();       
    }
}