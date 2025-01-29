using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] protected CanvasGroup canvasGroup;
    public virtual void OnEnter()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    public virtual void OnExit()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}