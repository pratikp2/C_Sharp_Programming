#pragma once
# include"patterns.h"

void DrawPattern :: DrawGridPattern()
{
	ExportToBMP::GetBitmapRenderTarget()->FillRectangle(D2D1::RectF(0.0f, 0.0f, 4.0f, 1.0f), ExportToBMP::GetSolidColorBrush());
	ExportToBMP::GetBitmapRenderTarget()->FillRectangle(D2D1::RectF(0.0f, 0.1f, 1.0f, 4.0f), ExportToBMP::GetSolidColorBrush());
}