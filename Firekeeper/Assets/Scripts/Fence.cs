using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fence : MonoBehaviour, IPointerClickHandler
{
    //[SerializeField] private Player _player;
    [SerializeField] private PopUpController _controller;
    [SerializeField] private GameObject _popUp;

    private bool _playerReady;
    private bool _buttonPressed;

    public void OnPointerClick(PointerEventData eventData)
    {
        //_player.SetShouldFixFence(true);
        _buttonPressed = false;

        _controller.InitiatePopUp(_popUp, this);  
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Player>() != null)
        {
            _playerReady = true;

            if (_buttonPressed)
            {
                Debug.Log("FIX TRIGGER LAST");
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<Player>() != null)
        {
            _playerReady = false;
        }
    }

    public void ButtonPressed()
    {
        _buttonPressed = true;

        if (_playerReady)
        {
            Debug.Log("FIX BUTTON LAST");
        }
    }
}
