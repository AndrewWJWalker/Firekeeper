using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Player _player;

    public void OnPointerClick(PointerEventData eventData)
    {       

        if (gameObject.GetComponent<Resource>() != null)
        {
            Debug.Log("Lets chop em trees");
            _player.SetShouldGatherResource(true);
        }
        else
        {
            _player.SetShouldGatherResource(false);
        }

        _player.Move(eventData.pointerCurrentRaycast.worldPosition); 
    }

}
