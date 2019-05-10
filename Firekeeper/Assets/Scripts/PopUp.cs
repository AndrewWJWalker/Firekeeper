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
        Harvest = 1 << 2
    }

    [SerializeField] private PopUpType _type;

    private Fence _fence;
    private Base _base;
    private Tree _tree;

    private Button _button;
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

    public void SetPopUpAmount(int amount, bool hasEnoughCurrency)
    {
        var text = gameObject.GetComponentInChildren<Text>();
       text.text = amount.ToString();
        if (hasEnoughCurrency)
        {
            text.color = Color.black;
        }
        else
        {
            text.color = Color.red;
        }
    }

    private void PopUpButtonClicked()
    {
        switch (_type)
        {
            case PopUpType.Fix:
                if (_fence == null)
                {
                    Debug.LogError("Fence reference is missing from pop up");
                    return;
                }
                _fence.PopUpButtonPressed();
                break;
            case PopUpType.Build:
                if (_base == null)
                {
                    Debug.LogError("Base reference is missing from pop up");
                    return;
                }
                _base.ButtonPressed();
                break;
            case PopUpType.Harvest:
                if (_tree == null)
                {
                    Debug.LogError("Tree reference is missing from pop up");
                    return;
                }
                _tree.ButtonPressed();
                break;
            default:
                Debug.LogError("Pop Up type not supported");
                break;
        }
    }
}
