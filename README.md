ApplicationLogging
---
ApplicationLogging provides simple interfaces and classes to allow logging from a client application.  The included interface IApplicationLogger can be injected into client classes, and exposes simple methods for capturing detailed logging information from these client classes.  ApplicationLogging includes implementations of IApplicationLogger which allow logging to a file or the console.  The ApplicationLogging.Adapters project additionally includes an adapter class to route ApplicationLogging log events to an instance of the log4net ILog interface.

The project was originally released as part of my [MethodInvocationRemoting](https://github.com/alastairwyse/MethodInvocationRemoting) project, but has been released here as a separate project to allow it to be referenced/used in other projects.

##### Links

A detailed sample implementation and use case...<br>
[http://www.oraclepermissiongenerator.net/methodinvocationremoting/sample-application-4.html](http://www.oraclepermissiongenerator.net/methodinvocationremoting/sample-application-4.html)

Code documentation...<br>
[http://www.oraclepermissiongenerator.net/methodinvocationremoting/ndoc/~ApplicationLogging.html](http://www.oraclepermissiongenerator.net/methodinvocationremoting/ndoc/~ApplicationLogging.html)
[http://www.oraclepermissiongenerator.net/methodinvocationremoting/ndoc/~ApplicationLogging.Adapters.html](http://www.oraclepermissiongenerator.net/methodinvocationremoting/ndoc/~ApplicationLogging.Adapters.html)

##### Notes
- After opening the solution in Visual Studio, the referenced NuGet packages should be restored using the 'Restore' button in the 'Manage NuGet Packages' window.