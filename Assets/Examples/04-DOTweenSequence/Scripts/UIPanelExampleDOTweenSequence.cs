using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class for the UI panel example using DOTween tweens.
/// </summary>
public sealed class UIPanelExampleDOTweenSequence : UIPanelExampleBase
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
    /// The local rotation of the context when hidden
    /// </summary>
    [SerializeField]
    private float m_contextRotation = 15.0f;

    /// <summary>
    /// The duration of the tweening effect.
    /// </summary>
    [SerializeField]
    private float m_duration = 0.5f;

    /// <summary>
    /// Instance references of the DOTween Sequence.
    /// </summary>
    private Sequence m_sequence;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public UIPanelExampleDOTweenSequence()
    {
        m_sequence = null;
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
        m_sequence?.Kill(); // Kill the previous sequence if it exists
        m_sequence = DOTween.Sequence(); // Create a new sequence. Cannot new sequence directly because of pooling usage.
        m_sequence.OnStart(() =>
        {
            // Set the canvas group to be interactable and block raycasts.
            m_canvasGroup.interactable = true; // Enable interaction
            m_canvasGroup.blocksRaycasts = true; // Enable raycasting
            m_rectTransformContext.localPosition = m_contextOffsetPosition; // Set the initial position
            m_rectTransformContext.localEulerAngles = new Vector3(0f, 0f, m_contextRotation); // Set the initial rotation
            m_rectTransformContext.localScale = Vector3.one; // Set the initial scale
        });
        m_sequence.Append(m_canvasGroup.DOFade(1f, m_duration).SetEase(Ease.OutExpo)); // Fade in the canvas group
        m_sequence.Join(m_rectTransformContext.DOLocalMove(Vector3.zero, m_duration).SetEase(Ease.OutBounce)); // Move the context to its final position
        m_sequence.Join(m_rectTransformContext.DOLocalRotate(Vector3.zero, m_duration).SetEase(Ease.OutBounce)); // Rotate the context to its final rotation
        m_sequence.OnKill(() =>
        {
            m_sequence = null; // Release the sequence reference
        });
        m_sequence.SetAutoKill(true); // Automatically kill the sequence when it's done
        m_sequence.Play(); // Play the sequence
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
        m_sequence?.Kill(); // Kill the previous sequence if it exists
        m_sequence = DOTween.Sequence(); // Create a new sequence. Cannot new sequence directly because of pooling usage.
        m_sequence.OnStart(() =>
        {
            // Set the canvas group to be interactable and block raycasts.
            m_canvasGroup.interactable = false; // Disable interaction
            m_canvasGroup.blocksRaycasts = false; // Disable raycasting
        });
        m_sequence.Append(m_canvasGroup.DOFade(0f, m_duration).SetEase(Ease.OutExpo)); // Fade out the canvas group
        m_sequence.Join(m_rectTransformContext.DOScale(Vector3.one * 0.8f, m_duration).SetEase(Ease.OutCirc)); // Scale the context to zero
        m_sequence.Join(m_rectTransformContext.DOLocalMove(-m_contextOffsetPosition * 0.5f, m_duration).SetEase(Ease.OutCirc)); // Move the context to its offset position
        m_sequence.Join(m_rectTransformContext.DOLocalRotate(new Vector3(0f, 0f, -m_contextRotation * 0.5f), m_duration).SetEase(Ease.OutCirc)); // Rotate the context to its offset rotation
        m_sequence.OnKill(() =>
        {
            m_sequence = null; // Release the sequence reference
        });
        m_sequence.SetAutoKill(true); // Automatically kill the sequence when it's done
        m_sequence.Play(); // Play the sequence
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
        if (m_sequence != null)
        {
            m_sequence.Kill();
            m_sequence = null; // Release the sequence reference
        }
    }
}
