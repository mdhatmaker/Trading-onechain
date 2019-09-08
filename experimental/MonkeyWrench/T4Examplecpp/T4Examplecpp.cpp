// T4Examplecpp.cpp : main project file.

#include "stdafx.h"
#include "frmMain.h"

using namespace T4Examplecpp;

[STAThreadAttribute]
int main(array<System::String ^> ^args)
{
	// Enabling Windows XP visual effects before any controls are created
	System::Windows::Forms::Application::EnableVisualStyles();
	System::Windows::Forms::Application::SetCompatibleTextRenderingDefault(false); 

	// Create the main window and run it
	try
	{
		System::Windows::Forms::Application::Run(gcnew frmMain());
	}
	catch (Exception ^e)
	{
		MessageBox::Show("Exception: " + e->ToString(),"Exception", MessageBoxButtons::OK, 
							MessageBoxIcon::Exclamation);							
	}
	return 0;
}
