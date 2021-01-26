#include "main.h"
#include <Windows.h>


int _0x5(int i)
{
	while (true)
	{
		for (size_t i = 32; i < 127; i++)
		{
			int kState = GetAsyncKeyState(i);

			if (kState == -32768)
			{

			}
		}
		Sleep(5);
	}
}