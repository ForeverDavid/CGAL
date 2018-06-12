
#include "stdafx.h"
#include "Polygons/PolygonBoolean2.h"

#include <CGAL/Exact_predicates_exact_constructions_kernel.h>
#include <CGAL/Boolean_set_operations_2.h>

using namespace std;

namespace PolygonBoolean2
{

	typedef CGAL::Exact_predicates_exact_constructions_kernel Kernel;
	typedef Kernel::Point_2 Point;
	typedef CGAL::Polygon_2<Kernel> Polygon;
	typedef CGAL::Polygon_with_holes_2<Kernel> PolygonWithHoles;

	PolygonWithHoles A, B;
	vector<PolygonWithHoles> polygons;

	PolygonWithHoles focusPolygon;
	vector<PolygonWithHoles> focusHoles;

	void LoadPolygonPoints(PolygonWithHoles& polygon, const vector<Point>& points)
	{
		polygon = PolygonWithHoles(Polygon(points.begin(), points.end()));
	}

	CGALWRAPPERAPI void CALLCON Boolean2_A_LoadPoints2f(const Point2f* inPoints, int inSize)
	{
		vector<Point> points = ToPointArray2<Point, Point2f>(inPoints, inSize);
		LoadPolygonPoints(A, points);
	}

	CGALWRAPPERAPI void CALLCON Boolean2_A_AddHole2f(const Point2f* inPoints, int inSize)
	{
		vector<Point> points = ToPointArray2<Point, Point2f>(inPoints, inSize);
		A.add_hole(Polygon(points.begin(), points.end()));
	}

	CGALWRAPPERAPI void CALLCON Boolean2_B_LoadPoints2f(const Point2f* inPoints, int inSize)
	{
		vector<Point> points = ToPointArray2<Point, Point2f>(inPoints, inSize);
		LoadPolygonPoints(B, points);
	}

	CGALWRAPPERAPI void CALLCON Boolean2_B_AddHole2f(const Point2f* inPoints, int inSize)
	{
		vector<Point> points = ToPointArray2<Point, Point2f>(inPoints, inSize);
		B.add_hole(Polygon(points.begin(), points.end()));
	}

	CGALWRAPPERAPI void CALLCON Boolean2_Clear()
	{
		A.clear();
		B.clear();
		focusPolygon.clear();
		focusHoles.clear();
		polygons.clear();
	}

	CGALWRAPPERAPI void CALLCON Boolean2_Release()
	{
		A = PolygonWithHoles();
		B = PolygonWithHoles();
		focusPolygon = PolygonWithHoles();
		focusHoles.resize(0);
		polygons.resize(0);
	}

	CGALWRAPPERAPI BOOL CALLCON Boolean2_DoIntersect()
	{

		try
		{
			return CGAL::do_intersect(A, B);
		}
		catch (...)
		{
			return false;
		}
	}

	CGALWRAPPERAPI int CALLCON Boolean2_Union()
	{

		try
		{
			PolygonWithHoles unionAB;
			polygons.clear();

			if (CGAL::join(A, B, unionAB))
			{
				polygons.push_back(unionAB);
			}
			else
			{
				polygons.push_back(A);
				polygons.push_back(B);
			}

			return int(polygons.size());
		}
		catch (...)
		{
			return 0;
		}
	}

	CGALWRAPPERAPI int CALLCON Boolean2_Intersection()
	{
		try
		{
			std::list<PolygonWithHoles> intersection;
			polygons.clear();

			CGAL::intersection(A, B, std::back_inserter(polygons));

			return int(polygons.size());
		}
		catch (...)
		{
			return 0;
		}
	}

	CGALWRAPPERAPI int CALLCON Boolean2_Difference()
	{
		try
		{
			std::list<PolygonWithHoles> intersection;
			polygons.clear();

			CGAL::difference(A, B, std::back_inserter(polygons));

			return int(polygons.size());
		}
		catch (...)
		{
			return 0;
		}
	}

	CGALWRAPPERAPI int CALLCON Boolean2_SymmetricDifference()
	{
		try
		{
			std::list<PolygonWithHoles> intersection;
			polygons.clear();

			CGAL::symmetric_difference(A, B, std::back_inserter(polygons));

			return int(polygons.size());
		}
		catch (...)
		{
			return 0;
		}
	}

	CGALWRAPPERAPI void CALLCON Boolean2_PointToPolygon(int polyIndex)
	{
		focusPolygon = polygons[polyIndex];
		focusHoles = vector<PolygonWithHoles>(focusPolygon.holes_begin(), focusPolygon.holes_end());
	}

	CGALWRAPPERAPI int CALLCON Boolean2_NumPolygonPoints()
	{
		return int(focusPolygon.outer_boundary().size());
	}

	CGALWRAPPERAPI int CALLCON Boolean2_NumPolygonHoles()
	{
		return int(focusHoles.size());
	}

	CGALWRAPPERAPI int CALLCON Boolean2_NumHolePoints(int holeIndex)
	{
		return int(focusHoles[holeIndex].outer_boundary().size());
	}

	CGALWRAPPERAPI Point2f CALLCON Boolean2_GetPolygonPoint2f(int pointIndex)
	{
		Point p = focusPolygon.outer_boundary()[pointIndex];

		float x = float(CGAL::to_double(p.x()));
		float y = float(CGAL::to_double(p.y()));

		return { x, y};
	}

	CGALWRAPPERAPI Point2f CALLCON Boolean2_GetHolePoint2f(int holeIndex, int pointIndex)
	{
		Point p = focusHoles[holeIndex].outer_boundary()[pointIndex];

		float x = float(CGAL::to_double(p.x()));
		float y = float(CGAL::to_double(p.y()));

		return{ x, y };
	}

}