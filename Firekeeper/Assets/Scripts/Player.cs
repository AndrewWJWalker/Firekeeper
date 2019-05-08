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
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _moveTargetTolerance = 0.5f;

    private NavMeshAgent _navMeshAgent;

    private bool _bShouldGatherResource;
    private bool _bShouldFixFence;
    private bool __bShouldBuldFence;

    private Resource _collidingResource;
    private Fence _collidingFence;
    //private Build _collidingResource;

    private bool moving = false;

    

    private void Awake()
    {
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();

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
        _navMeshAgent.destination = targetPosition;


        if (_popUpController == null)
        {
            Debug.LogError("Pop Up controller missing");
            return;
        }
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
            //StartCoroutine(ButtonPressed(fence));
            }
        }
        //else if (base != null)
        //{
        //    _bIsColliding = true;

        //    if (_bShouldBuldFence)
        //    {
        //        StartCoroutine(ButtonPressed(base));
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

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(this.transform.position, _navMeshAgent.destination);
        Debug.Log(distance);
        if (distance > _moveTargetTolerance)
        {
    
                moving = true;
            
        } else
        {
            if (moving)
            {
                Move();
            }
            moving = false;
        }
        if (moving)
        {
            Move();
            
        }

        _playerAnimator.SetBool("isRunning", moving);

    }

    void Move()
    {
        transform.LookAt(_navMeshAgent.steeringTarget);
        Vector3 rot = transform.eulerAngles;
        rot.x = 0;
        rot.z = 0;
        transform.eulerAngles = rot;
        transform.position += transform.forward * (_speed * Time.fixedDeltaTime);
    }
}
