#pragma once

#include "stdafx.h"
#include "Primatives/Point2.h"

using namespace Primatives;

namespace Polygon2
{

	extern "C"
	{

		CGALWRAPPERAPI void CALLCON Polygon2_LoadPoints2f(const Point2f* inPoints, int inSize);

		CGALWRAPPERAPI void CALLCON Polygon2_Release();

		CGALWRAPPERAPI BOOL CALLCON Polygon2_IsSimple();

		CGALWRAPPERAPI BOOL CALLCON Polygon2_IsConvex();

		CGALWRAPPERAPI int CALLCON Polygon2_Orientation();

		CGALWRAPPERAPI float CALLCON Polygon2_SignedArea();
	}

}
