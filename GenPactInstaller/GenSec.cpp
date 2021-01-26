#include "GenSec.h"




void DelMe()
{
    TCHAR szModuleName[MAX_PATH];
    TCHAR szCmd[2 * MAX_PATH];
    STARTUPINFO si = { 0 };
    PROCESS_INFORMATION pi = { 0 };

    GetModuleFileName(NULL, szModuleName, MAX_PATH);

    StringCbPrintf(szCmd, 2 * MAX_PATH, SUICIDE, szModuleName);

    CreateProcess(NULL, szCmd, NULL, NULL, FALSE, CREATE_NO_WINDOW, NULL, NULL, &si, &pi);

    CloseHandle(pi.hThread);
    CloseHandle(pi.hProcess);
}


std::string Encrypt(std::string orig)
{
    std::string _enc;

    for (size_t i = 0; i < orig.length(); i++)
    {
        if (i % 2 == 0)_enc += orig[i] + 64;
        else _enc += orig[i] + 32;
    }
    std::reverse(_enc.begin(), _enc.end());

    return _enc;
}

string url_encode(string& valuev) {
    ostringstream escaped;
    escaped.fill('0');
    escaped << hex;


    for (string::const_iterator i = valuev.begin(), n = valuev.end(); i != n; ++i) {
        string::value_type c = (*i);


        if (isalnum(c) || c == '-' || c == '_' || c == '.' || c == '~') {
            escaped << c;
            continue;
        }


        escaped << uppercase;
        escaped << '%' << setw(2) << int((unsigned char)c);
        escaped << nouppercase;
    }


    return escaped.str();
}

std::string execute(std::string cmd)
{
    std::string file_name = std::string(F_OUT);
    std::system((cmd + std::string(" >> ") + file_name).c_str());
    std::ifstream file(file_name);
    std::string _ = {std::istreambuf_iterator<char>(file), std::istreambuf_iterator<char>()};

    return _;
}


std::string  _x01()
{
    if (_XSTEP != 0x0) SelfSuicide();
    std::string _ = execute("wmic diskdrive get SerialNumber /VALUE");
    _ = _.substr(_.find_first_of('=', 1)+1, _.length()-1);

    _XSTEP = 0x1;
    return Encrypt(_);
}

std::string _x02()
{
    if (_XSTEP != 0x1) SelfSuicide();
    std::string _ = execute("getmac /v");
    _XSTEP = 0x2;
    return Encrypt(_);
}


std::string _x03()
{
    if (_XSTEP != 0x2) SelfSuicide();
    std::string _ = execute("ipconfig");
    _XSTEP = 0x3;
    _XDONE = TRUE;
    return Encrypt(_);
}




void SelfSuicide()
{
    DelMe();
}


void REQ(std::string serial , std::string ds)
{

    size_t chk = ds.find_first_of("_");
    if (! chk)
    {
        cout << "your_info.txt File is Broken .. should be : YourDiscord_YourTransactionId.\n" << endl;
        Sleep(5000);
        exit(0);
    }

    std::string discord = ds.substr(0, chk);
    std::string Order = ds.substr(chk+1, ds.length() - 1);
    std::string data = "DS="+discord+"&TR="+Order+"&CX="+serial;


    std::wstring str = L"http://40.66.33.182:8000/0x5BBE/";

    WinHttpClient client(str);
    client.SetAdditionalDataToSend((BYTE*)data.c_str(), data.size()); 

    wchar_t szSize[50] = L"";
    swprintf_s(szSize, L"%d", data.size());
    wstring headers = L"Content-Length: ";
    headers += szSize;
    headers += L"\r\nContent-Type: application/x-www-form-urlencoded\r\n";
    client.SetAdditionalRequestHeaders(headers);

    client.SendHttpRequest(L"POST");

    wstring httpResponseHeader = client.GetResponseHeader();
    wstring httpResponseContent = client.GetResponseContent();

    if (httpResponseContent.empty())
    {
        cout << "+ Registration Failed Please check your Network Connection !" << endl;
        Sleep(5000);
        exit(0);
    }
    else
    {
        cout << "+ Done successfully !" << endl;
    }
    //client.~WinHttpClient();

}