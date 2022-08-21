# BSyncRJP

A sample App built using Microservice architecture.

## Components
The app is built using 3 services: Accounts, Accounts Transactions and Security.<br />
Inter service communication is performed using gRPC. An API gateway is available to expose secured REST API endpoints. The API gateway communicates with the services using gRPC.

## How to Run The App
To run the app. start the following services: BS.Accounts.Service, BS.Secutiry.Service, BS.Transactions.Service and BS.APIGateway.<br />
The services use SQLite, therefore the database file should be created automatically if it does not exist (Migrations will be executed at startup).

### Configuration
BS.Accounts.Service requires settings AccountsTransactionsGRPCAddress (Accounts transactions gRPC service secure address) entry in appsettings.json<br />
BS.Transactions.Service requires setting AccountsGRPCAddress (Accounts gRPC service secure address) entry in appsettings.json<br />
BS.APIGateway requires setting AccountsGRPCAddress, AccountsTransactionsGRPCAddress and SecurityTransactionsGRPCAddress entry in appsettings.json.

