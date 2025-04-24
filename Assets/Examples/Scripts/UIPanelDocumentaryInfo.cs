using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a UI panel that displays documentary information and provides functionality
/// to toggle the visibility of a message with a fade effect.
/// </summary>
[SelectionBase]
[DisallowMultipleComponent]
public sealed class UIPanelDocumentaryInfo : UserInterfaceBehaviour
{
    /// <summary>
    /// The CanvasGroup used to control the visibility and transparency of the message.
    /// </summary>
    [SerializeField]
    private CanvasGroup m_canvasGroupMessage = default;

    /// <summary>
    /// The button used to toggle the visibility of the message.
    /// </summary>
    [SerializeField]
    private Button m_buttonToggleMessage = default;

    /// <summary>
    /// Duration for the fade effect when showing or hiding the message.
    /// </summary>
    [SerializeField]
    private float m_fadeDuration = 0.5f;

    /// <summary>
    /// The coroutine used to handle the fade effect.
    /// </summary>
    private Coroutine m_coroutineFade;

    /// <summary>
    /// Indicates whether the message is currently visible.
    /// </summary>
    private bool m_isShowing;

    /// <summary>
    /// Default constructor for UIPanelDocumentaryInfo.
    /// </summary>
    public UIPanelDocumentaryInfo()
    {
        m_coroutineFade = null;
        m_isShowing = false;
    }

    /// <summary>
    /// Called when the script instance is being loaded. Sets up the button click listener.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        if (m_buttonToggleMessage != null)
        {
            m_buttonToggleMessage.onClick.AddListener(OnButtonToggleMessageClicked);
        }
    }

    protected override void Start()
    {
        base.Start();
        if (m_canvasGroupMessage != null)
        {
            m_canvasGroupMessage.alpha = 0f;
            m_canvasGroupMessage.interactable = false;
            m_canvasGroupMessage.blocksRaycasts = false;
        }
    }

    private void Update()
    {
        // Check if the target panel example is not null.
        if (m_canvasGroupMessage == null)
        {
            return;
        }
        // For testing purposes, we can use the space key to toggle show and hide the panel.
        if (Input.GetKeyDown(KeyCode.I))
        {
            OnButtonToggleMessageClicked();
        }
    }

    /// <summary>
    /// Called when the behavior becomes disabled. Cleans up listeners and stops any active coroutines.
    /// </summary>
    protected override void OnDisable()
    {
        if (m_buttonToggleMessage != null)
        {
            m_buttonToggleMessage.onClick.RemoveListener(OnButtonToggleMessageClicked);
        }
        if (m_coroutineFade != null)
        {
            StopCoroutine(m_coroutineFade);
            m_coroutineFade = null;
        }
        base.OnDisable();
    }

    /// <summary>
    /// Handles the button click event to toggle the visibility of the message.
    /// </summary>
    private void OnButtonToggleMessageClicked()
    {
        if (m_coroutineFade != null)
        {
            StopCoroutine(m_coroutineFade);
            m_coroutineFade = null;
        }
        m_isShowing = !m_isShowing;
        m_coroutineFade = StartCoroutine(CoroutineFade(m_isShowing ? 1f : 0f, m_fadeDuration));
    }

    /// <summary>
    /// Coroutine to fade the CanvasGroup to the specified alpha value over a given duration.
    /// </summary>
    /// <param name="alpha">The target alpha value.</param>
    /// <param name="duration">The duration of the fade effect in seconds.</param>
    /// <returns>An enumerator for the coroutine.</returns>
    private IEnumerator CoroutineFade(float alpha, float duration)
    {
        if (m_canvasGroupMessage == null)
        {
            yield break;
        }
        float startAlpha = m_canvasGroupMessage.alpha;
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            m_canvasGroupMessage.alpha = Mathf.Lerp(startAlpha, alpha, time / duration);
            yield return null;
        }
        m_canvasGroupMessage.alpha = alpha;
        m_coroutineFade = null;
    }
}
