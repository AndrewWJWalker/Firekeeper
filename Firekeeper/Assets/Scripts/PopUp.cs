using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public enum PopUpType
    {
        Fix = 1 << 0,
        Build = 1 << 1,
        Collect = 1 << 2
    }

    [SerializeField] private PopUpType _type;

    public PopUpType GetPoPopUpType()
    {
        return _type;
    }

}
