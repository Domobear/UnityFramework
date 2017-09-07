using System;

namespace Domo
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CoreStateAttribute : Attribute
    {
        public readonly int id;

        public CoreStateAttribute(int iID)
        {
            id = iID;
        }
    }
}