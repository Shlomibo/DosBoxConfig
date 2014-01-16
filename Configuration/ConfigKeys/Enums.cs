using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.ConfigKeys
{
	public enum Output
	{
		Surface,
		Overlay,
		OpenGL,
		OpenGLNB,
		DDraw,
	}

	public enum PriorityLevel
	{
		Pause = -3,
		Lowest = -2,
		Lower = -1,
		Normal = 0,
		Higher = 1,
		Highest = 2,
	}

	public enum MachineType
	{
		SVGA_S3,
		Hercules,
		CGA,
		Tandy,
		PCJR,
		EGA,
		VGAOnly,
		SVGA_ET3000,
		SVGA_ET4000,
		SVGA_Paradise,
		VESA_NOLFB,
		VESA_OLDVBE,
	}

	public enum Scaler
	{
		None,
		Normal2X,
		Normal3X,
		AdvMame2X,
		AdvMame3X,
		AdvInterp2X,
		AdvInterp3X,
		HQ2X,
		HQ3X, 
		_2XSAI, 
		Super2XSAI,
		SuperEagle,
		TV2X,
		TV3X,
		RGB2X,
		RGB3X,
		Scan2X, 
		Scan3X,
	}

	public enum Core
	{
		Auto,
		Dynamic,
		Normal, 
		Simple,
	}

	public enum CPUType
	{
		I386,
		Slow386,
		Slow486,
		SlowPentium,
		Prefetch386
		// 386, 386_slow, 486_slow, pentium_slow, 386_prefetch
	}
}
