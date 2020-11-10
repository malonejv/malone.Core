using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyDescription("Esta librería provee una estructura de clases base para administrar operaciones CRUD e integrar con un repositorio de datos.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyProduct("malone.Core")]
[assembly: AssemblyCulture("")]

//Internals visibility in malone.Core
[assembly: InternalsVisibleTo("malone.Core.WebApi")]
[assembly: InternalsVisibleTo("malone.Core.EF")]
[assembly: InternalsVisibleTo("malone.Core.AdoNet")]
[assembly: InternalsVisibleTo("malone.Core.Identity")]
[assembly: InternalsVisibleTo("malone.Core.Identity.AdoNet")]
[assembly: InternalsVisibleTo("malone.Core.Identity.AdoNet.SqlServer")]
[assembly: InternalsVisibleTo("malone.Core.Identity.EntityFramework")]
[assembly: InternalsVisibleTo("malone.Core.Unity.ModulesInitializers")]


// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("b59a92f9-abdc-4200-a80d-58a5ab936591")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]


