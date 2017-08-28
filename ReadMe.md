# Get Relationship Type Demo

Uses EF 6 metadata workspace API to get relationships types.

1. There are two methods in `DbContextExtensions` that are extension methods for EF `DbContext` derived classes.

    - `GetNavigationProperties`:
	  + Accepts an entity type.
	  + Returns sequence of navigation properties.
	  + `NavigationProperty` has properties for Name, RelationshipType, etc.
	- `GetRelationshipType`:
	  + Accepts an entity type and string prroperty name.
	  + Returns a `RelationshipType` enum.

2. Before running tests, create NorthwindSlim database for LocalDb.
    - Open SQL Management Studio, connect to: (localdb)\MsSqlLocalDb.
    - Create a new database named **NorthwindSlim**.
    - Download NorthwindSlip.zip from http://bit.ly/northwindslim.
    - Run NorthwindSlim.sql to create tables with data.

3. To run the tests, open the Test Explorer in Visual Studio.
    - Build the solution.
    - Click Run All to run all the tests.
	- You can also run and/or debug individual tests.