Presentation Layer

In accordance with the 3-Tier Architecture principles, no core business logic is implemented in the Presentation Layer. This layer is responsible only for interacting with the user and displaying meaningful messages such as success or failure notifications.

In this project, the Presentation Layer does not directly access the Data Access Layer under any circumstances. All operations are forwarded to the Business Layer, which then handles communication with the Data Access Layer. This approach ensures a clear separation of concerns and prevents tight coupling between layers.

Within this layer, contact objects are created and their properties are filled based on user input. After that, the save operation is triggered by calling the appropriate method from the Business Layer. The result returned from the Business Layer (true or false) is evaluated, and the user is informed accordingly.