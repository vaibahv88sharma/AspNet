Environemnt Variables in Visual Studio > Project Properties > Debug Tab >

Development >
ASPNETCORE_ENVIRONMENT : Development

Production > 
ASPNETCORE_ENVIRONMENT -> Production
connectionStrings:cityInfoDBConnectionString -> Server = SPAPP05P-BRO-TES1T; Database = CityInfoDB; Trusted_Connection = True;


EF =>
	Package Manager Console =>
		1. Initial create Database

		Add-Migration CityInfoDBInitialMigration
		Update-Database

		2. Update Table Structure

		Add-Migration CityInfoDBAddPOIDescription

			a. Use Below mentioned Package Manager Console command 
				Update-Database

			b. Use Controller API
				http://localhost:61614/api/testdatabase



Packages Added =>

  <ItemGroup>
    <PackageReference Include="AutoMapper" Version="6.2.2" />
    <PackageReference Include="Microsoft.AspNetCore.All" Version="2.0.5" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="2.0.1" />
    <PackageReference Include="NLog.Extensions.Logging" Version="1.0.0-rtm-rc6" />
  </ItemGroup>



Mapping Objects =>

AutoMapper.Mapper.CreateMap = >  cfg.CreateMap<Entities.City, Models.CityWithoutPointsOfInterestDto>()		=>		SOURCE To TARGET/DESTINATION



API URLs ==>>

A. PARENT ROUTE

	1. HTTP-Get (Get) All Parents in Parent Route => 
		URL => http://localhost:61614/api/cities
		C# Code in Startup.cs => cfg.CreateMap<Entities.City, Models.CityWithoutPointsOfInterestDto>();
		Sample Output =>
			[
				{
					"id": 2,
					"name": "Antwerp",
					"description": "The one with the cathedral that was never really finished."
				},
				{
					"id": 1,
					"name": "New York City",
					"description": "The one with that big park."
				},
				{
					"id": 3,
					"name": "Paris",
					"description": "The one with that big tower."
				}
			]

	2.a. HTTP-Get (Get) All Specific Parent and optionally DO NOT include Child => 
		URL => http://localhost:61614/api/cities/1
		C# Code in Startup.cs => cfg.CreateMap<Entities.City, Models.CityDto>();
		Sample Output =>		
			{
				"id": 1,
				"name": "New York City",
				"description": "The one with that big park."
			}

	2.b. HTTP-Get (Get) All Specific Parent and optionally DO include Child => 
		URL => http://localhost:61614/api/cities/1?includePointsofinterest=true
		C# Code in Startup.cs => cfg.CreateMap<Entities.City, Models.CityDto>();
		Sample Output =>		
			{
				"id": 1,
				"name": "New York City",
				"description": "The one with that big park.",
				"numberOfPointsOfInterest": 2,
				"pointsOfInterest": [
					{
						"id": 1,
						"name": "Central Park",
						"description": "The most visited urban park in the United States. Please visit it."
					},
					{
						"id": 2,
						"name": "Empire State Building",
						"description": "A 102-story skyscraper located in Midtown Manhattan."
					}
				]
			}
		

B. CHILD ROUTE
		
	3.a. HTTP-Get (Get) All Children in Child Route => 
		URL => http://localhost:61614/api/cities/3/pointsofinterest
		C# Code in Startup.cs => cfg.CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();
		Sample Output =>
			[
				{
					"id": 5,
					"name": "Updated Eiffel Tower",
					"description": "Updated A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave Eiffel."
				},
				{
					"id": 6,
					"name": "The Louvre",
					"description": "The world's largest museum."
				}
			]

	3.b. HTTP-Get (Get) All Children in Child Route => 
		URL => http://localhost:61614/api/cities/3/pointsofinterest/5
		C# Code in Startup.cs => cfg.CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();
		Sample Output =>
			{
				"id": 5,
				"name": "Updated Eiffel Tower",
				"description": "Updated A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave Eiffel."
			}

	4. HTTP-Post (Insert) Single Child Route => 
		URL => http://localhost:61614/api/cities/3/pointsofinterest
		Data Sent to Insert =>
			{
				"name": "Delhi",
				"description": "New Delhi"
			}		
		C# Code in Startup.cs => cfg.CreateMap<Models.PointOfInterestForCreationDto, Entities.PointOfInterest>();
		Sample Output =>
			http://localhost:61614/api/cities/3/pointsofinterest/7			

	5. HTTP-Put (Full Update) Single Child Route => 
		URL => http://localhost:61614/api/cities/1/pointsofinterest/1
		Data Sent to Insert =>
			{
				"name": "Central Park",
				"description": "The most visited urban park in the United States. Please visit it."
			}		
		C# Code in Startup.cs => cfg.CreateMap<Models.PointOfInterestForUpdateDto, Entities.PointOfInterest>();
		Sample Output =>
			No Output			
			
	6. HTTP-Patch (Partial Update) Single Child Route => 
		URL => http://localhost:61614/api/cities/3/pointsofinterest/5
		Data Sent to Insert =>
			[
				{
					"op": "replace",
					"path": "/name",
					"value": "Updated Eiffel Tower"
				},
				{
					"op": "replace",
					"path": "/description",
					"value": "Updated A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave Eiffel."
				}	
			]		
		C# Code in Startup.cs => cfg.CreateMap<Entities.PointOfInterest, Models.PointOfInterestForUpdateDto>();
		Sample Output =>
			No Output
			
	7. HTTP-Delete Delete Single Child Route => 
		URL => http://localhost:61614/api/cities/3/pointsofinterest/7		
		Sample Output =>
			No Output			