#pragma once

#include "stdafx.h"
#include "Primatives/Point2.h"

using namespace Primatives;

namespace PolygonIntersection2
{

	extern "C"
	{

		CGALWRAPPERAPI void CALLCON Intersection2_PushPolygon2f(const Point2f* inPoints, int inSize);

		CGALWRAPPERAPI void CALLCON Intersection2_AddHole2f(const Point2f* inPoints, int inSize);

		CGALWRAPPERAPI void CALLCON Intersection2_PopPolygon();

		CGALWRAPPERAPI void CALLCON Intersection2_PopAll();

		CGALWRAPPERAPI BOOL CALLCON Intersection2_ContainsPoint2f(Point2f point);

	}

}
