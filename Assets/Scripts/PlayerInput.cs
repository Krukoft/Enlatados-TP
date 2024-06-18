using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Camera Camera;
    [SerializeField] private RectTransform SelectionBox;
    [SerializeField] private LayerMask UnitLayers;
    [SerializeField] private LayerMask FloorLayers; 
    [SerializeField] private float DragDelay = 0.1f; 

    private float MouseDownTime;

    private Vector2 StartMousePosition;

    private void Update()
    {
        HandleSelectionInputs();
        HandleMoveInputs();
    }

    private void HandleMoveInputs()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1) && SelectionManager.Instance.SelectedUnits.Count > 0)
        {
            if (Physics.Raycast(Camera.ScreenPointToRay(Input.mousePosition), out RaycastHit Hit, FloorLayers))
            {
                foreach (SelectableUnit unit in SelectionManager.Instance.SelectedUnits)
                {
                    unit.MoveTo(Hit.point);
                }
            }
        }
    }

    private void HandleSelectionInputs()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            SelectionBox.sizeDelta = Vector2.zero;
            SelectionBox.gameObject.SetActive(true);
            StartMousePosition = Input.mousePosition;
            MouseDownTime = Time.time;
        }
        else if (Input.GetKey(KeyCode.Mouse0) && MouseDownTime + DragDelay < Time.time)
        {
            ResizeSelectionBox();
        }
        else if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            SelectionBox.sizeDelta = Vector2.zero;
            SelectionBox.gameObject.SetActive(false);
            if (Physics.Raycast(Camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, UnitLayers)
                && hit.collider.TryGetComponent<SelectableUnit>(out SelectableUnit unit))
                {
                    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    {
                        if (SelectionManager.Instance.IsSelected(unit))
                        {
                            SelectionManager.Instance.Deselect(unit);
                        }
                        else
                        {
                            SelectionManager.Instance.Select(unit);
                        }
                    }
                    else
                    {
                        SelectionManager.Instance.DeselectAll();
                        SelectionManager.Instance.Select(unit);
                    }
                }
                else if (MouseDownTime + DragDelay > Time.time)
                {
                    SelectionManager.Instance.DeselectAll();
                }
            MouseDownTime = 0;
        }
    }

    private void ResizeSelectionBox()
    {
        float width = Input.mousePosition.x - StartMousePosition.x;
        float heigth = Input.mousePosition.y - StartMousePosition.y;

        SelectionBox.anchoredPosition = StartMousePosition + new Vector2(width / 2, heigth / 2);
        SelectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(heigth));

        Bounds bounds = new Bounds(SelectionBox.anchoredPosition, SelectionBox.sizeDelta);

        for (int i = 0; i < SelectionManager.Instance.AvaliableUnits.Count; i++)
        {
            if (SelectionManager.Instance.AvaliableUnits[i] != null && UnitIsSelectionBox(Camera.WorldToScreenPoint(SelectionManager.Instance.AvaliableUnits[i].transform.position), bounds))
            {
                SelectionManager.Instance.Select(SelectionManager.Instance.AvaliableUnits[i]);
            }
            else
            {
                SelectionManager.Instance.Deselect(SelectionManager.Instance.AvaliableUnits[i]);
            }
        }
        
    }
    private bool UnitIsSelectionBox(Vector2 Position, Bounds Bounds)
    {
        return Position.x > Bounds.min.x && Position.x <Bounds.max.x && Position.y > Bounds.min.y && Position.y < Bounds.max.y;
    }
}
