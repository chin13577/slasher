using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinnii.Controller
{
    public interface IReceiveAttackEvent : IReceiveAttackEnter, IReceiveAttackDrag, IReceiveAttackUp
    {
    }
}
