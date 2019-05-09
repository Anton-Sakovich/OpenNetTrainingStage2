# URLParser (Day 18, Task 1)

## Task description

The task is to develop a system of types for parsing a text file containing lines of urls, each of which has the following
form:

    scheme://host[/path][?query]
    
where `path` and `query` can be ommitted. If `query` is present, then it has a form of `&`-separated list of key-value pairs:
`key1=value1&key2=value2&...`.

The result of parsing must be an XML file of the form

```xml
<urlAdresses>
  <urlAddress>
    <host name="host" />
    <uri>
      <segment>pathSegment0</segment>
      <segment>pathSegment1</segment>
    </uri>
    <parameters>
      <parameter key="key" value="value" />
    </parameters>
 </urlAddress>
</urlAdresses>
```

If the input file contains lines which do not represent a valid url (in the sense of the pattern above), those lines must be
skipped and logged.
