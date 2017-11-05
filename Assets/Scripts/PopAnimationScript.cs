using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopAnimationScript : MonoBehaviour {
    public bool Popped = false;

    public IEnumerator PopInAnimationIEnumerator(float fromScale, float toScale, Vector3 popVelocity, float popTime)
    {
        float t = 0;
        while (t < popTime)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(new Vector3(fromScale, fromScale, fromScale), new Vector3(toScale, toScale, toScale), t/popTime);
            transform.Translate(popVelocity * Time.deltaTime);
            yield return null;
        }
    }

    public void PopIn(float fromScale, float toScale, float popDistance, Vector3 popDirection, float popTime)
    {
        if (!Popped)
        {
            Vector3 velocity = popDirection.normalized * popDistance/popTime;
            StartCoroutine(PopInAnimationIEnumerator(fromScale, toScale, velocity, popTime));
            Popped = true;
        }
    }

    public void PopOut(float fromScale, float toScale, float popDistance, Vector3 popDirection, float popTime)
    {
        if (Popped)
        {
            Vector3 velocity = popDirection.normalized * popDistance/popTime;
            StartCoroutine(PopInAnimationIEnumerator(fromScale, toScale, velocity, popTime));
            Popped = false;
        }
    }
}
