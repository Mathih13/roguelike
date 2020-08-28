using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingInfoText : MonoBehaviour, UIElement<FloatingTextData>
{
    private TextMeshProUGUI textMesh;

    public void Initialize(FloatingTextData data)
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        transform.position = data.StartPosition;
        textMesh.text = data.Text;
        gameObject.SetActive(true);
        transform.DOMove(data.StartPosition + new Vector3(0, 1f, 0), 2f)
            .SetEase(Ease.OutCubic)
            .OnComplete(() => gameObject.SetActive(false));

        
    }
}

public struct FloatingTextData
{
    public string Text;
    public Vector3 StartPosition;
}