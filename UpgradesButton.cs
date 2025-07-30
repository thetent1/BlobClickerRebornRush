using UnityEngine;

public class UpgradesButton : MonoBehaviour
{
    public GameObject upgradesPanel;

    public void ToggleUpgrades()
    {
        upgradesPanel.SetActive(!upgradesPanel.activeSelf);
    }

    public void Exit()
    {
        upgradesPanel.SetActive(false);
    }

}
