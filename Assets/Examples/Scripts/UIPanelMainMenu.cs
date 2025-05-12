using LuviKunG.Tools;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public struct MainMenuButtonInfo
{
    public string text;
    [StringScene]
    public string scenePath;
}

public sealed class UIPanelMainMenu : UserInterfaceBehaviour
{
    [SerializeField]
    [StringScene]
    private MainMenuButtonInfo[] m_scenes = default;

    [SerializeField]
    private UIPanelMainMenuButton m_prefabButton = default;

    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < m_scenes.Length; i++)
        {
            UIPanelMainMenuButton button = Instantiate(m_prefabButton, m_prefabButton.transform.parent);
            button.SetInfo(m_scenes[i]);
            int sceneIndex = i;
            button.onClick.AddListener(() => SceneManager.LoadScene(m_scenes[sceneIndex]));
        }
    }
}

public sealed class UIPanelMainMenuButton : UserInterfaceBehaviour
{
    public delegate void OnClickHandler(MainMenuButtonInfo info);

    [SerializeField]
    private Button m_button = default;
    [SerializeField]
    private TextMeshProUGUI m_text = default;

    public MainMenuButtonInfo info;
    public OnClickHandler onClick;

    public void SetInfo(MainMenuButtonInfo info)
    {
        this.info = info;
        m_text.text = info.text;
    }
}