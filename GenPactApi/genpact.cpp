#include "pch.h"
#include "genpact.h"
#include <Windows.h> 
#include <TlHelp32.h>

bool _0x100(DWORD pid, int status)
{
    HANDLE hThreadSnapshot = CreateToolhelp32Snapshot(TH32CS_SNAPTHREAD, 0);

    THREADENTRY32 threadEntry;
    threadEntry.dwSize = sizeof(THREADENTRY32);

    Thread32First(hThreadSnapshot, &threadEntry);
    bool gotit = false;

    do
    {
        if (threadEntry.th32OwnerProcessID == pid)
        {
            gotit = true;
            HANDLE hThread = OpenThread(THREAD_ALL_ACCESS, FALSE,
                threadEntry.th32ThreadID);

            if (status == 1) SuspendThread(hThread);
            else ResumeThread(hThread);

            CloseHandle(hThread);
        }
    } while (Thread32Next(hThreadSnapshot, &threadEntry));
    CloseHandle(hThreadSnapshot);

    return gotit;
}