# include <iostream>
# include <string>
# include <cstdlib>
# include <exdisp.h>
# include <cassert>
# include <fstream>
# include <Windows.h>

int main()
{
	HRESULT hret;
	hret = CoInitialize(NULL);
	assert(SUCCEEDED(hret));

	CLSID clsid;            // Get IE CLSID
	hret = CLSIDFromProgID(L"InternetExplorer.Application", &clsid);
	assert(SUCCEEDED(hret));

	IUnknown *p;            // Get IUnknown Interface
	hret = CoCreateInstance(clsid, NULL, CLSCTX_ALL, IID_IUnknown, reinterpret_cast<void**>(&p));
	assert(SUCCEEDED(hret));

	IDispatch * ptrDispatch;           // Get IDispatch Interface from IUnknown
	hret = p->QueryInterface(IID_IDispatch, reinterpret_cast<void**>(&ptrDispatch));
	assert(SUCCEEDED(hret));

	IWebBrowser2 * ptrWebBrsr;        // Get IWebBrowser2 Interface from IDispatch
	hret = ptrDispatch->QueryInterface(IID_IWebBrowser2, reinterpret_cast<void**>(&ptrWebBrsr));
	assert(SUCCEEDED(hret));

	IUnknown *s;            // Get IUnknown from IWebBrowser2
	hret = ptrWebBrsr->QueryInterface(IID_IUnknown, reinterpret_cast<void**>(&s));
	assert(SUCCEEDED(hret));

	///// Transitive ///////////////////////
	assert(p == s);
	////////////////////////////////////////

	VARIANT vEmpty, vFlags;
	VariantInit(&vEmpty);
	V_VT(&vFlags) = VT_I4;
	V_I4(&vFlags) = navOpenInNewTab;

	{
		std::wstring TEMP;
		std::wifstream SiteList("Site_List.txt");		// To Work Windows stream Files for windows strings

		//SiteList.open("", std::ios::in);
		if (SiteList)
		{
			while (std::getline(SiteList, TEMP))
			{
				const WCHAR * StrConvert = TEMP.c_str();
				std::wcout << TEMP << std::endl;
				BSTR bstrURL = SysAllocString(StrConvert);
				ptrWebBrsr->Navigate(bstrURL, &vFlags, &vEmpty, &vEmpty, &vEmpty);
				SysFreeString(bstrURL);
			}
		}
		SiteList.close();
	}

	ptrWebBrsr->Quit();

	p->Release();
	s->Release();
	ptrWebBrsr->Release();
	ptrDispatch->Release();

	CoUninitialize();
	system("pause");

	return 0;
}