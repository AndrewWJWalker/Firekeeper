using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tree : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private PopUpController _controller;
    [SerializeField] private GameObject _popUp;
    [SerializeField] private int _resourceAmountHarvest;

    private readonly PopUp.PopUpType _popUpType = PopUp.PopUpType.Harvest;
    private Resource _resource;

    private bool _playerReady;
    private bool _buttonPressed;

    private void Start()
    {
        _resource = gameObject.GetComponent<Resource>();

        if (_resource == null)
        {
            Debug.LogError("Attach Resource script");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _buttonPressed = false;

        _controller.InitiatePopUp(_popUp, this, _popUpType);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Player>() != null)
        {
            _playerReady = true;

            if (_buttonPressed)
            {
                HarvestTree();
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
            HarvestTree();
        }
    }

    private void HarvestTree()
    {
        // var resourceCost = _resourceAmountHarvest;

        //_resource.resourceCost = resourceCost;
        _controller.ClearPopUp();
        // Start Animation Coroutine
        // pass the resource, resource type, the amount and this game object references to the player so you can disable them when we want
        //gameObject.GetComponentInParent<Player>().

        //_resource.GainResourcesFromHarvest(ResourceType.Wood, _resourceAmountHarvest, this.gameObject);
    }
}
