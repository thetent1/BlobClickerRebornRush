using UnityEngine;

public class StatsButton : MonoBehaviour
{
    public GameObject statsPanel;

    public void ToggleStats()
    {
        statsPanel.SetActive(!statsPanel.activeSelf);
    }

    public void Exit()
    {
        statsPanel.SetActive(false);
    }
}
