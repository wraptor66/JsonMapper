
Web APIs need Intelligent Mapping
JSON Mapper makes mapping less brittle and more modular


Companies, moving to the Cloud and leveraging the use of Web APIs for exchanging data, struggle with a challenge resulting from data models passed to/fro in a JSON format. History: Legacy software solutions typically draw data from a data source like a relational database or dataset. Over the last 10 years ORM Object Relational Mapping was the tool of choice for mapping database tables to programming objects/classes.

When a company begins the daunting task of migrating their n-tier application architectures to the cloud, they do so because of the efficiencies. RDMS are often replaced by NoSQL solutions like DynamoDB and the Business Logic encapsulated in distributed Web API services. Therefore, the architecture demands the exchange of data produced and consumed to be in a common structure (JSON).

Although the use of JSON may not be rare, the complete dependency is a shift of its priority. Teams of developers are tasked with trying to map the JSON objects to their programming objects. On the surface this task may seem like the ORMs of the past, but there is one significant difference: Frequency of Data Structure Changes. Whereas, RDMS tables were rarely, if ever, modified, JSON Schemas/Structures are often modified.

The flexibility of data schema changes is the very strength of modern solutions, however the development paradigm has yet to capitalize on its introduction. Most development teams are forced to build everchanging programming models to reflect everchanging JSON Payloads. This constant gardening of data models is an enormous task multiplied by the increased endpoints and services.

JSON Mapper represents an ORM solution for Cloud Architectures and Web API solutions that exchange JSON Payloads. JSON Mapper allows for simple external declaration of Data Model Structures for population and dynamic field level mapping to Data Payloads being consumed. JSON Mapper provides “Seek” & “Scan” methods of parsing Data Payloads so if a Data Payload structure changes Key/Values can be located automatically.

JSON Mapper is not limited to only ORM activities. It provides the ability to encapsulate the introduction of business logic and data validation for the data mapping process. It is so powerful that data payloads can be consumed, processed, mapped and then invoke an action like calling a Database Action, calling another Web API Endpoint or Returning the Populated JSON Schema. This is done through the use of a programming object injected to the JSON Mapper mapping process. This process has been clocked at < 1000 milliseconds for typical data object hydration.

The opportunity is to use the JSON Mapper library as a companion to the Cloud Architecture Design. It can provide the customer, migrating to the cloud, with a “jump start” to consuming/producing JSON Payloads and ensure TCS can provide additional services.

For more information:
https://drive.google.com/file/d/1vsQbWuikyFC4qc2kTK6MV3V987OE5NpT/view?usp=share_link
