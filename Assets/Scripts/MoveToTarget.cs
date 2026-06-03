using UnityEngine;
using System.Collections;
public class MoveToTarget : MonoBehaviour
{
    public void MoveTo(Vector2 target)
    {
        StartCoroutine(MoveRoutine(target, 0.25f));
    }

    private IEnumerator MoveRoutine(Vector2 target, float duration)
    {
        
        Vector2 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            transform.position = Vector2.Lerp(startPos, target, t);

            yield return null;
        }

        transform.position = target;

        transform.position = startPos;
    }
}
