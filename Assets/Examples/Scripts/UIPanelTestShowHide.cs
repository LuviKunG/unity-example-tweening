using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI Panel to test the UI Panel Example to show and hide.
/// </summary>
[SelectionBase]
[DisallowMultipleComponent]
public sealed class UIPanelTestShowHide : UserInterfaceBehaviour
{
    /// <summary>
    /// The target panel example to show and hide.
    /// </summary>
    [SerializeField]
    private UIPanelExampleBase m_targetPanelExample = default;

    /// <summary>
    /// The UI button to show the panel.
    /// </summary>
    [SerializeField]
    private Button m_buttonShow = default;
    /// <summary>
    /// The UI button to hide the panel.
    /// </summary>
    [SerializeField]
    private Button m_buttonHide = default;

    protected override void Awake()
    {
        base.Awake();
        // Check if the target panel example is not null.
        if (m_targetPanelExample == null)
        {
            Debug.LogError("Target panel example is not assigned.", this);
            return;
        }
        // Check if the buttons are not null.
        if (m_buttonShow != null)
        {
            m_buttonShow.onClick.AddListener(OnClickShow);
        }
        if (m_buttonHide != null)
        {
            m_buttonHide.onClick.AddListener(OnClickHide);
        }
    }

    protected override void Start()
    {
        base.Start();
        // Update the button state based on the initial state of the target panel example.
        UpdateButtonState();
    }

    private void Update()
    {
        // Check if the target panel example is not null.
        if (m_targetPanelExample == null)
        {
            return;
        }
        // For testing purposes, we can use the space key to toggle show and hide the panel.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_targetPanelExample.IsShowing())
            {
                _ = m_targetPanelExample.Hide();
            }
            else
            {
                _ = m_targetPanelExample.Show();
            }
            UpdateButtonState();
        }
    }

    /// <summary>
    /// Update the state of the buttons based on the target panel example's state.
    /// </summary>
    private void UpdateButtonState()
    {
        if (m_buttonShow != null)
        {
            m_buttonShow.interactable = !m_targetPanelExample.IsShowing();
        }
        if (m_buttonHide != null)
        {
            m_buttonHide.interactable = m_targetPanelExample.IsShowing();
        }
    }

    /// <summary>
    /// (Delegate) Called when the show button is clicked.
    /// </summary>
    private void OnClickShow()
    {
        if (m_targetPanelExample == null)
        {
            return;
        }
        _ = m_targetPanelExample.Show();
        UpdateButtonState();
    }

    /// <summary>
    /// (Delegate) Called when the hide button is clicked.
    /// </summary>
    private void OnClickHide()
    {
        if (m_targetPanelExample == null)
        {
            return;
        }
        _ = m_targetPanelExample.Hide();
        UpdateButtonState();
    }
}
