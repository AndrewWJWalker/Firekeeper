using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fence : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private PopUpController _controller;
    [SerializeField] private GameObject _popUp;
    [SerializeField] private GameObject _base;
    [SerializeField] private int _fenceBuildCost;

    private readonly PopUp.PopUpType _popUpType = PopUp.PopUpType.Fix;

    private Health _health;
    private Resource _resource;

    private bool _playerReady;
    private bool _bPopUpButtonPressed;

    private void Start()
    {
        _health = gameObject.GetComponent<Health>();

        if (_health == null)
        {
            Debug.LogError("Attach Health script");
        }

        _resource = gameObject.GetComponent<Resource>();

        if (_resource == null)
        {
            Debug.LogError("Attach Resource script");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _bPopUpButtonPressed = false;
       
        _controller.InitiatePopUp(_popUp, this, _popUpType);  
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Player>() != null)
        {
            _playerReady = true;

            if (_bPopUpButtonPressed)
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

    public void PopUpButtonPressed()
    {
        _bPopUpButtonPressed = true;

        if (_playerReady)
        {
            FixFence();
        }
    }

    public bool IsFenceFixable()
    {
        return _health.IsDamaged();
    }

    public int GetFenceFixCost()
    {
        var resourceCost = _health.maxHealth - _health.GetCurrentHealthPoints();
        return resourceCost *= 5;
    }

    public int GetFenceBuildCost()
    {
        return _fenceBuildCost;
    }

    public void OpenBase()
    {
        _base.SetActive(true);
    }

    private void FixFence()
    {
        var resourceCost = GetFenceFixCost();

        _resource.resourceCost = resourceCost;

        if (_resource.PayResourcesForFix(ResourceType.Wood))
        {
            _controller.ClearPopUp();
            // Start Animation Coroutine
            //pass the health parameter so you can do it on the player _health.RestoreHealth();
            //gameObject.GetComponentInParent<Player>().
            //RestoreFenceHealth();
        }
    }
}
