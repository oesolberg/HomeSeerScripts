using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

static string _logName="jallabjalla";

public int Main(object[] args)
{
	//Console.WriteLine("Start");
	
	LogToHomeseer("Start WarmWater script" );
	//LogToHomeseer(args[0] as string );
	var listTest=new List<string>();
	listTest.Add("jalla");
	listTest.Add("falla");
	listTest.Add("balla");
hs.WriteLog(_logName,listTest[1]);
SetHsDevice(265,listTest[0]);
//hs.SetDeviceString(265,listTest[1],true);
	//UpdateHomeSeerDevices(listTest);
	return 1;
}

public  void LogToHomeseer(string message)
{	
	//Console.WriteLine(message);
	hs.WriteLog(_logName, message);
}

public void UpdateHomeSeerDevices(List<string> formattedData)
{
	//LogToHomeseer(formattedData[2]);
		SetHsDevice(265,formattedData[0]);
		SetHsDevice(266,formattedData[1]);
		SetHsDevice(267,formattedData[2]);
		
	
}

public void SetHsDevice(int deviceNumber, string valueToUpdateTo)
{
	
	hs.SetDeviceString(deviceNumber,valueToUpdateTo,true);
	
}