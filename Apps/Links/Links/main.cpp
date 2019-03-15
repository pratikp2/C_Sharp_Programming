# include <exdisp.h>
# include <cassert>

int main(void)
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

	IDispatch *q;           // Get IDispatch Interface from IUnknown
	hret = p->QueryInterface(IID_IDispatch, reinterpret_cast<void**>(&q));
	assert(SUCCEEDED(hret));

	IWebBrowser2 *r;        // Get IWebBrowser2 Interface from IDispatch
	hret = q->QueryInterface(IID_IWebBrowser2, reinterpret_cast<void**>(&r));
	assert(SUCCEEDED(hret));

	IUnknown *s;            // Get IUnknown from IWebBrowser2
	hret = r->QueryInterface(IID_IUnknown, reinterpret_cast<void**>(&s));
	assert(SUCCEEDED(hret));

	///// Transitive //////////////////////////
	assert(p == s);
	////////////////////////////////////////

	VARIANT vEmpty;
	VariantInit(&vEmpty);
	VARIANT vFlags;
	V_VT(&vFlags) = VT_I4;
	V_I4(&vFlags) = navOpenInNewTab;

	BSTR bstrURL1 = SysAllocString(L"http://www.google.com");
	BSTR bstrURL2 = SysAllocString(L"http://www.youtube.com");

	r->Navigate(bstrURL1, &vFlags, &vEmpty, &vEmpty, &vEmpty);
	r->Navigate(bstrURL2, &vFlags, &vEmpty, &vEmpty, &vEmpty);

	r->Quit();

	SysFreeString(bstrURL1);
	SysFreeString(bstrURL2);


	p->Release();
	q->Release();
	r->Release();
	s->Release();

	CoUninitialize();
	return 0;
}