using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace KendoEnumEditor.Infrastructure
{
    public static class EnumExtensions
    {
        public static string ToDescription(this Enum value)
        {
            var attribute = value.GetAttribute<DescriptionAttribute>();

            var displayAttribute = value.GetAttribute<DisplayAttribute>();

            if (attribute != null)
            {
                return attribute.Description;
            }

            return displayAttribute != null ? displayAttribute.GetName() : value.ToString();
        }

        public static TAttribute GetAttribute<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            var attributeValue = value.GetType().GetField(value.ToString());
            return attributeValue.GetAttribute<TAttribute>();
        }

        /// <summary>
        /// Fetches the given <typeparamref name="TAttribute"/> from the given <paramref name="subject"/>
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="subject"></param>
        /// <returns>The attribute, or null if one is not defined</returns>
        public static TAttribute GetAttribute<TAttribute>(this MemberInfo subject)
            where TAttribute : Attribute
        {
            if (subject == null) throw new ArgumentNullException(nameof(subject));

            return Attribute.GetCustomAttribute(subject, typeof(TAttribute)) as TAttribute;
        }

        public static IEnumerable<SelectListItem> ToSelectList(this Enum enumValue)
        {
            return from Enum e in Enum.GetValues(enumValue.GetType())
                   select new SelectListItem
                   {
                       Selected = e.Equals(enumValue),
                       Text = e.ToDescription(),
                       Value = e.ToString()
                   };
        }

        /// <summary>
        /// Creates a select item list
        /// </summary>
        /// <param name="enumValue">Selected Enum - I.E Model.EnumProperty</param>
        /// <param name="unselectedText">Text for the unselected options, I.E Please Select...</param>
        /// <returns>Select Item List for a Dropdown</returns>
        public static IEnumerable<SelectListItem> ToSelectList(this Enum enumValue, string unselectedText)
        {
            var enums = Enum.GetValues(enumValue.GetType()).Cast<Enum>().ToArray();
            var selectList = new List<SelectListItem>();

            for (var i = 0; i < enums.Length; i++)
            {
                if (i == 0)
                {
                    selectList.Insert(0, new SelectListItem { Selected = enums[i].Equals(enumValue), Text = unselectedText, Value = enums[i].ToString() });
                }
                else
                {
                    selectList.Add(new SelectListItem { Selected = enums[i].Equals(enumValue), Text = enums[i].ToDescription(), Value = enums[i].ToString() });
                }
            }

            return selectList;
        }
    }

}
