using System;
using System.Collections;
using System.ComponentModel;

namespace EiMM.View.Manager
{
    /// <summary>
    /// vgl: http://stackoverflow.com/questions/4306743/wpf-data-binding-how-to-data-bind-an-enum-to-combo-box-using-xaml
    /// </summary>
    public class EnumerationManager
    {
        public static Array GetValues(Type enumeration)
        {
            var wArray = Enum.GetValues(enumeration);
            var wFinalArray = new ArrayList();
            foreach (Enum wValue in wArray)
            {
                var fi = enumeration.GetField(wValue.ToString());
                if (null == fi) continue;
                var wBrowsableAttributes = fi.GetCustomAttributes(typeof(BrowsableAttribute), true) as BrowsableAttribute[];
                if (wBrowsableAttributes != null && wBrowsableAttributes.Length > 0)
                {
                    //  If the Browsable attribute is false
                    if (wBrowsableAttributes[0].Browsable == false)
                    {
                        // Do not add the enumeration to the list.
                        continue;
                    }
                }
                var wDescriptions = fi.GetCustomAttributes(typeof(DescriptionAttribute), true) as DescriptionAttribute[];
                if (wDescriptions != null && wDescriptions.Length > 0)
                    wFinalArray.Add(wDescriptions[0].Description);
                else
                    wFinalArray.Add(wValue);
            }

            return wFinalArray.ToArray();
        }
    }
}