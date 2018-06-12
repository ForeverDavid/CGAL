#pragma once

#include "stdafx.h"
#include "Primatives/Point2.h"
#include "Descriptors/MeshDescriptor.h"
#include "Descriptors/TriangleIndex.h"

using namespace Primatives;
using namespace Descriptors;

namespace ConformingTriangulation2
{

	extern "C"
	{

		CGALWRAPPERAPI CGALResult CALLCON Conforming2_InsertPoints2f(const Point2f* inPoints, int inSize, BOOL close);

		CGALWRAPPERAPI void CALLCON Conforming2_InsertSeed2f(Point2f point);

		CGALWRAPPERAPI void CALLCON Conforming2_Clear();

		CGALWRAPPERAPI void CALLCON Conforming2_Release();

		CGALWRAPPERAPI CGALResult CALLCON Conforming2_RefineMesh(int iterations, float angleBounds, float lengthBounds);

		CGALWRAPPERAPI CGALResult CALLCON Conforming2_Triangulate(MeshDescriptor& descriptor);

		CGALWRAPPERAPI Point2f CALLCON Conforming2_GetPoint2f(int i);

		CGALWRAPPERAPI TriangleIndex CALLCON Conforming2_GetTriangle(int i);

		CGALWRAPPERAPI TriangleIndex CALLCON Conforming2_GetNeighbor(int i);

	}

}
