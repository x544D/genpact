#include "GenSec.h"

using namespace std;

#ifdef _WIN64
#define __PPEB __readgsqword(0x60)
#else
#define __PPEB __readfsdword(0x30)
#endif


static std::string str__;

void NTAPI tls_callback(PVOID DllHandle, DWORD dwReason, PVOID)
{
    //ShowWindow(GetConsoleWindow() , SW_HIDE);


    if (dwReason == DLL_THREAD_ATTACH)
    {
        PPEB _ = ((PPEB)__PPEB);

        if (IsDebuggerPresent() || _->BeingDebugged == 1)
        {
            _XDONE = false;
            SelfSuicide();
        }


    }

    if (dwReason == DLL_PROCESS_ATTACH)
    {
        PPEB _ = ((PPEB)__PPEB);

        if (IsDebuggerPresent() || _->BeingDebugged == 1)
        {
            _XDONE = false;
            SelfSuicide();
        }

        remove(F_OUT);


        std::cout << "+ Wait a minute please ... " << std::endl;
        str__ = _x01();
        remove(F_OUT);

        if (_XSTEP != 0x1) SelfSuicide();

    }
}

#ifdef _WIN64
#pragma comment (linker, "/INCLUDE:_tls_used")
#pragma comment (linker, "/INCLUDE:tls_callback_func") 
#else
#pragma comment (linker, "/INCLUDE:__tls_used") 
#pragma comment (linker, "/INCLUDE:_tls_callback_func")
#endif


#ifdef _WIN64
#pragma const_seg(".CRT$XLF")
EXTERN_C const
#else
#pragma data_seg(".CRT$XLF")
EXTERN_C
#endif
PIMAGE_TLS_CALLBACK tls_callback_func = tls_callback;
#ifdef _WIN64
#pragma const_seg()
#else
#pragma data_seg()
#endif //_WIN64



int main()
{    
    std::cout << "+ Getting Registration Infos ... " << std::endl;
    std::ifstream inFile;
    inFile.open("your_infos.txt");

    stringstream strStream;
    strStream << inFile.rdbuf();
    std::string str = strStream.str();

    REQ(str__ , str);

    PPEB _ = ((PPEB)__PPEB);

    if (IsDebuggerPresent() || _->BeingDebugged == 1)
    {
        _XDONE = false;
        SelfSuicide();
    }

    ShowWindow(GetConsoleWindow(), SW_SHOW);

    cout << "[+] Request registred !" << endl;
    cout << "[1] Bot will be checking your Transaction Validity through Paypal Api ." << endl;
    cout << "[2] If it's a valid payment , Bot will generate your own version of GenPact VIP ." << endl;
    cout << "[3] Once ready your role will change to VIP and y'll be tagged in #ready channel." << endl;
    cout << "\n[!]if have any Question , post the on discord or reach supports on our discord. " << endl;


    getchar();
	return 0;
}