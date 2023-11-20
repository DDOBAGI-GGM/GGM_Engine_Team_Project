using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurrectionAnim : MonoBehaviour
{
    private Transform resurrectionTrm;
    private SpriteRenderer resurrectionRenderer;
    [SerializeField] private SpriteRenderer player;
    [SerializeField] private ParticleSystem particle = default;

    private void Start()
    {
        resurrectionTrm = GetComponent<Transform>();
        resurrectionRenderer = GetComponent<SpriteRenderer>();
        //Debug.Log(resurrectionTrm);
        ObjReset(false);
        //ResurrectionAnimStart();
    }

    public void ObjReset(bool value)
    {
        resurrectionTrm.gameObject.SetActive(value);
        resurrectionTrm.localPosition = new Vector2(-8.18f, 4.2f);
        resurrectionRenderer.color = new Color(1, 1, 1, 1);
        player.color = new Color(1, 1, 1, 1);
    }

    public void ResurrectionAnimStart()
    {
        //StopCoroutine(ResurrectionRoutine());
        ObjReset(true);

        StartCoroutine(ResurrectionRoutine());
        Debug.Log("부활다함");
    }

    IEnumerator ResurrectionRoutine()
    {
        resurrectionTrm.DOLocalMove(new Vector2(0, 0), 1f).SetEase(Ease.OutCubic);
        resurrectionTrm.DOScale(new Vector2(2, 2), 1f).SetEase(Ease.InOutCubic);
        yield return new WaitForSeconds(0.7f);
        resurrectionRenderer.color = new Color(1, 1, 1, 0);

    particle.Play();

        float duration = 0.25f; // 타격을 받은 후 투명한 상태를 유지할 시간
        float alphaValue = 0.25f; // 투명한 알파값
        for (int i = 0; i < 3; i++)
        {
            Debug.Log(i);

            Color originalColor = player.color;
            Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, alphaValue);

            // 서서히 알파값을 낮추기
            float elapsed = 0f;
            while (elapsed < duration)
            {
                player.color = Color.Lerp(originalColor, targetColor, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            // 서서히 알파값을 회복시키기
            elapsed = 0f;
            while (elapsed < duration)
            {
                player.color = Color.Lerp(targetColor, originalColor, elapsed / duration);
                elapsed += Time.deltaTime;
               yield return null;
            }

            alphaValue += 0.25f;
        }

        Debug.Log("부활");
        ObjReset(false);
    }
}
