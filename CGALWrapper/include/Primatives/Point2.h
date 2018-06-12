#pragma once

#include "stdafx.h"
#include <vector>

namespace Primatives
{

	typedef struct Point2f {
		float   x;
		float   y;
	} Point2f;

	template <class POINT2, class REAL>
	void ToPointArray(std::vector<POINT2>& points, const REAL* inPoints, int inSize)
	{
		points.clear();

		if (inPoints == nullptr) return;
		if (inSize < 2) return;
		if (inSize % 2 != 0) return;

		int numPoints = inSize / 2;
		for (int i = 0; i < numPoints; i++)
		{
			REAL x = inPoints[i * 2 + 0];
			REAL y = inPoints[i * 2 + 1];
			points.push_back(POINT2(x, y));
		}
	}

	template <class POINT2, class REAL>
	std::vector<POINT2> ToPointArray(const REAL* inPoints, int inSize)
	{
		std::vector<POINT2> points;

		if (inPoints == nullptr) return points;
		if (inSize < 2) return points;
		if (inSize % 2 != 0) return points;

		int numPoints = inSize / 2;
		for (int i = 0; i < numPoints; i++)
		{
			REAL x = inPoints[i * 2 + 0];
			REAL y = inPoints[i * 2 + 1];
			points.push_back(POINT2(x, y));
		}

		return points;
	}

	template <class POINT2, class REAL2>
	void ToPointArray2(std::vector<POINT2>& points, const REAL2* inPoints, int inSize)
	{
		points.clear();

		if (inPoints == nullptr) return;
		if (inSize < 1) return;

		int numPoints = inSize;
		for (int i = 0; i < numPoints; i++)
		{
			double x = inPoints[i].x;
			double y = inPoints[i].y;
			points.push_back(POINT2(x, y));
		}
	}

	template <class POINT2, class REAL2>
	std::vector<POINT2> ToPointArray2(const REAL2* inPoints, int inSize)
	{
		std::vector<POINT2> points;

		if (inPoints == nullptr) return points;
		if (inSize < 1) return points;

		int numPoints = inSize;
		for (int i = 0; i < numPoints; i++)
		{
			double x = inPoints[i].x;
			double y = inPoints[i].y;
			points.push_back(POINT2(x, y));
		}

		return points;
	}

}
