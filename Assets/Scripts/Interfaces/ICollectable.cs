using System;
using UnityEngine;

namespace Interfaces
{
    public interface ICollectable
    {
        void Collect();
        void Uncollect();
        
        Collider2D GetCollider();
    }
}
