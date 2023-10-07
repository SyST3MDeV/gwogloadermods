An actual readme with instructions for how to build your own mods is coming soon (tm)

To build:

create a folder named "Dependencies" in the root of the mod you want to build, then copy all dll's from Wormtown's "Managed" directory into it

create a folder named Harmony in the root of the mod you want to build, then copy the latest version of the libharmony dlls into it

then run "dotnet build -c Release" in the root of the mod you want to build

the built DLL needs to be hosted somewhere, and then change the GwogLoader endpoint in the main GwogLoader project to point to it