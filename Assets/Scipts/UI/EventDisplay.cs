using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventDisplay : MonoBehaviour
{
    private TextMeshProUGUI text;
    Vector3 originalPos;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        originalPos = text.transform.localPosition;
    }
    public void DisplayEvent(string message)
    {
        text.text = message;
        var sequence = DOTween.Sequence();
        sequence.Append(text.transform.DOLocalMoveY(originalPos.y + 150f, 0.5f));
        //sequence.Join(text.DOFade(100f,0.5));
        sequence.AppendInterval(2f);
        sequence.Append(text.transform.DOLocalMoveY(originalPos.y,0.5f));


        
    }
}
