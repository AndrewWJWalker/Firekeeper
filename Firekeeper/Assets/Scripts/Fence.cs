using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fence : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Player _player;
    [SerializeField] private PopUpController _controller;
    [SerializeField] private GameObject _popUp;

    public void OnPointerClick(PointerEventData eventData)
    {
        _player.SetShouldFixFence(true);
        _controller.InitiatePopUp(_popUp);
    }
}
