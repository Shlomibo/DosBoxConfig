using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extansions.Object;

namespace Configuration.ConfigKeys
{
	public struct Resolution : IEquatable<Resolution>
	{
		#region Consts

		private const string ORIGINAL_STRING = "Original";
		private const string DESKTOP_STRING = "Desktop";

		private const int RES_VALUE_COUNT = 2;

		private const int DESKTOP_VALUE = -1;
		#endregion

		#region Fields

		private static readonly char[] resSeparator = { 'x', 'X' };

		public static readonly Resolution Original = new Resolution();
		public static readonly Resolution Desktop = new Resolution(DESKTOP_VALUE, DESKTOP_VALUE);
		#endregion

		#region Properties

		public int Width { get; }
		public int Height { get; }
		#endregion

		#region Ctor

		public Resolution(int width, int height)
			: this()
		{
			this.Width = width;
			this.Height = height;
		}
		#endregion

		#region Methods

		public override string ToString()
		{
			if (this == Resolution.Original)
			{
				return ORIGINAL_STRING;
			}
			else if (this == Resolution.Desktop)
			{
				return DESKTOP_STRING;
			}
			else
			{
				return $"{this.Width}x{this.Height}";
			}
		}

		public bool Equals(Resolution other) =>
			(this.Width == other.Width) &&
			(this.Height == other.Height);

		// override object.Equals
		public override bool Equals(object obj)
		{
			//       
			// See the full list of guidelines at
			//   http://go.microsoft.com/fwlink/?LinkID=85237  
			// and also the guidance for operator== at
			//   http://go.microsoft.com/fwlink/?LinkId=85238
			//

			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			// TODO: write your implementation of Equals() here
			return Equals((Resolution)obj);
		}

		// override object.GetHashCode
		public override int GetHashCode() =>
			ObjectExtansions.CreateHashCode(this.Width, this.Height);

		public static Resolution Parse(string s)
		{
			if (s == null)
			{
				throw new NullReferenceException(nameof(s));
			}

			Resolution result;

			if (!TryParse(s, out result))
			{
				throw new FormatException("Invalid resolution format");
			}

			return result;
		}

		public static bool TryParse(string s, out Resolution result)
		{
			result = Original;
			bool didSucceed = false;

			if (ORIGINAL_STRING.Equals(s, StringComparison.InvariantCultureIgnoreCase))
			{
				didSucceed = true;
			}
			else if (DESKTOP_STRING.Equals(s, StringComparison.InvariantCultureIgnoreCase))
			{
				didSucceed = true;
				result = Resolution.Desktop;
			}
			else
			{
				string[] split = s.Split(resSeparator);
				int width = 0;
				int height = 0;

				didSucceed = (split.Length == RES_VALUE_COUNT) &&
					int.TryParse(split[0], out width) &&
					int.TryParse(split[1], out height);

				if (didSucceed)
				{
					result = new Resolution(width, height);
				}
			}

			return didSucceed;
		}
		#endregion

		#region Operators

		public static bool operator ==(Resolution left, Resolution right) =>
			left.Equals(right);

		public static bool operator !=(Resolution left, Resolution right) =>
			!(left == right);
		#endregion
	}
}
