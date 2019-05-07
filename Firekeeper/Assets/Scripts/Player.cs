using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private ResourceHud _resourceHud;
    [SerializeField] private PopUpController _popUpController;

    private NavMeshAgent _navMesh;

    private bool _bShouldGatherResource;
    private bool _bShouldFixFence;
    private bool __bShouldBuldFence;

    private Resource _collidingResource;
    private Fence _collidingFence;
    //private Build _collidingResource;

    private void Awake()
    {
        _navMesh = gameObject.GetComponent<NavMeshAgent>();

        if (_playerAnimator == null)
        {
            Debug.LogError("Currently missing the animator");
        }
    }

    public void SetShouldGatherResource(bool shouldGather)
    {
        _bShouldGatherResource = shouldGather;
    }

    public void SetShouldFixFence(bool shouldFix)
    {
        _bShouldFixFence = shouldFix;
    }

    public void Move(Vector3 targetPosition)
    {
        _navMesh.destination = targetPosition;

        _popUpController.ClearPopUp();
    }

    public void IsReadyToCollectResource()
    {
        if (_collidingResource != null && _bShouldGatherResource)
        {
            StartCoroutine(GatherResources(_collidingResource));
        }
    }   

    private void OnTriggerEnter(Collider collider)
    {
        _collidingResource = collider.gameObject.GetComponent<Resource>();
        _collidingFence = collider.gameObject.GetComponent<Fence>();
        //BuildingBase base = collider.gameObject.GetComponent<BuildingBase>();

        if (_collidingResource != null)
        {
            if (_bShouldGatherResource)
            {
                StartCoroutine(GatherResources(_collidingResource));
            }
        }
        else if (_collidingFence != null)
        {
            if (_bShouldFixFence)
            {
            //StartCoroutine(FixFence(fence));
            }
        }
        //else if (base != null)
        //{
        //    _bIsColliding = true;

        //    if (_bShouldBuldFence)
        //    {
        //        StartCoroutine(FixFence(base));
        //    }
        //}
    }

    private void OnTriggerExit(Collider collider)
    {
        _collidingResource = null;
        _collidingFence = null;
    }

    private IEnumerator GatherResources(Resource resource)
    {
        //_playerAnimator.SetBool("shouldGather", true);
        yield return new WaitForSeconds(resource.GetHarvestTime());
        _resourceHud.AddResources(resource.GetResourceType(), resource.GetResourcePoints());
        //_playerAnimator.SetBool("shouldGather", false);
    }
}
