using Configuration.ConfigKeys.Accomodators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.ConfigKeys
{
	public sealed class RenderKey: ConfigKey
	{
		#region Consts

		private const string KEY_NAME = "render";

		private const string VAL_NAME_FRAME_SKIP = "frameskip";
		private const string VAL_NAME_ASPECT = "aspect";
		private const string VAL_NAME_SCALER = "scaler";
		#endregion

		#region Properties

		public override string Name => KEY_NAME;

		[ConfigValue(VAL_NAME_FRAME_SKIP)]
		public int FrameSkip { get; set; }

		[ConfigValue(VAL_NAME_ASPECT,
			Accomodator = typeof(SmallLettersAccomodator),
			AccomodatorParam = typeof(DefaultTypeParser<bool>))]
		public bool DoAspectCorrection { get; set; }

		[ConfigValue(VAL_NAME_SCALER,
			Accomodator = typeof(IllegalEnumValAccomodator<Scaler>),
			AccomodatorParam = typeof(SmallLettersAccomodator))]
		public Scaler Scaler { get; set; }
		#endregion
	}
}
