using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveFalse : MonoBehaviour
{
    [SerializeField] private float activeFalseTime = 0.5f;

    private void OnEnable()
    {
        Debug.Log("ÄÑÁ³¾î!");
        StopCoroutine(ActiveMe());
        StartCoroutine(ActiveMe());
    }

    IEnumerator ActiveMe()
    {
        yield return new WaitForSeconds(activeFalseTime);
        gameObject.SetActive(false);
    }
}
