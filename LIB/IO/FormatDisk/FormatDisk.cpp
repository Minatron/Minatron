// FormatDisk.cpp : Defines the entry point for the DLL application.
//

#include "stdafx.h"
#include "FormatDisk.h"

#include <storemgr.h>


BOOL APIENTRY DllMain( HANDLE hModule, 
                       DWORD  ul_reason_for_call, 
                       LPVOID lpReserved
					 )
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
    return TRUE;
}


// This is an example of an exported function.
FORMATDISK_API bool fnFormatDisk( LPCTSTR szDeviceName)
{	  	
	  HANDLE hStore = OpenStore(szDeviceName);	  
	  DismountStore(hStore);
	  return (bool)FormatStore(hStore);               
}
