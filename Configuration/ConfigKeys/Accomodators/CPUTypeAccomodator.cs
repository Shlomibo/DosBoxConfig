using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.ConfigKeys.Accomodators
{
	class CPUTypeAccomodator : ConfigValueAccomodator<CPUType, string>
	{
		#region Fields

		private static readonly Dictionary<CPUType, string> translation;
		private static readonly Dictionary<string, CPUType> backTranslation; 
		#endregion

		#region Ctor

		static CPUTypeAccomodator()
		{
			translation = new Dictionary<CPUType, string>()
				{
					{ CPUType.I386, "386" },
					{ CPUType.Slow386, "386_slow" },
					{ CPUType.Slow486, "486_slow" },
					{ CPUType.SlowPentium, "pentium_slow" },
					{ CPUType.Prefetch386, "386_prefetch" },
				};

			backTranslation = translation.ToDictionary(
				keyValue => keyValue.Value,
				keyValue => keyValue.Key);
		} 
		#endregion

		public override string Accomodate(CPUType value)
		{
			return translation[value];
		}

		public override CPUType AccomodateBack(string value)
		{
			return backTranslation[value];
		}
	}
}
