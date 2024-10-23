using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switcha.UI.Model
{
    public class OptionAndValue
    {
        public string Option { get; set; }
        public string Value { get; set; }

        public List<OptionAndValue> GetOptionValues(Type enumType)
        {
            var result = new List<OptionAndValue>();
            foreach (var e in Enum.GetNames(enumType))
            {
                result.Add(new OptionAndValue
                {
                    Option = e,
                    Value = Convert.ToInt32(Enum.Parse(enumType, e)).ToString()
                });
            }
            return result;
        }
    }
}
