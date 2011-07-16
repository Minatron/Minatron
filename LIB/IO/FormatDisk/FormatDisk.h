// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the FORMATDISK_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// FORMATDISK_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef FORMATDISK_EXPORTS
#define FORMATDISK_API __declspec(dllexport)
#else
#define FORMATDISK_API __declspec(dllimport)
#endif


#include <storemgr.h>

FORMATDISK_API bool fnFormatDisk(void);
