#pragma once

#include "stdafx.h"
#include "Primatives/Point2.h"

using namespace Primatives;

namespace PolygonSimplify2
{

	extern "C"
	{

		CGALWRAPPERAPI void CALLCON Simplify2_LoadPoints2f(const Point2f* inPoints, int inSize);

		CGALWRAPPERAPI void CALLCON Simplify2_Clear();

		CGALWRAPPERAPI void CALLCON Simplify2_Release();

		CGALWRAPPERAPI int CALLCON Simplify2_SquareDistCostSimplify(float threshold);

		CGALWRAPPERAPI int CALLCON Simplify2_ScaledSquareDistCostSimplify(float threshold);

		CGALWRAPPERAPI Point2f CALLCON Simplify2_GetSimplifiedPoint2f(int i);

	}

}
