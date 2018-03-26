http://localhost:63293/api/applications?orderBy=Name&pageNumber=1&pageSize=20
https://localhost:44302/api/applications?orderBy=Name&pageNumber=1&pageSize=20

SSL =>
	
	1. Project Properties => Debug (Left Tab) => Enable SSL
			(This will enal Visual Studio Self Signed Certificate for Development Purpose - IIS Express Only)
			https://localhost:44302/
			=> Project Root Foler => Properties => launchSettings.json => Change 'sslPort' to above mentioned port i.e. 44302

    2. setupAction.Filters.Add(new RequireHttpsAttribute());

	3a. File > Settings => 
		SSL certificate verification => OFF
		Automatically follow redirects => OFF

		It Will Work => https://localhost:44302/api/applications?orderBy=Name&pageNumber=1&pageSize=20
		It will not work => http://localhost:63293/api/applications?orderBy=Name&pageNumber=1&pageSize=20

	3b. File > Settings => 
		SSL certificate verification => OFF
		Automatically follow redirects => ON

		It Will Work => https://localhost:44302/api/applications?orderBy=Name&pageNumber=1&pageSize=20
		It will Work as well => http://localhost:63293/api/applications?orderBy=Name&pageNumber=1&pageSize=20



git remote add origin https://vaibhav88sharma.visualstudio.com/_git/ASPNetCore2-RESTfulAPISecuringStudentApplication

git push -u origin --all


NuGet Package Manager:-
Microsoft.EntityFrameworkCore.Tools.DotNet

OR

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.All" Version="2.0.5" />
    <DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet">
      <Version>2.0.0</Version>
    </DotNetCliToolReference>
  </ItemGroup>


EF Add Migration:-
        dotnet ef migrations add InitialCreate
        dotnet ef database update
        
EF Remove Migration:-
        dotnet ef database update 0
        dotnet ef migrations remove


ToDo =>

	1. Bulk insert applications =>
		Many Applications, Many Qualifications 
		OR
		Many Applications, Many Courses