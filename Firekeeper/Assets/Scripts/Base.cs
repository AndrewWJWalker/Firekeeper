using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Base : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private PopUpController _controller;
    [SerializeField] private GameObject _popUp;
    [SerializeField] private GameObject _fencePrefab;
    [SerializeField] private int _fenceBuildCost;

    private readonly PopUp.PopUpType _popUpType = PopUp.PopUpType.Build;
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

        if (_fencePrefab == null)
        {
            Debug.LogError("Fence to be built is missing");
        }
        _controller.InitiatePopUp(_popUp, _fencePrefab.GetComponent<Fence>(), _popUpType , this);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Player>() != null)
        {
            _playerReady = true;

            if (_buttonPressed)
            {
                BuildFence();
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
            BuildFence();
        }
    }

    private void BuildFence()
    {
        _resource.resourceCost = _fenceBuildCost;

        if (_resource.PayResourcesForBuild(ResourceType.Wood))
        {
            _controller.ClearPopUp();

            // Start Animation Coroutine
            // pass the _fencePrefab and this game object references to the player so you can disable them when we want
            //gameObject.GetComponentInParent<Player>().

            //_fencePrefab.SetActive(true);
            //gameObject.SetActive(false);
        }
    }

}
