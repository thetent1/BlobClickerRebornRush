using UnityEngine;

public class ClickButton : MonoBehaviour
{
    public RebirthData rebirthData;

    public void OnClick()
    {
        rebirthData.GainEnergy();
    }
}
