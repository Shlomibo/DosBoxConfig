using Configuration.ConfigKeys.Accomodators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.ConfigKeys
{
	public class CPUKey : ConfigKey
	{
		#region Consts

		private const string KEY_NAME = "cpu";

		private const string VAL_NAME_CORE = "core";
		private const string VAL_NAME_CPU_TYPE = "cputype";
		private const string VAL_NAME_CYCLES = "cycles";
		private const string VAL_NAME_CYCLE_UP = "cycleup";
		private const string VAL_NAME_CYCLE_DOWN = "cycledown";
		#endregion

		#region Properties

		public override string Name
		{
			get { return KEY_NAME; }
		}

		[ConfigValue(VAL_NAME_CORE, 
			Accomodator = typeof(SmallLettersAccomodator))]
		public Core Core { get; set; }

		[ConfigValue(VAL_NAME_CPU_TYPE,
			Accomodator = typeof(CPUTypeAccomodator))]
		public CPUType CPUType { get; set; }

		[ConfigValue(VAL_NAME_CYCLES,
			Accomodator = typeof(SmallLettersAccomodator),
			TypeParser = typeof(CyclesParser))]
		public Cycles Cycles { get; set; }

		[ConfigValue(VAL_NAME_CYCLE_UP)]
		public int CycleUp { get; set; }

		[ConfigValue(VAL_NAME_CYCLE_DOWN)]
		public int CycleDown { get; set; }
		#endregion
	}
}
