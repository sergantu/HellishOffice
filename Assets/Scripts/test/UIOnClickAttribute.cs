using System;
using JetBrains.Annotations;

namespace AgkUI.Attributes.Method
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Method)]
    public class UIOnClickAttribute: Attribute
    {
        [CanBeNull]
        public string Name { get; }
        public UIOnClickAttribute([CanBeNull] string name = null)
        {
            Name = name;
        }
    }
}
