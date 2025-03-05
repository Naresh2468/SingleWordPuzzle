using UnityEngine.Rendering.PostProcessing;
using System.Collections;
using UnityEngine;

public class Effects : MonoBehaviour
{
    public PostProcessVolume Post;
    [SerializeField]private float lerpTime = 2f; //smooth transition
    private Coroutine currentCoroutine;
    public void EffectsStart()
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(SmoothChangeWeight());
    }
    IEnumerator SmoothChangeWeight()
    {
        yield return StartCoroutine(DoSmoothTransition(0f, 1f));

        yield return StartCoroutine(DoSmoothTransition(1f, 0f));
    }
    IEnumerator DoSmoothTransition(float StartWeight, float endWeight)
    {
        float elapsedTime = 0;
        float initalWeight = StartWeight;

        while (elapsedTime < lerpTime)
        {
            Post.weight = Mathf.Lerp(initalWeight, endWeight, elapsedTime / lerpTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Post.weight = endWeight;
    }
}
