using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UserInterfaceBehaviour : UIBehaviour
{
    /// <summary>
    /// Get the RectTransform component of this GameObject.
    /// May be null if this GameObject does not have a RectTransform component.
    /// </summary>
    public RectTransform rectTransform => transform is RectTransform rect ? rect : null;
}
