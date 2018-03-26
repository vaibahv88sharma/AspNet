VIDEO 33.a
VIDEO 33.a
VIDEO 33.a
VIDEO 33.a
VIDEO 33.a
VIDEO 33.a
VIDEO 33.a
VIDEO 33.a
VIDEO 33.a
VIDEO 33.a
VIDEO 33.a
VIDEO 33.a
VIDEO 33.a
VIDEO 33.a
VIDEO 33.a
VIDEO 33.a
VIDEO 33.a
VIDEO 33.a
VIDEO 33.a
VIDEO 33.a


git remote add origin https://vaibhav88sharma.visualstudio.com/_git/ASPNetCore2-RESTfulAPIStudentApplication
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