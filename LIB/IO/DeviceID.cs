#if DEMO
#else

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace  MapEngine
{
	public class DeviceID
	{
		
		#region WINCE
		#if WINCE
		
		const int FILE_ANY_ACCESS = 0x0;
		const int METHOD_BUFFERED = 0x0;
		const int FILE_DEVICE_DISK =  0x00000007;
		const int IOCTL_DISK_GET_STORAGEID =((FILE_DEVICE_DISK) << 16) | ((FILE_ANY_ACCESS) << 14) | ((0x709) << 2) | (METHOD_BUFFERED);


		[DllImport("coredll.dll", EntryPoint = "CreateFileW", SetLastError = true,CharSet=CharSet.Unicode, CallingConvention=CallingConvention.Winapi)]
		private static extern IntPtr CreateFile(
			string				fileName,
			uint				desiredAccess,
			uint				shareMode,
			IntPtr				securityAttributes,
			uint				creationDisposition,
			uint				flagsAndAttributes,
			IntPtr				templateFile
			);

		[DllImport ("coredll.dll")]
		private static extern bool DeviceIoControl( 
			IntPtr     hDevice,		
			Int32      dwIoControlCode,
			IntPtr     InputBuffer,	
			Int32      InputBufferSize,		
			byte[]     OutputBuffer,		
			Int32      OutputBufferSize,	
			ref Int32  BytesReturned,
			IntPtr     overlapped
			);

		[DllImport("coredll", EntryPoint = "CloseHandle", SetLastError = true,CallingConvention=CallingConvention.Winapi)]
		public static extern bool CloseHandle(
			IntPtr				handle
			);
		

        public static bool FotmatDevice(string path)
        {
            string[] p = path.Split(@"/\".ToCharArray());
            path = @"\" + p[1] + @"\Vol:";
            IntPtr hDevice = CreateFile(path,			// drive to open
                                        0,				// no access to the drive
                                        0x00000001 | 0x00000002,
                                        IntPtr.Zero,    // default security attributes
                                        3,				// disposition
                                        0,              // file attributes
                                        IntPtr.Zero
                                        );
            int blah = 0;
            bool result = DeviceIoControl(hDevice, 459296, IntPtr.Zero, 0, null, 0, ref blah, IntPtr.Zero);
            

            CloseHandle(hDevice);
            return result;
        }


	    public  static int[] GetArray(string path)
		{
			string[] p=path.Split(@"/\".ToCharArray());
			path=@"\"+p[1]+@"\Vol:";
			byte[] OutputBuffer = new byte[50];
			int BytesReturned=0;
			int[] ID=new int[5];
			
			IntPtr hDevice = CreateFile(path,			// drive to open
										0,				// no access to the drive
										0x00000001|0x00000002 , 
										IntPtr.Zero,    // default security attributes
										3,				// disposition
										0,              // file attributes
										IntPtr.Zero
										);      			
			bool res = DeviceIoControl(hDevice, IOCTL_DISK_GET_STORAGEID, IntPtr.Zero, 0, OutputBuffer, OutputBuffer.Length, ref BytesReturned, IntPtr.Zero);
					
			CloseHandle(hDevice);
			if (res)
			{
				Int32 IDOffset	= BitConverter.ToInt32(OutputBuffer,8);				
				for (int i = 0; i < 5; i++)
				{
					ID[i] = BitConverter.ToInt32(OutputBuffer,IDOffset);
					IDOffset+=4;
				}
			}		
			return ID;
		}

#endif
		#endregion
		#region WIN32
#if WIN32

		[DllImport ("kernel32.dll")]
		private static extern bool GetVolumeInformation(
			string RootPathName,			//LPCTSTR lpRootPathName
			byte[] VolumeNameBuffer,		//LPTSTR lpVolumeNameBuffer
			Int32 VolumeNameSize,			//DWORD nVolumeNameSize
			ref Int32 VolumeSerialNo,		//LPDWORD lpVolumeSerialNumber
			ref Int32 MaxComponentLength,	//LPDWORD lpMaximumComponentLength
			ref Int32 FileSystemFlags,		//LPDWORD lpFileSystemFlags
			byte[] FileSystemNameBuffer,	//LPTSTR lpFileSystemNameBuffer
			Int32 FileSystemNameSize		//DWORD nFileSystemNameSize
			);

		public static int[] GetArray(string path)
		{
			path=Path.GetPathRoot(path);
			int SerrialNo=0;
			int CML=0;
			int FSFlags=0;
			GetVolumeInformation(path,null,0,ref SerrialNo,ref CML,ref FSFlags,null,0);
			int[] ID=new int[5];
			ID[0]=SerrialNo;
			ID[1]=FSFlags;
			ID[2]=0;
			ID[3]=0;
			ID[4]=0;
			return ID;
		}
#endif
		#endregion
		public static string GetString(string path)
		{
			int[] ID=GetArray(path);
			return String.Format("{0:X8}-{1:X8}-{2:X8}-{3:X8}-{4:X8}",ID[0],ID[1],ID[2],ID[3],ID[4]);
		}
		public static int GetInt32(string path)
		{
			int[] ID=GetArray(path);
			return (ID[0]^ID[1]^ID[2]^ID[3]^ID[4]);
			//return 0;
		}
		
	}
}
#endif
