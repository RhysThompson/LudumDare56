using UnityEngine;
using UnityEngine.UI;
public class ScoreMenu : SingletonMenu<ScoreMenu>
{
    public Text MainText;
    public Text SubText;
    public Image Background;
    public override void Open()
    {
        base.Open();
        PauseMenuManager.Instance.PauseWithoutMenu();
    }
    public void SetText(string main, string sub)
    {
        MainText.text = main;
        SubText.text = sub;
    }
    public void SetColor(Color color)
    {
        Background.color = color;
    }
    public void MainMenu()
    {
        SceneSwitcher.Instance.ReturnToMenu();
    }
    public void Restart()
    {
        SceneSwitcher.Instance.RestartScene();
    }
}
