using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Level
{
    public interface IPausable 
    {
        void Pause();

        void Unpause();
    }
}