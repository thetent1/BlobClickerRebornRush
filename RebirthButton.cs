using UnityEngine;
using UnityEngine.UI;

public class RebirthButton : MonoBehaviour
{
    public RebirthData rebirthData;
    public Button rebirthButton;
    public Image buttonImage;
    public Color normalColor = Color.white;
    public Color disabledColor = new Color(0.27f, 0.27f, 0.27f);

    void Update()
    {
        bool canRebirth = rebirthData.CanRebirth();
        rebirthButton.interactable = canRebirth;
        buttonImage.color = canRebirth ? normalColor : disabledColor;
    }

    public void OnClick()
    {
        if (rebirthData.CanRebirth())
            rebirthData.DoRebirth();
    }
}
