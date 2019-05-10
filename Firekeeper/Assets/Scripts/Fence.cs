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
    [SerializeField] private GameObject _fixingAndBuildingPFX;
    [SerializeField] private float _particleDuration = 1f;

    private readonly PopUp.PopUpType _popUpType = PopUp.PopUpType.Fix;
    private GameObject _currentlyPlayingPFX;

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
            _bPopUpButtonPressed = false;
            FixFence();
        }
    }

    public bool IsFenceFixable()
    {
        return _health.IsDamaged();
    }

    public int GetFixBuildCost()
    {
        return _fenceBuildCost;
    }

    public void OpenBase()
    {
        _base.SetActive(true);
    }

    private void FixFence()
    {
        _resource.resourceCost = _fenceBuildCost;

        if (_resource.PayResourcesForFix(ResourceType.Wood))
        {
            _controller.ClearPopUp();
            _currentlyPlayingPFX = Instantiate(_fixingAndBuildingPFX, transform.position, Quaternion.identity);

            StartCoroutine(PlayFixAndBuildAnimation());
        }
    }

    private IEnumerator PlayFixAndBuildAnimation()
    {
        yield return new WaitForSeconds(_particleDuration);
        _health.RestoreHealth();
        Destroy(_currentlyPlayingPFX);
    }
}
