using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarleyBreakRestartButton : MonoBehaviour
{
    private void OnMouseUp()
    {
        BarleyBreakController.instance.RestartRiddle();
    }
}
