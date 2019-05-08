using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpController : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private GameObject _activeGameObject;
    private Ray ray;

    public void InitiatePopUp(GameObject hud, Fence fence)
    {
        var popUp = hud.GetComponent<PopUp>();

        if (popUp == null)
        {
            Debug.LogError("The gameobject given is missing a pop up component");
            return;
        }

        InitiatePopUpOnMouseClick(hud, fence);
    }

    public void ClearPopUp()
    {
        Destroy(_activeGameObject);
    }

    private void InitiatePopUpOnMouseClick(GameObject hud, Fence fence)
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (_activeGameObject == null)
            {
                var screenSpaceCord = Camera.main.WorldToScreenPoint(hit.point);
                var canvasRectTransform = _canvas.GetComponent<RectTransform>();
                Vector3 SpawnPosition;

                RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRectTransform, screenSpaceCord,
                    Camera.main, out SpawnPosition);

                _activeGameObject = Instantiate(hud, SpawnPosition,
                        Quaternion.identity, _canvas.transform) as GameObject;

                _activeGameObject.transform.localRotation = Quaternion.identity;

                var popUp = _activeGameObject.GetComponent<PopUp>();
                popUp.SetFence(fence);
            }
        }
    }
} 
