start-process powershell.exe -argument "-noexit -command npm start --prefix .\DBCourseWork-frontend\dbcoursework-frontend\" | start-process powershell.exe -argument "-noexit -command dotnet run --project .\DBCourseWork\DBCourseWorkAPI\DBCourseWorkAPI.csproj"