using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Argix08.MakingContact.CE")]
[assembly: AssemblyDescription("Argix MakingContact Windows CE Application")]
[assembly: AssemblyConfiguration("Created 05/25/2011, jph")]
[assembly: AssemblyCompany("Argix Direct, Inc.")]
[assembly: AssemblyProduct("MakingContact")]
[assembly: AssemblyCopyright("2011 Argix Direct")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]


[assembly: AssemblyVersion("3.5.0.0")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: ComVisible(false)]
[assembly: Guid("0b33ea6e-a554-4f2e-b3cf-9daa3aca6f57")]

// Below attribute is to suppress FxCop warning "CA2232 : Microsoft.Usage : Add STAThreadAttribute to assembly"
// as Device app does not support STA thread.
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage","CA2232:MarkWindowsFormsEntryPointsWithStaThread")]
