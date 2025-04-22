using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class for the UI panel example using DOTween tweens.
/// </summary>
public sealed class UIPanelExampleDOTweenTween : UIPanelExampleBase
{
    /// <summary>
    /// The canvas group of the panel.
    /// </summary>
    [SerializeField]
    private CanvasGroup m_canvasGroup = default;

    /// <summary>
    /// The rect transform of the context.
    /// </summary>
    [SerializeField]
    private RectTransform m_rectTransformContext = default;

    /// <summary>
    /// The offset position of the context when hidden.
    /// </summary>
    [SerializeField]
    private Vector3 m_contextOffsetPosition = new Vector3(0f, 300f, 0f);

    /// <summary>
    /// The duration of the tweening effect.
    /// </summary>
    [SerializeField]
    private float m_duration = 0.5f;

    /// <summary>
    /// Collection of DOTween tweens.
    /// </summary>
    private readonly List<Tween> m_tweens;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public UIPanelExampleDOTweenTween()
    {
        m_tweens = new List<Tween>();
    }

    public override bool Show()
    {
        if (!base.Show())
        {
            return false;
        }
        if (m_canvasGroup == null || m_rectTransformContext == null)
        {
            return false;
        }
        StopAllTweening();
        // Set the canvas group to be interactable and block raycasts.
        m_canvasGroup.interactable = true; // Enable interaction
        m_canvasGroup.blocksRaycasts = true; // Enable raycasting
        // Create a tween for the canvas group alpha.
        Tween alphaTween = m_canvasGroup.DOFade(1f, m_duration);
        // Set ease function for the alpha tween.
        alphaTween.SetEase(Ease.OutExpo);
        // Reset the tween when it's complete.
        alphaTween.OnComplete(() =>
        {
            alphaTween.Kill(); // Kill the tween for release resources
            m_tweens.Remove(alphaTween); // Remove the tween from the list
        });
        // Add the tween to the list of tweens.
        m_tweens.Add(alphaTween);
        // Play the alpha tween.
        alphaTween.Play();
        // Create a tween for the context position.
        Tween positionTween = m_rectTransformContext.DOLocalMove(Vector3.zero, m_duration);
        // Set ease function for the position tween.
        positionTween.SetEase(Ease.OutBounce);
        // Reset the tween when it's complete.
        positionTween.OnComplete(() =>
        {
            positionTween.Kill(); // Kill the tween for release resources
            m_tweens.Remove(positionTween); // Remove the tween from the list
        });
        // Add the tween to the list of tweens.
        m_tweens.Add(positionTween);
        // Play the position tween.
        positionTween.Play();
        return true;
    }

    public override bool Hide()
    {
        if (!base.Hide())
        {
            return false;
        }
        if (m_canvasGroup == null || m_rectTransformContext == null)
        {
            return false;
        }
        StopAllTweening();
        // Set the canvas group to be non-interactable and not block raycasts.
        m_canvasGroup.interactable = false; // Disable interaction
        m_canvasGroup.blocksRaycasts = false; // Disable raycasting
        // Create a tween for the canvas group alpha.
        Tween alphaTween = m_canvasGroup.DOFade(0f, m_duration);
        // Set ease function for the alpha tween.
        alphaTween.SetEase(Ease.OutExpo);
        // Reset the tween when it's complete.
        alphaTween.OnComplete(() =>
        {
            alphaTween.Kill(); // Kill the tween for release resources
            m_tweens.Remove(alphaTween); // Remove the tween from the list
        });
        // Add the tween to the list of tweens.
        m_tweens.Add(alphaTween);
        // Play the alpha tween.
        alphaTween.Play();
        // Create a tween for the context position.
        Tween positionTween = m_rectTransformContext.DOLocalMove(m_contextOffsetPosition, m_duration);
        // Set ease function for the position tween.
        positionTween.SetEase(Ease.OutCirc);
        // Reset the tween when it's complete.
        positionTween.OnComplete(() =>
        {
            positionTween.Kill(); // Kill the tween for release resources
            m_tweens.Remove(positionTween); // Remove the tween from the list
        });
        // Add the tween to the list of tweens.
        m_tweens.Add(positionTween);
        // Play the position tween.
        positionTween.Play();
        return true;
    }

    protected override void Awake()
    {
        base.Awake();
        // Check if the canvas group is not null.
        if (m_canvasGroup != null)
        {
            m_canvasGroup.interactable = false; // Disable interaction
            m_canvasGroup.blocksRaycasts = false; // Disable raycasting
            m_canvasGroup.alpha = 0f; // Set the initial alpha to 0
        }
        // Check if the rect transform context is not null.
        if (m_rectTransformContext != null)
        {
            m_rectTransformContext.localPosition = m_contextOffsetPosition; // Set the initial position
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        StopAllTweening();
    }

    /// <summary>
    /// Stop all coroutines.
    /// </summary>
    private void StopAllTweening()
    {
        foreach (var tween in m_tweens)
        {
            tween?.Kill();
        }
        m_tweens.Clear();
    }
}
