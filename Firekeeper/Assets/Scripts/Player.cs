using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private ResourceHud _resourceHud;

    private NavMeshAgent _navMesh;
    private bool _bShouldGatherResource;

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

    public void Move(Vector3 targetPosition)
    {
        _navMesh.destination = targetPosition; 
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (_bShouldGatherResource)
        {
            Resource resource = collider.gameObject.GetComponent<Resource>();
            if (resource == null)
            {
                return;
            }

            Debug.Log("Got em boss");
            StartCoroutine(GatherResources(resource));
        }
    }

    private IEnumerator GatherResources(Resource resource)
    {
        //_playerAnimator.SetBool("shouldGather", true);
        yield return new WaitForSeconds(resource.GetHarvestTime());
        _resourceHud.AddResources(resource.GetResourceType(), resource.GetResourcePoints());
        //_playerAnimator.SetBool("shouldGather", false);

        // points.Gain(resource.resourcePoints, resource.resourceType);
    }
}
