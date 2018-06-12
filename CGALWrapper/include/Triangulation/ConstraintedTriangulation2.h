#pragma once

#include "stdafx.h"
#include "Primatives/Point2.h"
#include "Descriptors/MeshDescriptor.h"
#include "Descriptors/TriangleIndex.h"

using namespace Primatives;
using namespace Descriptors;

namespace ConstraintedTriangulation2
{

	extern "C"
	{

		CGALWRAPPERAPI CGALResult CALLCON Constrainted2_InsertPoints2f(const Point2f* inPoints, int inSize, BOOL close);

		CGALWRAPPERAPI void CALLCON Constrainted2_Clear();

		CGALWRAPPERAPI void CALLCON Constrainted2_Release();

		CGALWRAPPERAPI CGALResult CALLCON Constrainted2_Triangulate(MeshDescriptor& descriptor);

		CGALWRAPPERAPI Point2f CALLCON Constrainted2_GetPoint2f(int i);

		CGALWRAPPERAPI TriangleIndex CALLCON Constrainted2_GetTriangle(int i);

	}

}
