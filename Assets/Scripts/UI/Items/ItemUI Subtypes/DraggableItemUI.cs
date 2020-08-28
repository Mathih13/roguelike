using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DraggableItemUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public bool IsDragging { get; private set; }
    public Transform OriginalParent;

    Vector2 startPos;
    Transform menuTransform;
    CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        OriginalParent = transform.parent;
        menuTransform = GameObject.FindGameObjectWithTag("Menu_Overlay").transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = eventData.position;
        transform.SetParent(menuTransform);
        IsDragging = true;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(OriginalParent);
        transform.position = startPos;
        IsDragging = false;
        canvasGroup.blocksRaycasts = true;
    }
}
