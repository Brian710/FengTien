using System.Collections;
using UnityEngine;
public interface IWashable 
{
    GameObject Obj();

    void SetWashed(bool value);

    bool IsWashed();

    int WashTime();
}
