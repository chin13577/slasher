using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DamageText : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private Text text;
    [SerializeField]
    private AnimationCurve curve;

    private Vector2 targetPos;
    private Sequence sequence;

    public void SetDamageText(int damage)
    {
        text.text = damage.ToString();
    }

    private void OnEnable()
    {
        Vector2 randomCircle = Random.insideUnitCircle * 0.5f;
        targetPos = transform.position.ToVector2() + new Vector2(Random.Range(-1f, 1f), 1) + randomCircle;
        canvasGroup.alpha = 1;
        sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveX(targetPos.x, .75f));
        sequence.Join(transform.DOMoveY(targetPos.y, .75f).SetEase(curve));
        sequence.Join(canvasGroup.DOFade(0, 1f));
        sequence.OnComplete(() => { gameObject.SetActive(false); });
        sequence.Play();
    }

    private void OnDisable()
    {
        canvasGroup.alpha = 1;
    }

}
