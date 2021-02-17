using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WeSplitApp
{
    public class MemberPhoneRule : ValidationRule
    {
        public int min { get; set; }
        public int max { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            String phone = (String)value;

            try
            {
                foreach (var i in phone)
                {
                    if (!Char.IsDigit(i))
                        throw new Exception();
                }
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Số điện thoại phải là ký tự số");

            }


            if ((phone.Length < min) || (phone.Length > max))
                return new ValidationResult(false, "Số điện thoại phải là " + min + " hoặc " + max + " số");
            else
                return ValidationResult.ValidResult;
        }
    };

}
