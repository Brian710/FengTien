using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinYanGame.Core
{
    public class WashHandCanV : OptionalSystemBase
    {

        public ShowHandWash ShowHandWash;
        private void OnEnable()
        {
            ShowHandWash.ShowAnimation(true);
        }
        private void OnDisable()
        {
            ShowHandWash.ShowAnimation(false);
        }
    }
}
