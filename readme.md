The soluion is temporarily hosted at https://donet-x-213213.appspot.com/api/customers

# Run Api

`dotnet restore`
`dotnet run --projects Api/Api.csprop --launch-profile Api`

# Run Tests

`dotnet test Tests/Tests.csproj`

# Run api tests

## Command line 

* `npm install -g newman`
* `newman run Easybet.postman_collection.json`

## GUI

* Import Easybet.postman_collection.json
* Run collection in the Runner.

# Deploy 

`dotnet publish`
`cp app.yaml Api/bin/Debug/netcoreapp2.1/publish/`
`gcloud app deploy Api/bin/Debug/netcoreapp2.1/publish/app.yaml`
