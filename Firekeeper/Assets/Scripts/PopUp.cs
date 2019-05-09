using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    public enum PopUpType
    {
        Fix = 1 << 0,
        Build = 1 << 1,
        Collect = 1 << 2
    }

    [SerializeField] private PopUpType _type;

    private Fence _fence;
    private Base _base;
    private Button _button;
    private Tree _tree;
    private Text _text;


    private void Start()
    {
        _button = gameObject.GetComponentInChildren<Button>();

        _button.onClick.AddListener(PopUpButtonClicked);

        _text = gameObject.GetComponentInChildren<Text>();
    }

    public PopUpType GetPoPopUpType()
    {
        return _type;
    }

    public void SetFence(Fence fence)
    {
        _fence = fence;
    }

    public void SetBase(Base myBase)
    {
        _base = myBase;
    }

    public void SetTree(Tree tree)
    {
        _tree = tree;
    }

    public void SetPopUpAmount(int amount)
    {
        gameObject.GetComponentInChildren<Text>().text = amount.ToString();
    }

    private void PopUpButtonClicked()
    {
        switch (_type)
        {
            case PopUpType.Fix:
                if (_fence == null)
                {
                    Debug.LogError("Fence reference is missing");
                    return;
                }
                _fence.ButtonPressed();
                break;
            case PopUpType.Build:
                 _base.ButtonPressed();
                break;
            case PopUpType.Collect:
                _tree.ButtonPressed();
                break;
            default:
                Debug.LogError("Pop Up type not supported");
                break;
        }
    }
}
