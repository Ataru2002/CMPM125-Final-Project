using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;

public class RecordDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] recordLabels;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        for (int i = 0; i < recordLabels.Length; i++)
        {
            float value = GameManager.Instance.GetRecordTime(i + 1);
            int seconds = Mathf.FloorToInt(value);
            int centiseconds = (int)(100 * value) % 100;
            string timeString = value > 0 ? string.Format("{0:00}'{1:00}\"", seconds, centiseconds) : "Not yet cleared";
            recordLabels[i].text = "Best Clear Time: " + timeString;
        }
    }
}
