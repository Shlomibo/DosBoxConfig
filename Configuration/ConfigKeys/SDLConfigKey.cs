using Configuration.ConfigKeys.Accomodators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.ConfigKeys
{
	public sealed class SDLConfigKey : ConfigKey
	{
		#region Consts

		private const string KEY_NAME = "sdl";

		private const string VAL_NAME_FULL_SCREEN = "fullscreen";
		private const string VAL_NAME_FULL_DOUBLE = "fulldouble";
		private const string VAL_NAME_FULL_RESOLUTION = "fullresolution";
		private const string VAL_NAME_WINDOW_RESOLUTION = "windowresolution";
		private const string VAL_NAME_OUTPUT = "output";
		private const string VAL_NAME_AUTO_LOCK = "autolock";
		private const string VAL_NAME_SENSITIVITY = "sensitivity";
		private const string VAL_NAME_WAIT_ON_ERROR = "waitonerror";
		private const string VAL_NAME_PRIORITY = "priority";
		private const string VAL_NAME_MAPPER_FILE = "mapperfile";
		private const string VAL_NAME_USE_SCAN_CODES = "usescancodes";
		#endregion

		#region Properties

		public override string Name => KEY_NAME;

		[ConfigValue(VAL_NAME_FULL_SCREEN,
			Accomodator = typeof(SmallLettersAccomodator),
			AccomodatorParam = typeof(DefaultTypeParser<bool>))]
		public bool FullScreen { get; set; }

		[ConfigValue(VAL_NAME_FULL_DOUBLE, 
			Accomodator = typeof(SmallLettersAccomodator),
			AccomodatorParam = typeof(DefaultTypeParser<bool>))]
		public bool FullDoubleBuffering { get; set; }

		[ConfigValue(VAL_NAME_FULL_RESOLUTION, 
			Accomodator = typeof(SmallLettersAccomodator),
			AccomodatorParam=typeof(ResolutionParser))]
		public Resolution FullResolution { get; set; }

		[ConfigValue(VAL_NAME_WINDOW_RESOLUTION,
			Accomodator = typeof(SmallLettersAccomodator),
			AccomodatorParam = typeof(ResolutionParser))]
		public Resolution WindowResolution { get; set; }

		[ConfigValue(VAL_NAME_OUTPUT,
			Accomodator = typeof(SmallLettersAccomodator),
			AccomodatorParam = typeof(DefaultTypeParser<Output>))]
		public Output Output { get; set; }

		[ConfigValue(VAL_NAME_AUTO_LOCK,
			Accomodator = typeof(SmallLettersAccomodator),
			AccomodatorParam = typeof(DefaultTypeParser<bool>))]
		public bool AutoLock { get; set; }

		[ConfigValue(VAL_NAME_SENSITIVITY)]
		public int Sensitivity { get; set; }

		[ConfigValue(VAL_NAME_WAIT_ON_ERROR,
			Accomodator = typeof(SmallLettersAccomodator),
			AccomodatorParam = typeof(DefaultTypeParser<bool>))]
		public bool WaitOnError { get; set; }

		[ConfigValue(VAL_NAME_PRIORITY,
			Accomodator = typeof(SmallLettersAccomodator),
			AccomodatorParam = typeof(PriorityParser))]
		public Priority Priority { get; set; }

		[ConfigValue(VAL_NAME_MAPPER_FILE)]
		public string MapperFile { get; set; }

		[ConfigValue(VAL_NAME_USE_SCAN_CODES,
			Accomodator = typeof(SmallLettersAccomodator),
			AccomodatorParam = typeof(DefaultTypeParser<bool>))]
		public bool UseScanCodes { get; set; }
		#endregion
	}
}
