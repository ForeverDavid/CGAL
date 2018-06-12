// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently, but
// are changed infrequently
//

#pragma once

#include "targetver.h"

#define WIN32_LEAN_AND_MEAN             // Exclude rarely-used stuff from Windows headers
// Windows Header Files:
#include <windows.h>

#if defined(CGALWRAPPER_EXPORTS)
#   define CGALWRAPPERAPI   __declspec(dllexport)
#else 
#   define CGALWRAPPERAPI   __declspec(dllimport)
#endif  

#define CALLCON __cdecl

typedef enum CGALResult 
{
	CGAL_SUCCESS = 0,
	CGAL_ERROR = 1
}CGALResult;
