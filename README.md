Bits and pieces of code that I have accumulated over the course of 20 or so years of C# .NET.

Highlights include the world-famous (in my world anyway!) AbInitio log file viewer.

Plus the following working utilities and functions:
* *Classes.ExportData* - converts the contents of a DataTable to various formats which can be streamed or saved. Formats available are .xlsx, .xml, .html, .csv, .dat (pipe-delimited). Uses OpenXml to create the Excel output. The formatted data is available as a stream which can be saved to disk, or can be piped to a html response.
* *Classes.FileAssociation* - trawls the registry for file associations and returns info such as default program.
* *LDAPSearcher* - a utility for running various queries against the default or specified LDAP DB. Results can be saved in various formats. Does not currently support encrypted LDAP connections.
* *OpenHostFileDialog* - an 'OpenFileDialog' that can open files on available Win or Linux/Unix hosts. Has a facility to store commonly-used locations, including associated passwords in encrypted form.
* *SaveDataTableDialog* - a 'SaveFileDialog' dialog which calls ExportData and saves the formatted data in the selected format.

*Testy* is an app to run tests against eh above (non-AILog) apps or classes.

