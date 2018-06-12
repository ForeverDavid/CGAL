
#include "stdafx.h"
#include "ConvexHull/ConvexHull2.h"

#include <CGAL/Exact_predicates_inexact_constructions_kernel.h>
#include <CGAL/convex_hull_2.h>
#include <CGAL/convexity_check_2.h>

using namespace std;

namespace ConvexHull2
{

	typedef CGAL::Exact_predicates_inexact_constructions_kernel K;
	typedef K::Point_2 Point;

	vector<Point> m_hull, m_points;

	CGALWRAPPERAPI void CALLCON ConvexHull2_Clear()
	{
		m_hull.clear();
		m_points.clear();
	}

	CGALWRAPPERAPI void CALLCON ConvexHull2_Release()
	{
		m_hull.resize(0);
		m_points.resize(0);
	}

	CGALWRAPPERAPI void CALLCON ConvexHull2_LoadPoints2f(const Point2f* inPoints, int inSize)
	{
		ConvexHull2_Clear();
		ToPointArray2<Point, Point2f>(m_points, inPoints, inSize);
	}

	CGALWRAPPERAPI int CALLCON ConvexHull2_FindHull()
	{
		if (m_points.size() < 3) return 0;

		try
		{
			m_hull.clear();
			CGAL::convex_hull_2(m_points.begin(), m_points.end(), back_inserter(m_hull));
			return int(m_hull.size());
		}
		catch(...)
		{
			m_hull.clear();
			return 0;
		}

	}

	CGALWRAPPERAPI BOOL CALLCON ConvexHull2_IsStronglyConvex(BOOL ccw)
	{
		if (m_points.size() < 3) return false;

		try
		{
			if (ccw)
				return CGAL::is_ccw_strongly_convex_2(m_points.begin(), m_points.end());
			else
				return CGAL::is_cw_strongly_convex_2(m_points.begin(), m_points.end());
		}
		catch (...)
		{
			return false;
		}
	}

	CGALWRAPPERAPI Point2f CALLCON ConvexHull2_GetHullPoint2f(int i)
	{
		Point p = m_hull[i];
		return{ float(p[0]) , float(p[1]) };
	}

}