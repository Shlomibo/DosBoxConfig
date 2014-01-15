using Configuration.ConfigKeys.Accomodators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.ConfigKeys
{
	public class DosBoxKey : ConfigKey
	{
		#region Consts

		private const string KEY_NAME = "dosbox";

		private const string VAL_NAME_LANGUAGE = "language";
		private const string VAL_NAME_MACHINE = "machine";
		private const string VAL_NAME_CAPTURES = "captures";
		private const string VAL_NAME_MEM_SIZE = "memsize";
		#endregion

		#region Properties

		public override string Name
		{
			get { return KEY_NAME; }
		}

		[ConfigValue(VAL_NAME_LANGUAGE)]
		public string LanguageFile { get; set; }

		[ConfigValue(VAL_NAME_MACHINE, 
			Accomodator=typeof(SmallLettersAccomodator),
			AccomodatorParam = typeof(DefaultTypeParser<MachineType>))]
		public MachineType MachineType { get; set; }

		[ConfigValue(VAL_NAME_CAPTURES)]
		public string CapturesDirectory { get; set; }

		[ConfigValue(VAL_NAME_MEM_SIZE)]
		public int MemorySize { get; set; }
		#endregion
	}
}
