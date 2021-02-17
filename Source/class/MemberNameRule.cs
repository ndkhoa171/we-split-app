using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WeSplitApp
{
    public class MemberNameRule : ValidationRule
    {
        public int min { get; set; }
        public int max { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            String name = (String)value;

            try
            {
                foreach (var i in name)
                {
                    if (!Char.IsLetter(i) && !char.IsWhiteSpace(i))
                        throw new Exception();
                }
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Tên thành viên không được chứa ký tự đặc biệt");

            }

            if ((name.Length < min) || (name.Length > max))
                return new ValidationResult(false, "Tên chuyến đi phải có độ dài từ " + min + " đến " + max);
            else
                return ValidationResult.ValidResult;                         
        }
    };

}
