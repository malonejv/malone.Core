//<author>Javier L�pez Malone</author>
//<date>25/11/2020 02:48:19</date>

using System.Reflection;

[assembly: AssemblyTitle("malonejv.Core")]
[assembly: AssemblyCompany("Javier López Malone")]

[assembly: AssemblyCopyright("Copyright ® Javier López Malone 2022")]

/*
 * Actualizar siempre AssemblyFileVersion.
 * El script se encarga de actualizar el resto.
 * Al hacer merge tanto de feature -> development
 * como de master -> development y development -> master
 * SIEMPRE tomar la versión de development.
 */
[assembly: AssemblyFileVersion("2.1.12.0")]

/*NO MODIFICAR*/
/**************/
[assembly: AssemblyVersion("2.1.12")]
[assembly: AssemblyInformationalVersion("2.1.12.0-beta")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
/**************/
/*NO MODIFICAR*/
