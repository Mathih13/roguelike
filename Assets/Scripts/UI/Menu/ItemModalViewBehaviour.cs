using DG.Tweening;
using mhartveit;
using UnityEngine;

public class ItemModalViewBehaviour : MonoBehaviour, IModalView<ItemModalViewData>
{
    [SerializeField]
    private ItemModalViewUI modalUI;

    private Item currentItem;
    private Tween currentTween;
    private Vector2 ModalOffset = new Vector2(100, 100);

    private bool open;

    public void Hide()
    {
        if (open)
        {
            currentTween.Kill();
            open = false;
            currentTween = UI.Instance.CloseAnimation(modalUI.transform).OnComplete(() => {
                modalUI.gameObject.SetActive(false);
            });
        }
    }

    private void SetCurrentItem(Item item)
    {
        currentItem = item;
    }

    public void Show(ItemModalViewData data)
    {
        SetCurrentItem(data.Item);
        modalUI.Initialize(data);

        if (!open)
        {
            currentTween.Kill();
            modalUI.gameObject.SetActive(true);
            open = true;
            currentTween = UI.Instance.OpenAnimation(modalUI.transform);
        }
    }
}

public interface IModalView
{
    void Show();
    void Hide();
}

public interface IModalView<T>
{
    void Show(T data);
    void Hide();
}

public struct HoverMenuData
{
    public string Title { get; set; }
    public string Description { get; set; }
}