using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Player player;

    public void OnPointerClick(PointerEventData eventData)
    {       

        if (gameObject.GetComponent<Resource>() != null)
        {
            Debug.Log("Lets chop em trees");
            player.SetShouldGatherResource(true);
        }
        else
        {
            player.SetShouldGatherResource(false);
        }

        player.Move(eventData.pointerCurrentRaycast.worldPosition); 
    }

}
