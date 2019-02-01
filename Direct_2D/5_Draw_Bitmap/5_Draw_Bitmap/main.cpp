# include "ExportToBMP.h"

int main()
{
	ExportToBMP * ptrExport = new ExportToBMP();

	ptrExport->DrawBitmap();

	delete ptrExport;
	ptrExport = NULL;
}