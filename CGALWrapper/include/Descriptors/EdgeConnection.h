#pragma once

namespace Descriptors
{

	typedef struct EdgeConnection
	{
		int edge;
		int previous;
		int next;
		int opposite;
	} EdgeConnection;

}
