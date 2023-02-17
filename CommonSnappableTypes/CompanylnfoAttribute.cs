using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSnappableTypes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class CompanylnfoAttribute : Attribute
    {
        public string CompanyName { get; set; }
        public string CompanyUrl { get; set; }
    }
}
