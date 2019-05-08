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
    private Button btn;


    private void Start()
    {
        btn = gameObject.GetComponentInChildren<Button>();

        btn.onClick.AddListener(PopUpButtonClicked);
    }   

    public PopUpType GetPoPopUpType()
    {
        return _type;
    }

    public void SetFence(Fence fence)
    {
        _fence = fence;
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
                break;
            case PopUpType.Collect:
                break;
            default:
                Debug.LogError("Pop Up type not supported");
                break;
        }
    }
}
