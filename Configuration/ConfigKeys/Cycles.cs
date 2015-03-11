using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extansions.Object;

namespace Configuration.ConfigKeys
{
	public struct Cycles : IEquatable<Cycles>, IComparable<Cycles>
	{
		#region Consts

		private const string AUTO_STRING = "Auto";
		private const string MAX_STRING = "Max";
		private const string FIXED_STRING = "Fixed ";
		private const string FIXED_STRING_FORMAT = FIXED_STRING + "#{0}";
		private const char CYCLES_SEPARATOR = '#';
		private const int FIXED_PARTS_COUNT = 2;

		private const int IDX_TYPE = 0;
		private const int IDX_CYCLES = 1;
		#endregion

		#region Fields

		public static readonly Cycles Auto = new Cycles(false);
		public static readonly Cycles Max = new Cycles(true);

		private readonly bool isMax;
		#endregion
		
		#region Properties

		public int? CyclesCount { get; }

		public bool IsAuto => !this.IsMax && !this.IsFixed;

		public bool IsMax => this.isMax && !this.IsFixed;

		public bool IsFixed => this.CyclesCount.HasValue;
		#endregion

		#region Ctor

		public Cycles(int cycles)
			: this()
		{
			this.isMax = false;
			this.CyclesCount = cycles;
		}
		
		private Cycles(bool isMax)
			: this()
		{
			this.isMax = isMax;
		}
		#endregion

		#region Methods

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
			return Equals((Cycles)obj);
		}

		// override object.GetHashCode
		public override int GetHashCode() =>
			ObjectExtansions.CreateHashCode(this.isMax, this.CyclesCount);

		public bool Equals(Cycles other) =>
			(this.isMax == other.isMax) &&
			(this.CyclesCount == other.CyclesCount);

		public int CompareTo(Cycles other)
		{
			if ((this.isMax && !other.isMax) || (other.IsAuto))
			{
				return 1;
			}
			else if ((!this.isMax && other.isMax) || (this.IsAuto))
			{
				return -1;
			}
			else
			{
				return this.CyclesCount.Value.CompareTo(other.CyclesCount.Value);
			}

		}

		public override string ToString()
		{
			if (this.IsAuto)
			{
				return AUTO_STRING;
			}
			else if (this.IsMax)
			{
				return MAX_STRING;
			}
			else
			{
				return string.Format(FIXED_STRING_FORMAT, this.CyclesCount);
			}
		}

		public static Cycles Parse(string s)
		{
			if (s == null)
			{
				throw new NullReferenceException(nameof(s));
			}

			Cycles result;

			if (!TryParse(s, out result))
			{
				throw new FormatException("Invalid cycles format");
			}

			return result;
		}

		public static bool TryParse(string s, out Cycles result)
		{
			result = new Cycles();
			bool didSucceeded = false;

			if (AUTO_STRING.Equals(s, StringComparison.InvariantCultureIgnoreCase))
			{
				didSucceeded = true;
				result = ConfigKeys.Cycles.Auto;
			}
			else if (MAX_STRING.Equals(s, StringComparison.InvariantCultureIgnoreCase))
			{
				didSucceeded = true;
				result = ConfigKeys.Cycles.Max;
			}
			else
			{
				string[] splitted = s.Split(CYCLES_SEPARATOR);
				int cycles;

				if ((splitted.Length == FIXED_PARTS_COUNT) &&
					FIXED_STRING.Equals(splitted[IDX_TYPE], StringComparison.InvariantCultureIgnoreCase) &&
					int.TryParse(splitted[IDX_CYCLES], out cycles)) 
				{
					didSucceeded = true;
					result = new Cycles(cycles);
				}
			}

			return didSucceeded;
		}
		#endregion

		#region Operators

		public static bool operator ==(Cycles left, Cycles right) =>
			left.Equals(right);

		public static bool operator !=(Cycles left, Cycles right) =>
			!(left == right);

		public static bool operator <(Cycles left, Cycles right) =>
			left.IsFixed && right.IsFixed &&
			(left.CompareTo(right) < 0);

		public static bool operator >(Cycles left, Cycles right) =>
			left.IsFixed && right.IsFixed &&
			(left.CompareTo(right) > 0);

		public static bool operator <=(Cycles left, Cycles right) =>
			left.IsFixed && right.IsFixed &&
			!(left > right);

		public static bool operator >=(Cycles left, Cycles right) =>
			left.IsFixed && right.IsFixed &&
			!(left < right);
		#endregion
	}
}
