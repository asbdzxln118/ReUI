﻿using System.Collections.Generic;
using System.Linq;
using Rentitas;
using ReUI.Api;
using UnityEngine;

namespace ReUI.Implementation.Systems
{
    public class ExecuteLuaOnStateSystem : IReactiveSystem<IUIPool>, ISetPool<IUIPool>
    {
        private Pool<IUIPool> _pool;

        public void Execute(List<Entity<IUIPool>> entities)
        {
//            Debug.Log("Exetue elements on state");
            foreach (var entity in entities)
            {
                entity.Get<LuaLifeCycle>().OnState();
                entity.Toggle<LuaScopeStateUpdate>(false);
            }
        }

        public TriggerOnEvent Trigger => Matcher
            .AllOf(
                typeof (LuaCodeOnState), 
                typeof (LuaScopeStateUpdate), 
                typeof (LuaLifeCycle))
            .NoneOf(
                typeof (LuaRequireCompile),
                typeof (ScopeType)
            ).OnEntityAdded();

        public void SetPool(Pool<IUIPool> typedPool)
        {
            _pool = typedPool;
        }
    }
}