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
    [SerializeField] private DayNightCycle dayNightCycle;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _linkSpeed = 5f;
    

    [SerializeField] private float _moveTargetTolerance = 0.5f;
    

    public NavMeshAgent _navMeshAgent;
    [SerializeField] GameObject gameOverUI;

    private bool _bShouldGatherResource;
    private bool _bShouldFixFence;
    private bool __bShouldBuldFence;

    private Resource _collidingResource;
    private Fence _collidingFence;

    public bool moving = false;

    private void Awake()
    {
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();

        if (_playerAnimator == null)
        {
            Debug.LogError("Currently missing the animator");
        }
    }

    private void FixedUpdate()
    {
        if (_navMeshAgent.isPathStale)
        {
            Vector3 destination = _navMeshAgent.destination;
            _navMeshAgent.ResetPath();
            _navMeshAgent.destination = destination;
        }

        float distance = Vector3.Distance(this.transform.position, _navMeshAgent.destination);
        //Debug.Log(distance);
        if (distance > _moveTargetTolerance)
        {

            moving = true;

        }
        else
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

    //private void OnTriggerEnter(Collider collider)
    //{
    //    _collidingResource = collider.gameObject.GetComponent<Resource>();
    //    _collidingFence = collider.gameObject.GetComponent<Fence>();
    //    //BuildingBase base = collider.gameObject.GetComponent<BuildingBase>();

    //    if (_collidingResource != null)
    //    {
    //        if (_bShouldGatherResource)
    //        {
    //            StartCoroutine(GatherResources(_collidingResource));
    //        }
    //    }
    //    else if (_collidingFence != null)
    //    {
    //        if (_bShouldFixFence)
    //        {
    //        //StartCoroutine(PopUpButtonPressed(fence));
    //        }
    //    }
    //    //else if (base != null)
    //    //{
    //    //    _bIsColliding = true;

    //    //    if (_bShouldBuldFence)
    //    //    {
    //    //        StartCoroutine(PopUpButtonPressed(base));
    //    //    }
    //    //}
    //}

    //private void OnTriggerExit(Collider collider)
    //{
    //    _collidingResource = null;
    //    _collidingFence = null;
    //}

    //private IEnumerator GatherResources(Resource resource)
    //{
    //    //_playerAnimator.SetBool("shouldGather", true);
    //    yield return new WaitForSeconds(resource.GetHarvestTime());
    //    _resourceHud.AddResources(resource.GetResourceType(), resource.GetResourcePoints());
    //    //_playerAnimator.SetBool("shouldGather", false);
    //}

    

    private void Move()
    {

            if (dayNightCycle.isDay)
            {
            _navMeshAgent.areaMask = NavMesh.AllAreas;
                if (_navMeshAgent.isOnOffMeshLink)
            {
                _navMeshAgent.speed = _linkSpeed;
            } 
            } else
            {
            _navMeshAgent.areaMask = 1 << NavMesh.GetAreaFromName("Walkable");

            }

        if (!_navMeshAgent.isOnOffMeshLink)
        {
            _navMeshAgent.speed = 0f;
        }
            transform.LookAt(_navMeshAgent.steeringTarget);
            Vector3 rot = transform.eulerAngles;
            //rot.x = 0;
            //rot.z = 0;
            transform.eulerAngles = rot;
            transform.position += transform.forward * (_speed * Time.fixedDeltaTime);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            Die();
        }
    }


    public void Die()
    {
        //death animation
        _playerAnimator.SetTrigger("Die");

        gameOverUI.SetActive(true);

        //enable Game Over UI
        Time.timeScale = 0f;

    }


}
