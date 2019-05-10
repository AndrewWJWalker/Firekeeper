using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpController : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Player _player;

    private GameObject _activePopUp;
    private Ray _ray;

    private Fence _fence;
    private Base _base;
    private Tree _tree;

    private void Update()
    {
        //TODO decide if this stays or not
        if (_activePopUp != null)
        {
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(_activePopUp.gameObject.transform.position);
            RectTransform rect = _activePopUp.GetComponent<RectTransform>();
            Vector2 localPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.GetComponent<RectTransform>(), screenPosition, Camera.main, out localPosition);
            _activePopUp.gameObject.transform.localPosition = localPosition;
        }

    }

    public void InitiatePopUp(GameObject hud, Fence fence, PopUp.PopUpType type, Base myBase)
    {
        _fence = fence;
        _base = myBase;

        InitiatePopUp(hud, type);
    }

    public void InitiatePopUp(GameObject hud, Tree tree, PopUp.PopUpType type)
    {
        _tree = tree;

        InitiatePopUp(hud, type);
    }

    public void InitiatePopUp(GameObject hud, Fence fence, PopUp.PopUpType type)
    {
        _fence = fence;
        
        InitiatePopUp(hud, type);
    }


    public void InitiatePopUp(GameObject hud, PopUp.PopUpType type)
    {
        var popUp = hud.GetComponent<PopUp>();

        if (popUp == null)
        {
            Debug.LogError("The gameobject given is missing a pop up component");
            return;
        }

        InitiatePopUpOnMouseClick(hud, type);
    }

    public void ClearPopUp()
    {
        Destroy(_activePopUp);
    }

    private void InitiatePopUpOnMouseClick(GameObject hud, PopUp.PopUpType type)
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(_ray, out hit))
        {
            if (_activePopUp == null)
            {
                if (type == PopUp.PopUpType.Fix && _fence.IsFenceFixable())
                {
                    PositionPopUp(hud, hit);

                    var popUp = _activePopUp.GetComponent<PopUp>();

                    popUp.SetFence(_fence);
                    popUp.SetPopUpAmount(_fence.GetFenceFixCost());

                }
                else if (type == PopUp.PopUpType.Build)
                {
                    PositionPopUp(hud, hit);

                    var popUp = _activePopUp.GetComponent<PopUp>();
                    popUp.SetPopUpAmount(_fence.GetFenceBuildCost());

                    popUp.SetFence(_fence);
                    popUp.SetBase(_base);
                }
                else if (type == PopUp.PopUpType.Harvest)
                {
                    PositionPopUp(hud, hit);

                    var popUp = _activePopUp.GetComponent<PopUp>();

                    popUp.SetTree(_tree);
                }

            }
        }
    }

    private void PositionPopUp(GameObject hud, RaycastHit hit)
    {
        var screenSpaceCord = Camera.main.WorldToScreenPoint(hit.point);
        var canvasRectTransform = _canvas.GetComponent<RectTransform>();
        Vector3 SpawnPosition;

        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRectTransform, screenSpaceCord,
            Camera.main, out SpawnPosition);

        _activePopUp = Instantiate(hud, SpawnPosition,
            Quaternion.identity, _canvas.transform) as GameObject;

        _activePopUp.transform.localRotation = Quaternion.identity;
    }
} 
