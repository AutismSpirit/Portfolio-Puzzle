using System.Collections.Generic;
using UnityEngine;

public class HeldItemController : MonoBehaviour
{
    IUsable currentItem;
    List<IUsable> heldItems = new List<IUsable>();
    int itemIndex;

    private void Update()
    {
        //Check for inputs. Customized from the options menu.

    }



}
