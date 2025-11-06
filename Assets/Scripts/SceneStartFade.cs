using System.Collections;
using UnityEngine;

public class SceneStartFade : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return StartCoroutine(FadeManager.instance.FadeIn(1f));
    }
}
