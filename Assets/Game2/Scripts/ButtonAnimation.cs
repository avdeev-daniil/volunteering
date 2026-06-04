using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scaleMultiplier = 1.1f;
    public float speed = 8f;

    private Vector3 originalScale;
    private Vector3 targetScale;

    public PlaySound4 sounds;

    private void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            Time.deltaTime * speed
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * scaleMultiplier;
        sounds.PlaySFX(4);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
    }
    
}