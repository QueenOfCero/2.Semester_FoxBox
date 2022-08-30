using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StartAtTop : MonoBehaviour
{
    void Start()
    {
        GetComponent<Scrollbar>().value = 1;
    }

}
