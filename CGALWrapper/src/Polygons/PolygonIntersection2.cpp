
#include "stdafx.h"
#include "Polygons/PolygonIntersection2.h"

#include <CGAL/Exact_predicates_inexact_constructions_kernel.h>
#include <CGAL/Polygon_2.h>
#include <CGAL/Polygon_with_holes_2.h>
#include <CGAL/enum.h>

using namespace std;

namespace PolygonIntersection2
{

	typedef CGAL::Exact_predicates_inexact_constructions_kernel K;
	typedef K::Point_2 Point;
	typedef CGAL::Polygon_2<K> Polygon;
	typedef CGAL::Polygon_with_holes_2<K> PolygonWithHoles;

	vector<PolygonWithHoles> polygons;

	CGALWRAPPERAPI void CALLCON Intersection2_PushPolygon2f(const Point2f* inPoints, int inSize)
	{
		vector<Point> points = ToPointArray2<Point, Point2f>(inPoints, inSize);
		polygons.push_back(PolygonWithHoles(Polygon(points.begin(), points.end())));
	}

	CGALWRAPPERAPI void CALLCON Intersection2_AddHole2f(const Point2f* inPoints, int inSize)
	{
		size_t size = polygons.size();
		if (size == 0) return;

		vector<Point> points = ToPointArray2<Point, Point2f>(inPoints, inSize);
		Polygon hole = Polygon(points.begin(), points.end());

		polygons.back().add_hole(hole);
	}

	CGALWRAPPERAPI void CALLCON Intersection2_PopPolygon()
	{
		size_t size = polygons.size();
		if (size == 0) return;

		polygons.pop_back();
	}

	CGALWRAPPERAPI void CALLCON Intersection2_PopAll()
	{
		polygons.clear();
	}

	CGALWRAPPERAPI BOOL CALLCON Intersection2_ContainsPoint2f(Point2f point)
	{

		Point p(point.x, point.y);

		for each (auto polygon in polygons)
		{
			if (polygon.outer_boundary().bounded_side(p) == CGAL::ON_BOUNDED_SIDE)
			{
				bool contains = true;

				for(auto hole = polygon.holes_begin(); hole != polygon.holes_end(); ++hole)
				{
					if (hole->bounded_side(p) != CGAL::ON_UNBOUNDED_SIDE)
					{
						contains = false;
						break;
					}
				}

				if (contains) return true;
			}
		}

		return false;
	}

}