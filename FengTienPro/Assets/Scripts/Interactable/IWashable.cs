using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWashable 
{
    bool IsWashed(bool value);

    int WashTime();
}
