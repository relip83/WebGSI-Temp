using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galcon.DAL.BaseDAL.Reflection
{
    public interface IPropertyAssigner { }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PropertyAssignerIgnoreAttribute : Attribute, IPropertyAssigner
    {
        public bool Ignore { get; set; }

        public PropertyAssignerIgnoreAttribute()
            : this(true)
        {
        }

        public PropertyAssignerIgnoreAttribute(bool IgnoreThisProperty)
            : base()
        {
            Ignore = IgnoreThisProperty;
        }
    }
}
