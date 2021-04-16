color B

del  .PublishFiles\*.*   /s /q

dotnet restore

dotnet build

cd BiliFor.Api

dotnet publish -o ..\BiliFor.Api\bin\Debug\net5.0\

md ..\.PublishFiles

xcopy ..\BiliFor.Api\bin\Debug\net5.0\*.* ..\.PublishFiles\ /s /e 

echo "Successfully!!!! ^ please see the file .PublishFiles"

cmd