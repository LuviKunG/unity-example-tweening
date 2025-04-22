using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class for the UI panel example using coroutine tweening.
/// </summary>
public sealed class UIPanelExampleCoroutineTween : UIPanelExampleBase
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
    /// Collection of tweening coroutines.
    /// </summary>
    private readonly List<Coroutine> m_tweeningCoroutines;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public UIPanelExampleCoroutineTween()
    {
        m_tweeningCoroutines = new List<Coroutine>();
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
        StopAllTweeningCoroutine();
        Coroutine coroutine = StartCoroutine(CoroutineShow(m_duration, () => { m_tweeningCoroutines.Clear(); }));
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
        StopAllTweeningCoroutine();
        Coroutine coroutine = StartCoroutine(CoroutineHide(m_duration, () => { m_tweeningCoroutines.Clear(); }));
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
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        StopAllTweeningCoroutine();
    }

    /// <summary>
    /// Stop all coroutines.
    /// </summary>
    private void StopAllTweeningCoroutine()
    {
        foreach (var coroutine in m_tweeningCoroutines)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }
        m_tweeningCoroutines.Clear();
    }

    /// <summary>
    /// Coroutine to show the panel with a fade and move effect.
    /// </summary>
    /// <param name="duration">The duration of the tween.</param>
    /// <param name="onDone">The action to call when the tween is done.</param>
    /// <returns>IEnumerator of Coroutine.</returns>
    private IEnumerator CoroutineShow(float duration, Action onDone = null)
    {
        m_canvasGroup.alpha = 0f;
        m_canvasGroup.interactable = true;
        m_canvasGroup.blocksRaycasts = true;
        m_rectTransformContext.localPosition = m_contextOffsetPosition;
        Coroutine coroutineFade = StartCoroutine(CoroutineFadeTween(duration, 1f));
        m_tweeningCoroutines.Add(coroutineFade);
        Coroutine coroutineMove = StartCoroutine(CoroutineMoveTween(Vector3.zero, duration));
        m_tweeningCoroutines.Add(coroutineMove);
        yield return coroutineFade;
        yield return coroutineMove;
        onDone?.Invoke();
    }

    /// <summary>
    /// Coroutine to hide the panel with a fade and move effect.
    /// </summary>
    /// <param name="duration">The duration of the tween.</param>
    /// <param name="onDone">The action to call when the tween is done.</param>
    /// <returns>IEnumerator of Coroutine.</returns>
    private IEnumerator CoroutineHide(float duration, Action onDone = null)
    {
        m_canvasGroup.interactable = false;
        m_canvasGroup.blocksRaycasts = false;
        m_rectTransformContext.localPosition = Vector3.zero;
        Coroutine coroutineFade = StartCoroutine(CoroutineFadeTween(duration, 0f));
        m_tweeningCoroutines.Add(coroutineFade);
        Coroutine coroutineMove = StartCoroutine(CoroutineMoveTween(m_contextOffsetPosition, duration));
        m_tweeningCoroutines.Add(coroutineMove);
        yield return coroutineFade;
        yield return coroutineMove;
        onDone?.Invoke();
    }

    /// <summary>
    /// Coroutine to tween the alpha of the canvas group.
    /// </summary>
    /// <param name="duration">The duration of the tween.</param>
    /// <param name="targetAlpha">The target alpha value.</param>
    /// <param name="onDone">The action to call when the tween is done.</param>
    /// <returns>IEnumerator of Coroutine.</returns>
    private IEnumerator CoroutineFadeTween(float duration, float targetAlpha, Action onDone = null)
    {
        float startAlpha = m_canvasGroup.alpha;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            m_canvasGroup.alpha = alpha;
            yield return null;
        }
        m_canvasGroup.alpha = targetAlpha;
        onDone?.Invoke();
    }

    /// <summary>
    /// Coroutine to tween the local position of the rect transform.
    /// </summary>
    /// <param name="targetLocalPosition">The target local position.</param>
    /// <param name="duration">The duration of the tween.</param>
    /// <param name="onDone">The action to call when the tween is done.</param>
    /// <returns>IEnumerator of Coroutine.</returns>
    private IEnumerator CoroutineMoveTween(Vector3 targetLocalPosition, float duration, Action onDone = null)
    {
        Vector3 startPosition = m_rectTransformContext.localPosition;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            Vector3 position = Vector3.Lerp(startPosition, targetLocalPosition, elapsedTime / duration);
            m_rectTransformContext.localPosition = position;
            yield return null;
        }
        m_rectTransformContext.localPosition = targetLocalPosition;
        onDone?.Invoke();
    }
}
