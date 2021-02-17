using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WeSplitApp
{
	public class ExpenseMonneyRule : ValidationRule
	{
		public int min { get; set; }
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			String money = (String)value;

			try
			{
				foreach (var i in money)
				{
					if (!Char.IsDigit(i))
						throw new Exception();
				}
			}
			catch (Exception)
			{
				return new ValidationResult(false, "Số tiền phải là ký tự số");
			}

			if ((money.Length < min))
				return new ValidationResult(false, "Số tiền phải lớn hơn " + min);
			else
				return ValidationResult.ValidResult;
		}
	};
}
