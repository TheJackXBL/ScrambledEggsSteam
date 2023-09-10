using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSprite : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        if (gameObject.transform.position.y > 0)
        {
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(0.2f);

        gameObject.transform.parent.gameObject.GetComponent<Car>()?.CarGoesOffscreen();
    }
}
