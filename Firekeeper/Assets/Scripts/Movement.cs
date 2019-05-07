using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Player _player;

    public void OnPointerClick(PointerEventData eventData)
    {       
        _player.Move(eventData.pointerCurrentRaycast.worldPosition); 
    }

}
