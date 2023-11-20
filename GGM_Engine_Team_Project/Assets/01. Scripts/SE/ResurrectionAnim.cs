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
        Debug.Log("��Ȱ����");
    }

    IEnumerator ResurrectionRoutine()
    {
        resurrectionTrm.DOLocalMove(new Vector2(0, 0), 1f).SetEase(Ease.OutCubic);
        resurrectionTrm.DOScale(new Vector2(2, 2), 1f).SetEase(Ease.InOutCubic);
        yield return new WaitForSeconds(0.7f);
        resurrectionRenderer.color = new Color(1, 1, 1, 0);

    particle.Play();

        float duration = 0.25f; // Ÿ���� ���� �� ������ ���¸� ������ �ð�
        float alphaValue = 0.25f; // ������ ���İ�
        for (int i = 0; i < 3; i++)
        {
            Debug.Log(i);

            Color originalColor = player.color;
            Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, alphaValue);

            // ������ ���İ��� ���߱�
            float elapsed = 0f;
            while (elapsed < duration)
            {
                player.color = Color.Lerp(originalColor, targetColor, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            // ������ ���İ��� ȸ����Ű��
            elapsed = 0f;
            while (elapsed < duration)
            {
                player.color = Color.Lerp(targetColor, originalColor, elapsed / duration);
                elapsed += Time.deltaTime;
               yield return null;
            }

            alphaValue += 0.25f;
        }

        Debug.Log("��Ȱ");
        ObjReset(false);
    }
}
