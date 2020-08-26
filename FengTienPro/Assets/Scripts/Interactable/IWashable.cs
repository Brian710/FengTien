using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWashable 
{
    GameObject Obj();

    bool IsWashed(bool value);

    int WashTime();
}
