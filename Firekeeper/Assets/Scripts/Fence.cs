using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fence : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private PopUpController _controller;
    [SerializeField] private GameObject _popUp;

    private Health _health;
    private Resource _resource;

    private bool _playerReady;
    private bool _buttonPressed;

    private void Start()
    {
        _health = gameObject.GetComponent<Health>();

        if (_health == null)
        {
            Debug.LogError("Attach Health script");
        }

        _resource = gameObject.GetComponent<Resource>();

        if (_health == null)
        {
            Debug.LogError("Attach Resource script");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
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
                FixFence();
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
            FixFence();
        }
    }

    public bool IsFenceFixable()
    {
        return (_health.maxHealth > _health.GetCurrentHealthPoints());
    }

    private void FixFence()
    {

        var resourceCost = GetFenceFixCost();

        _resource.resourceCost = resourceCost;

        _resource.PayResources(ResourceType.Wood, this);
    }

    public int GetFenceFixCost()
    {
        var resourceCost = _health.maxHealth - _health.GetCurrentHealthPoints();
        return resourceCost *= 5;
    }

    public void RestoreFenceHealth()
    {
        _health.Heal(100);
        _controller.ClearPopUp();
    }
}
