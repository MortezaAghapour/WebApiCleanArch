using System;
using System.Collections.Generic;
using System.Text;
using Pluralize.NET.Core;

namespace WebApiCleanArch.Common.Extensions
{
  public static class PluralizeExtension
    {
        public static string Pluralize(this string s)
        {
            var pluralizer = new Pluralizer();
            return pluralizer.Pluralize(s);
        }
        public static string Singularize(this string s)
        {
            var pluralizer = new Pluralizer();
            return pluralizer.Singularize(s);
        }
    }
}
