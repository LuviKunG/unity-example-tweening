using UnityEngine;

/// <summary>
/// The abstract base class for UI panel examples.
/// </summary>
[SelectionBase]
[DisallowMultipleComponent]
public abstract class UIPanelExampleBase : UserInterfaceBehaviour
{
    /// <summary>
    /// Indicates whether the panel is currently showing or not.
    /// </summary>
    private bool m_isShowing;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public UIPanelExampleBase()
    {
        m_isShowing = false;
    }

    /// <summary>
    /// Show the panel.
    /// </summary>
    /// <returns>True if the panel was shown, false if it was already showing.</returns>
    public virtual bool Show()
    {
        if (m_isShowing)
        {
            return false;
        }
        m_isShowing = true;
        return true;
    }

    /// <summary>
    /// Hide the panel.
    /// </summary>
    /// <returns>True if the panel was hidden, false if it was already hidden.</returns>
    public virtual bool Hide()
    {
        if (!m_isShowing)
        {
            return false;
        }
        m_isShowing = false;
        return true;
    }
}
