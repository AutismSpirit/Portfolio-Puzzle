using System.Collections.Generic;
using UnityEngine;

public class HeldItemController : MonoBehaviour
{
    public IUsable currentItem;
    public List<IUsable> heldItems = new List<IUsable>();
    int _itemIndex;

    public int ItemIndex 
    { 
        get => _itemIndex; 
        set
        {
            _itemIndex = value;
            if (_itemIndex > heldItems.Count - 1)
            {
                _itemIndex = 0;
            }
            if (_itemIndex < 0)
            {
                _itemIndex = heldItems.Count - 1;
            }
            currentItem = heldItems[_itemIndex];
            currentItem.OnItemChange();
        }
    }

    private void Start()
    {
        currentItem = GameObject.FindGameObjectWithTag("Weapon").GetComponent<IUsable>();

        if (currentItem != null)
        {
            Debug.Log("Gravgun found");
            heldItems.Add(currentItem);
        }
    }

    private void Update()
    {
        //Check for inputs. Will be customizable once the controls are complete.
        if (Input.GetMouseButtonDown(0))
        {
            currentItem.Use();
        }

        if (Input.GetMouseButtonDown(1))
        {
            currentItem.RightClick();
        }

        if (Input.mouseScrollDelta.y > 0)
        {
            ItemIndex++;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            ItemIndex--;
        }
    }



}
