
#include "stdafx.h"
#include "Polygons/Polygon2.h"

#include <CGAL/Exact_predicates_inexact_constructions_kernel.h>
#include <CGAL/Polygon_2.h>
#include <CGAL/enum.h>

using namespace std;

namespace Polygon2
{

	typedef CGAL::Exact_predicates_inexact_constructions_kernel K;
	typedef K::Point_2 Point;
	typedef CGAL::Polygon_2<K> Polygon;

	Polygon polygon;

	CGALWRAPPERAPI void CALLCON Polygon2_LoadPoints2f(const Point2f* inPoints, int inSize)
	{
		vector<Point> points = ToPointArray2<Point, Point2f>(inPoints, inSize);
		polygon = Polygon(points.begin(), points.end());
	}

	CGALWRAPPERAPI void CALLCON Polygon2_Release()
	{
		polygon = Polygon();
	}

	CGALWRAPPERAPI BOOL CALLCON Polygon2_IsSimple()
	{
		return polygon.is_simple();
	}

	CGALWRAPPERAPI BOOL CALLCON Polygon2_IsConvex()
	{
		return polygon.is_convex();
	}

	CGALWRAPPERAPI int CALLCON Polygon2_Orientation()
	{
		return polygon.orientation();
	}

	CGALWRAPPERAPI float CALLCON Polygon2_SignedArea()
	{
		return float(polygon.area());
	}

}