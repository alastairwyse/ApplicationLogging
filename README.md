ApplicationLogging
---

ApplicationLogging provides interfaces and classes to allow simple logging from a client application.  The included interface IApplicationLogger can be injected into client classes, and exposes simple methods for capturing detailed logging information from these client classes.  

The main benefit of ApplicationLogging is to provide an abstraction layer between the client application and the logging framework the application uses.  The logging framework can then be swapped simply by changing the implementation of interface IApplicationLogger.  This means that if/when your chosen logging framework stops being maintained or is no longer flavour of the month, the logging can be changed by updating constructor parameters and/or in dependency injection, rather than having to update potentially 1000's of lines of logging statements :smiley:.

##### Links
The documentation below was written for version 1.* of ApplicationLogging.  Minor implementation details may have changed in versions 2.0.0 and above, however the basic principles and use cases documented are still valid.

A detailed sample implementation and use case...<br />
[http://www.alastairwyse.net/methodinvocationremoting/sample-application-4.html](http://www.alastairwyse.net/methodinvocationremoting/sample-application-4.html)

Code documentation...<br />
[http://www.alastairwyse.net/methodinvocationremoting/ndoc/~ApplicationLogging.html](http://www.alastairwyse.net/methodinvocationremoting/ndoc/~ApplicationLogging.html)<br />
[http://www.alastairwyse.net/methodinvocationremoting/ndoc/~ApplicationLogging.Adapters.html](http://www.alastairwyse.net/methodinvocationremoting/ndoc/~ApplicationLogging.Adapters.html)

#### Release History

<table>
  <tr>
    <td><b>Version</b></td>
    <td><b>Changes</b></td>
  </tr>
  <tr>
    <td valign="top">2.0.0</td>
    <td>
      Migrated to .NET Standard.<br />
      ApplicationLogging.Adapters project removed (will be moved to separate NuGet package(s)).<br />
    </td>
  </tr>
  <tr>
    <td valign="top">1.4.0.1</td>
    <td>
      Initial version forked from the <a href="http://www.alastairwyse.net/methodinvocationremoting/">Method Invocation Remoting</a> project.
    </td>
  </tr>
</table>