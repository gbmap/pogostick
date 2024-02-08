using DRAP.Objectives;
using UnityEngine;
using Zenject;

public class TimerLabel : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI label;
    [Inject] LevelTimer timer;

    void Update()
    {
        label.text = Mathf.Max(0f, timer.TimeRemaining).ToString("0.00");
    }
}
